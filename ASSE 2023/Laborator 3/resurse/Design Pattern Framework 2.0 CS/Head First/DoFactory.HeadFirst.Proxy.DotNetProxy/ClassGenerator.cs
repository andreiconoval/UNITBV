using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;

// --------------------------------------------------------------------------------
// Copyright (c) 2002, Joe Walnes, Chris Stevenson, Owen Rogers
// All rights reserved.
//
// NMock is open-source and released under the BSD license.
// For license details visit nmock.org
// --------------------------------------------------------------------------------

namespace DoFactory.HeadFirst.Proxy.DotNetProxy
{
    public class ClassGenerator
    {
        public const string INVOCATION_HANDLER_FIELD_NAME = "_invocationHandler";

        internal const System.Reflection.BindingFlags ALL_INSTANCE_METHODS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        readonly protected Type type;
        readonly protected IInvocationHandler handler;
        readonly protected IList methodsToIgnore;
        readonly protected Type superclassIfTypeIsInterface;

        static ModuleBuilder moduleBuilder;
        static Hashtable mockCache = new Hashtable();
        static int typeCounter = 0;

        public ClassGenerator(Type type, IInvocationHandler handler) : this(type, handler, new ArrayList()) {}

        public ClassGenerator(Type type, IInvocationHandler handler, IList methodsToIgnore) : this(type, handler, methodsToIgnore, null) {}

        public ClassGenerator(Type type, IInvocationHandler handler, IList methodsToIgnore, Type superclassIfTypeIsInterface)
        {
            this.type = type;
            this.handler = handler;
            this.methodsToIgnore = methodsToIgnore;
            this.superclassIfTypeIsInterface = superclassIfTypeIsInterface;
        }

        public virtual object Generate()
        {
            IgnoreMethod("Equals");
            IgnoreMethod("ToString");
            IgnoreMethod("Finalize");

            return CreateProxyInstance(GetMockType());
        }

        private void IgnoreMethod(string method)
        {
            if(!methodsToIgnore.Contains(method))
            {
                methodsToIgnore.Add(method);
            }
        }

        private Type GetMockType()
        {
            MockTypeIdentifier typeIdentifier = new MockTypeIdentifier(this.type, this.methodsToIgnore);
            
            Type mockType = (Type)mockCache[typeIdentifier];

            if(mockType == null)
            {
                TypeBuilder typeBuilder = CreateTypeBuilder();

                ImplementMethods(typeBuilder);
                
                mockType = typeBuilder.CreateType();

                mockCache[typeIdentifier] = mockType;
            }

            return mockType;
        }

        private TypeBuilder CreateTypeBuilder()
        {
            if(moduleBuilder == null)
            {
                AppDomain appDomain = AppDomain.CurrentDomain;
                AssemblyName assemblyName = new AssemblyName();
                assemblyName.Name = "DynamicProxyAssembly";
                AssemblyBuilder assemblyBuilder = appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
                moduleBuilder = assemblyBuilder.DefineDynamicModule("MockModule");
            }
            
            TypeBuilder builder = moduleBuilder.DefineType(ProxyClassName, TypeAttributes.Public, ProxySuperClass, ProxyInterfaces);

            return builder;
        }

        private object CreateProxyInstance(Type proxyType)
        {
            object result = Activator.CreateInstance(proxyType, true);

            FieldInfo handlerField = proxyType.GetField(INVOCATION_HANDLER_FIELD_NAME);
            handlerField.SetValue(result, handler);

            return result;
        }

        private FieldBuilder DefineInvocationHandlerField(TypeBuilder typeBuilder)
        {
            return typeBuilder.DefineField(INVOCATION_HANDLER_FIELD_NAME, typeof(IInvocationHandler), FieldAttributes.Public);
        }

        private void ImplementMethods(TypeBuilder typeBuilder)
        {
            FieldBuilder handlerFieldBuilder = DefineInvocationHandlerField(typeBuilder);
            MethodImplementor methodImplementor = new MethodImplementor(typeBuilder, handlerFieldBuilder);
            
            foreach (Type currentType in new InterfaceLister().List(type))
            {
                foreach ( MethodInfo methodInfo in currentType.GetMethods(ALL_INSTANCE_METHODS) )
                {
                    if (ShouldImplement(methodInfo))
                    {
                        methodImplementor.Implement(methodInfo);
                    }
                }
            }
        }

        private bool ShouldImplement(MethodInfo methodInfo)
        {
            if ((!methodInfo.IsVirtual) || methodInfo.IsFinal || methodInfo.IsAssembly)
            {
                IgnoreMethod(methodInfo.Name);
            }
            return ! methodsToIgnore.Contains(methodInfo.Name);
        }

        public string ProxyClassName
        {
            get { return "Proxy" + type.Name + "_" + typeCounter++; } 
        }

        public Type ProxySuperClass
        {
            get { return type.IsInterface ? superclassIfTypeIsInterface : type; } 
        }
        public Type[] ProxyInterfaces 
        {
            get { return type.IsInterface ? new Type[] { type } : new Type[0]; } 
        }
    }
    public interface IInvocationHandler 
    {
        /// <summary>
        /// Processes a method invocation on a proxy instance and returns the result. 
        /// This method will be invoked on an invocation handler when a method is invoked on a proxy instance 
        /// with which the invocation handler is associated.
        /// </summary>
        object Invoke(string methodName, params object[] args);
    }
    public class MockTypeIdentifier
    {
        Type typeToMock;
        IList methodsToIgnore;

        public MockTypeIdentifier(Type typeToMock, IList methodsToIgnore)
        {
            this.typeToMock = typeToMock;
            this.methodsToIgnore = new ArrayList();

            for(int i = 0; i < methodsToIgnore.Count; i++)
            {
                this.methodsToIgnore.Add(methodsToIgnore[i]);
            }
        }

        public override bool Equals(object obj)
        {
            MockTypeIdentifier other = obj as MockTypeIdentifier;

            if(other == null)
            {
                return false;
            }

            if(other.typeToMock == this.typeToMock && ListEquals(this.methodsToIgnore, other.methodsToIgnore))
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return typeToMock.GetHashCode() ^ methodsToIgnore.Count;  //Lame, but simple
        }

        private bool ListEquals(IList listA, IList listB)
        {
            if(listA.Count != listB.Count)
            {
                return false;
            }

            for(int i = 0; i < listA.Count; i++)
            {
                if(listA[i] != listB[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
    public class MethodImplementor 
    { 
        private TypeBuilder typeBuilder;
        private FieldInfo invocationHandler;

        public MethodImplementor(TypeBuilder typeBuilder, FieldInfo invocationHandler)
        {
            this.typeBuilder = typeBuilder;
            this.invocationHandler = invocationHandler;
        }

        public virtual void Implement(MethodInfo method)
        {
            Type[] parameters = ExtractParameterTypes(method);
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(method.Name, 
                MethodAttributes.Public | MethodAttributes.Virtual, method.ReturnType, parameters);

            MSILStack stack = new MSILStack(methodBuilder.GetILGenerator());

            CallInvocationHandler(stack, parameters, TypeCheckedMock.StripGetSetPrefix(method.Name));

            ReturnFromMethod(stack, method.ReturnType);
        }

        private void CallInvocationHandler(MSILStack stack, Type[] parameters, string methodName)
        {
            stack.DeclareLocal(typeof(object[]));
            stack.DeclareArray(typeof(object), parameters.Length);
            stack.StoreLocal(0);

            for(int i = 0; i < parameters.Length; i++) 
            {
                stack.LoadLocal(0);
                stack.PutParamInArray(i, parameters[i]);
            }

            stack.LoadField(invocationHandler);
            stack.LoadString(methodName);
            stack.LoadLocal(0);

            stack.CallVirtual(typeof(IInvocationHandler).GetMethod("Invoke"));
        }

        private void ReturnFromMethod(MSILStack stack, Type returnType)
        {
            if (returnType == typeof(void)) 
            {
                stack.Pop();
            }
            else
            {
                if (returnType.IsValueType) 
                {
                    stack.Unbox(returnType);
                }
                else
                {
                    stack.Cast(returnType);
                }
            }

            stack.Return();
        }

        private Type[] ExtractParameterTypes(MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();
            Type[] parameterTypes = new Type[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameterTypes[i] = parameters[i].ParameterType;
            }

            return parameterTypes;
        }
    }
    public class MSILStack
    {
        private BoxingOpCodes boxingOpCodes = new BoxingOpCodes();
        private ILGenerator il;
        public string StackString;

        public MSILStack(ILGenerator il)
        {
            this.il = il;
            StackString = "";
        }

        public void Call(ConstructorInfo constructor)
        {
            il.Emit(OpCodes.Call, constructor);
        }

        public void CallVirtual(MethodInfo method)
        {
            il.EmitCall(OpCodes.Callvirt, method, null);
        }

        public void Pop()
        {
            il.Emit(OpCodes.Pop);
        }

        public void Return()
        {
            il.Emit(OpCodes.Ret);
        }

        public void Cast(Type type)
        {
            il.Emit(OpCodes.Castclass, type);
        }

        public void Box(Type type)
        {
            il.Emit(OpCodes.Box, type);
        }

        public void Unbox(Type type)
        {
            il.Emit(OpCodes.Unbox, type);

            if (type.IsPrimitive) 
            {
                if(type.IsEnum)
                {
                    il.Emit(boxingOpCodes[Enum.GetUnderlyingType(type)]);
                }
                else
                {
                    il.Emit(boxingOpCodes[type]);
                }
            } 
            else
            {
                LoadObject(type);
            }
        }

        public void LoadString(string s)
        {
            il.Emit(OpCodes.Ldstr, s);
        }

        public void LoadField(FieldInfo field)
        {
            LoadThis();

            il.Emit(OpCodes.Ldfld, field);
        }

        public void LoadObject(Type type)
        {
            il.Emit(OpCodes.Ldobj, type);
        }

        public void LoadThis()
        {
            il.Emit(OpCodes.Ldarg_0);
        }

        public void LoadArg(int index)
        {
            switch(index)
            {
                case 0: 
                    il.Emit(OpCodes.Ldarg_1);
                    break;
                case 1: 
                    il.Emit(OpCodes.Ldarg_2);
                    break;
                case 2: 
                    il.Emit(OpCodes.Ldarg_3);
                    break;
                default:
                    il.Emit(OpCodes.Ldarg_S, index + 1);
                    break;
            }
        }

        public void LoadInt32(int value)
        {
            switch(value)
            {
                case 0: il.Emit(OpCodes.Ldc_I4_0);
                    break;
                case 1: il.Emit(OpCodes.Ldc_I4_1);
                    break;
                case 2: il.Emit(OpCodes.Ldc_I4_2);
                    break;
                case 3: il.Emit(OpCodes.Ldc_I4_3);
                    break;
                case 4: il.Emit(OpCodes.Ldc_I4_4);
                    break;
                case 5: il.Emit(OpCodes.Ldc_I4_5);
                    break;
                case 6: il.Emit(OpCodes.Ldc_I4_6);
                    break;
                case 7: il.Emit(OpCodes.Ldc_I4_7);
                    break;
                case 8: il.Emit(OpCodes.Ldc_I4_8);
                    break;
                default: il.Emit(OpCodes.Ldc_I4_S, value);
                    break;
            }
        }

        public void LoadLocal(int index)
        {
            switch(index)
            {
                case 0: il.Emit(OpCodes.Ldloc_0);
                    break;
                case 1: il.Emit(OpCodes.Ldloc_1);
                    break;
                case 2: il.Emit(OpCodes.Ldloc_2);
                    break;
                case 3: il.Emit(OpCodes.Ldloc_3);
                    break;
                default: il.Emit(OpCodes.Ldloc_S, index);
                    break;
            }
        }

        public void StoreLocal(int index)
        {
            switch(index)
            {
                case 0: il.Emit(OpCodes.Stloc_0);
                    break;
                case 1: il.Emit(OpCodes.Stloc_1);
                    break;
                case 2: il.Emit(OpCodes.Stloc_2);
                    break;
                case 3: il.Emit(OpCodes.Stloc_3);
                    break;
                default: il.Emit(OpCodes.Stloc_S, index);
                    break;
            }
        }

        public void DeclareLocal(Type type)
        {
            il.DeclareLocal(type);
        }

        public void DeclareArray(Type type, int length)
        {
            LoadInt32(length);

            il.Emit(OpCodes.Newarr, type);
        }

        public void PutParamInArray(int index, Type param)
        {
            LoadInt32(index);

            LoadArg(index);

            if (param.IsByRef)
            {
                param = param.GetElementType();

                LoadIndirect(param);
            }

            if (param.IsPrimitive || param.IsValueType) 
            {
                Box(param);
            }

            il.Emit(OpCodes.Stelem_Ref);
        }

        private void LoadIndirect(Type param)
        {
            switch(Type.GetTypeCode(param))
            {
                    // Could just use ldind_ref for everything, but this produces "well-formed" MSIL.
                case TypeCode.Single : 
                    il.Emit(OpCodes.Ldind_R4); break;
                case TypeCode.Double : 
                    il.Emit(OpCodes.Ldind_R8); break;
                case TypeCode.SByte : 
                    il.Emit(OpCodes.Ldind_I1); break;                 
                case TypeCode.Int16 : 
                    il.Emit(OpCodes.Ldind_I2); break;
                case TypeCode.Int32 : 
                    il.Emit(OpCodes.Ldind_I4); break;
                case TypeCode.UInt64 :
                case TypeCode.Int64 : 
                    il.Emit(OpCodes.Ldind_I8); break;
                case TypeCode.Boolean :
                case TypeCode.Byte : 
                    il.Emit(OpCodes.Ldind_U1); break;
                case TypeCode.Char :
                case TypeCode.UInt16 : 
                    il.Emit(OpCodes.Ldind_U2); break;
                case TypeCode.UInt32 : 
                    il.Emit(OpCodes.Ldind_U4); break;
                
                default : 
                    il.Emit(OpCodes.Ldind_Ref); break;
            }
        }
    }
    public class BoxingOpCodes
    {
        private static IDictionary boxingOpCodes;

        public OpCode this[Type aType] 
        {
            get
            {
                return GetOpCode(aType);
            }
        }

        private static OpCode GetOpCode( Type aType ) 
        {
            if (boxingOpCodes == null) 
            {
                boxingOpCodes = new Hashtable();
                boxingOpCodes[typeof(sbyte)]  = OpCodes.Ldind_I1;
                boxingOpCodes[typeof(short)]  = OpCodes.Ldind_I2;
                boxingOpCodes[typeof(int)]    = OpCodes.Ldind_I4;
                boxingOpCodes[typeof(long)]   = OpCodes.Ldind_I8;
                boxingOpCodes[typeof(byte)]   = OpCodes.Ldind_U1;
                boxingOpCodes[typeof(ushort)] = OpCodes.Ldind_U2;
                boxingOpCodes[typeof(uint)]   = OpCodes.Ldind_U4;
                boxingOpCodes[typeof(ulong)]  = OpCodes.Ldind_I8;
                boxingOpCodes[typeof(float)]  = OpCodes.Ldind_R4;
                boxingOpCodes[typeof(double)] = OpCodes.Ldind_R8;
                boxingOpCodes[typeof(char)]   = OpCodes.Ldind_U2;
                boxingOpCodes[typeof(bool)]   = OpCodes.Ldind_I1;
            }
            if (boxingOpCodes.Contains(aType)) 
            {
                return (OpCode)boxingOpCodes[aType];
            }
            else 
            {
                return OpCodes.Ldind_I1;
            }
        }
    }
    // List all interfaces implemented by an interface (includes classes)
    public class InterfaceLister
    {
        public Type[] List(Type i)
        {
            ArrayList found = new ArrayList();
            walk(found, i);    
            return (Type[]) found.ToArray(typeof(Type));
        }
        
        private void walk(IList found, Type current)
        {
            if (current == null || current == typeof(Object)) 
            {
                return;
            }
            add(found, current);
            foreach(Type superType in current.GetInterfaces()) 
            {
                add(found, superType);
            }
            walk(found, current.BaseType);
        }
        
        private void add(IList found, Type item)
        {
            if (!found.Contains(item))
            {
                found.Add(item);
            }
        }
    }
    public abstract class TypeCheckedMock : Mock
    {
        private Type type;

        public TypeCheckedMock(Type type, string name) : base(name)
        {
            this.type = type;
        }

        public Type MockedType
        {
            get { return type; }
        }

        public override void SetupResult(string methodName, object returnVal, params Type[] argTypes)
        {
            ISignature signature = CreateMethodSignature(methodName, argTypes);
            CheckReturnTypeIsValid(signature, returnVal);
            SetupResult(signature, returnVal);
        }

        protected override ISignature CreateMethodSignature(string methodName, Type[] args)
        {
            ISignature signature = new SignatureFactory(MockedType).CreateMethodSignature(methodName, args);
            ValidateSignature(signature);
            return signature;
        }

        protected override IMethod getMethod(ISignature signature)
        {
            return base.getMethod(signature);
        }

        protected void CheckReturnTypeIsValid(ISignature signature, object returnVal)
        {
            if (returnVal == null)
                return;

            if (signature.HasAConstraintArgument)
                return;

            Type realReturnType = signature.ReturnType;
            if (!realReturnType.IsAssignableFrom(returnVal.GetType()))
            {
                throw new ArgumentException(
                    string.Format("method <{0}> should return a {1}",
                    signature, realReturnType));
            }
        }

        internal static string StripGetSetPrefix(string methodName)
        {
            if (methodName.StartsWith("get_") || methodName.StartsWith("set_"))
            {
                methodName = methodName.Substring(4);
            }

            return methodName;
        }

        protected virtual void ValidateSignature(ISignature signature)
        {
        }
    }
    public interface IMock : IInvocationHandler, IVerifiable
    {
        /// <summary>
        /// Name of this Mock - used for test failure readability only.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Get mocked version of object.
        /// </summary>
        object MockInstance { get; }
        
        /// <summary>
        /// If strict, any method called that doesn't have an expectation set
        /// will fail. (Defaults false)
        /// </summary>
        bool Strict { get; set; }

        /// <summary>
        /// Expect a method to be called with the supplied parameters.
        /// </summary>        
        void Expect(string methodName, params object[] args);

        /// <summary>
        /// Expect no call to this method.
        /// </summary>        
        void ExpectNoCall(string methodName, params Type[] argTypes);

        /// <summary>
        /// Expect a method to be called with the supplied parameters and setup a 
        /// value to be returned.
        /// </summary>        
        void ExpectAndReturn(string methodName, object returnVal, params object[] args);

        /// <summary>
        /// Expect a method to be called with the supplied parameters and setup an 
        /// exception to be thrown.
        /// </summary>        
        void ExpectAndThrow(string methodName, Exception exceptionVal, params object[] args);

        /// <summary>
        /// Set a fixed return value for a method/property. This allows the method to be 
        /// called multiple times in no particular sequence and have the same value returned
        /// each time. Useful for getter style methods.
        /// </summary>
        void SetupResult(string methodName, object returnVal, params Type[] argTypes);
            
    }
    public class Mock : IMock
    {
        private string name;
        private object instance;
        private bool strict;
        protected IDictionary methods;

        public Mock(string name)
        {
            this.name = name;
            methods = new Hashtable();
        }

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual object MockInstance
        {
            get { return instance; }
            set { instance = value; }
        }

        public virtual bool Strict
        {
            get { return strict; }
            set { strict = value; }
        }

        public virtual void Expect(string methodName, params object[] args)
        {
            ExpectAndReturn(methodName, null, args);
        }

        public virtual void ExpectNoCall(string methodName, params Type[] argTypes)
        {
            addExpectation(methodName, new MockNoCall(CreateMethodSignature(methodName, argTypes)));
        }

        public virtual void ExpectAndReturn(string methodName, object result, params object[] args)
        {
            addExpectation(methodName, new MockCall(CreateMethodSignature(methodName, MockCall.GetArgTypes(args)), result, null, args));
        }

        public virtual void ExpectAndThrow(string methodName, Exception e, params object[] args)
        {
            addExpectation(methodName, new MockCall(CreateMethodSignature(methodName, MockCall.GetArgTypes(args)), null, e, args));
        }

        private void addExpectation(string methodName, MockCall call)
        {
            ISignature signature = CreateMethodSignature(methodName, call.ArgTypes);
            IMethod method = getMethod(signature);
            if (method == null)
            {
                method = new Method(signature);
                methods[methodName] = method;
            }
            method.SetExpectation(call);
        }

        public virtual void SetupResult(string methodName, object returnVal, params Type[] argTypes)
        {
            SetupResult(CreateMethodSignature(methodName, argTypes), returnVal);
        }

        protected void SetupResult(ISignature signature, object returnVal)
        {
            IMethod method = getMethod(signature);
            if (method == null)
            {
                method = new CallMethodWithoutExpectation(signature.MethodName);
                methods[signature.MethodName] = method;
            }
            method.SetExpectation(new MockCall(signature, returnVal, null, null));
        }

        public virtual object Invoke(string methodName, params object[] args)
        {
            ISignature signature = CreateMethodSignature(methodName, MockCall.GetArgTypes(args));
            IMethod method = getMethod(signature);
            if (method == null)
            {
                if (strict)
                {
                    throw VerifyException.TooManyCalls(signature, 0, 1);
                }
                return null;
            }
            return method.Call(args);
        }

        public virtual void Verify()
        {
            foreach (IMethod method in methods.Values)
            {
                method.Verify();
            }
        }

        protected virtual ISignature CreateMethodSignature(string methodName, Type[] args)
        {
            return new DefaultSignature(Name, methodName, args);
        }

        protected virtual IMethod getMethod(ISignature signature)
        {
            return (IMethod) methods[signature.MethodName];
        }

        public class Assertion
        {
            public static void Assert(DefaultSignature signature, string message, bool expression)
            {
                if (!expression)
                {
                    throw new VerifyException(signature, message);
                }
            }

            public static void AssertEquals(ISignature signature, string message, object expected, object actual)
            {
                if (!expected.Equals(actual))
                {
                    throw new VerifyException(signature, message, expected, actual);
                }
            }
        }
    }
    public interface ISignature
    {
        string TypeName { get; }
        string MethodName { get; }
        Type[] ArgumentTypes { get; }
        Type ReturnType { get; }
        bool HasAConstraintArgument { get; }
        bool IsVirtual { get; }
    }

    public interface IMethod : IVerifiable
    {
        string Name { get; }
        object Call(params object[] parameters);
        void SetExpectation(MockCall call);
    }

    public interface IVerifiable
    {
        void Verify();    
    }
    public class MockCall 
    {
        protected ISignature signature;
        private IConstraint[] expectedArgs;
        private object returnValue;
        private Exception e;
    
        public MockCall(ISignature signature, object returnValue, Exception e, object[] expectedArgs) 
        {
            this.signature = signature;
            this.returnValue = returnValue;
            this.e = e;
            this.expectedArgs = argsAsConstraints(expectedArgs);
        }
    
        public virtual bool HasExpectations
        {
            get { return expectedArgs.Length > 0; }
        }

        public Type[] ArgTypes { get {return signature.ArgumentTypes; } }

        private IConstraint[] argsAsConstraints(object[] args) 
        {
            if (null == args)
            {
                return new IConstraint[0];
            }

            IConstraint[] result = new IConstraint[args.Length];
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = argAsConstraint(args[i]);
            }
            return result;
        }

        private IConstraint argAsConstraint(object arg)
        {
            IConstraint constraint = arg as IConstraint;
            if (constraint != null)
            {
                return constraint;
            }
            return arg == null ? (IConstraint)new IsAnything() : (IConstraint)new IsEqual(arg);
        }

        public virtual object Call(string methodName, object[] actualArgs) 
        {
            checkArguments(actualArgs);
    
            if (e != null) 
            {
                throw e;
            }

            return returnValue;
        }

        private void checkArguments(object[] actualArgs)
        {
            if ( HasExpectations ) 
            { 
                Mock.Assertion.AssertEquals(signature, "called with incorrect number of parameters", 
                    expectedArgs.Length, actualArgs.Length);

                for (int i = 0; i < expectedArgs.Length; i++)
                {
                    checkConstraint(expectedArgs[i], actualArgs[i], i);
                }
            }
        }

        private void checkConstraint(IConstraint expected, object actual, int index)
        {
            if (!expected.Eval(actual))
            {
                throw new VerifyException(signature, expected, actual, index);
            }
        }

        public static Type[] GetArgTypes(object[] args)
        {
            if (null == args)
            {
                return new Type[0];
            }

            Type[] result = new Type[args.Length];
            for (int i = 0; i < result.Length; ++i)
            {
                if (args[i] == null)
                {
                    result[i] = null; //typeof(object);
                }
                else
                {
                    result[i] = args[i].GetType();
                }
            }

            return result;
        }

        public static Type[] GetArgTypes(MethodBase method)
        {
            ParameterInfo[] parameters = method.GetParameters();
            Type[] argTypes = new Type[parameters.Length];

            for(int i = 0; i < parameters.Length; i++)
            {
                argTypes[i] = parameters[i].ParameterType;
            }

            return argTypes;
        }
    }
        
    public class MockNoCall : MockCall
    {
        public MockNoCall(ISignature signature) : base(signature, null, null, null)
        {
        }

        public override object Call(string methodName, object[] actualArgs) 
        {
            throw VerifyException.TooManyCalls(signature, 0, 1);
        }
    }
    public class DefaultSignature : ISignature
    {
        private string typeName;
        private string methodName;
        private Type[] argumentTypes;

        public DefaultSignature(string typeName, string methodName, params Type[] argumentTypes)
        {
            this.typeName = typeName;
            this.methodName = methodName;
            this.argumentTypes = argumentTypes;
        }

        public string TypeName
        {
            get { return typeName; }
        }

        public string MethodName
        {
            get { return methodName; }
        }

        public Type[] ArgumentTypes
        {
            get { return argumentTypes; }
        }

        public Type ReturnType
        {
            get { throw new NotImplementedException(); }
        }

        public override string ToString() 
        {
            StringBuilder argumentList = new StringBuilder();
            foreach (Type type in ArgumentTypes)
            {
                if (argumentList.Length > 0) argumentList.Append(", ");
                
                if (typeof(IConstraint).IsAssignableFrom(type))
                {
                    argumentList.Append("<constraint>");
                }
                else
                {
                    argumentList.Append(type.FullName);
                }
            }
            return string.Format("{0}.{1}({2})", TypeName, MethodName, argumentList);
        }

        public bool HasAConstraintArgument
        {
            get 
            {
                foreach(Type argType in ArgumentTypes) 
                {
                    if(typeof(IConstraint).IsAssignableFrom(argType)) 
                        return true;
                }
                return false;
            }
        }

        public bool IsVirtual
        {
            get { return false; }
        }
    }
    public interface IConstraint
    {
        bool Eval(object val);
        object ExtractActualValue(object actual);
        string Message { get; }
    }
    public class SignatureFactory
    {
        private Type type;

        public SignatureFactory(Type type)
        {
            this.type = type;
        }

        public ISignature CreateMethodSignature(string name, Type[] args)
        {
            MethodInfo method = FindMatchingMethod(name, args);
            if (method != null)
            {
                return new MethodSignature(method);
            }

            PropertyInfo property = FindMatchingProperty(name);
            if (property != null)
            {
                return new PropertySignature(property);
            }
            throw new MissingMethodException(string.Format("method <{0}> not defined", new DefaultSignature("Mock" + type.Name, name, args)));
        }

        private MethodInfo FindMatchingMethod(string methodName, Type[] args)
        {
            Type[] implementedTypes = new InterfaceLister().List(type);
            foreach (Type t in implementedTypes)
            {
                MethodInfo method = FindMatchingMethodIn(methodName, args, t);
                if (method != null)
                {
                    return method;
                }
            }
            return null;
        }


        private MethodInfo FindMatchingMethodIn(string methodName, Type[] args, Type type)
        {
            foreach (MethodInfo methodInfo in type.GetMethods(ClassGenerator.ALL_INSTANCE_METHODS))
            {
                if (MatchesNameAndAllNonNullArguments(methodName, args, methodInfo))
                {
                    return methodInfo;
                }
            }
            return null;
        }

        private bool MatchesNameAndAllNonNullArguments(string methodName, Type[] args, MethodInfo methodInfo)
        {
            if (methodName != methodInfo.Name)
                return false;

            if (! MatchNonNullArguments(args, methodInfo.GetParameters()))
                return false;

            return true;
        }

        private bool MatchNonNullArguments(Type[] args, ParameterInfo[] parameters)
        {
            if (args.Length != parameters.Length)
                return false;

            for (int pi = 0; pi < parameters.Length; pi++)
            {
                Type argumentType = args[pi];
                if (argumentType == null || typeof(IConstraint).IsAssignableFrom(argumentType))
                    continue;

                Type parameterType = parameters[pi].ParameterType;
                if (parameterType.IsByRef) parameterType = parameterType.GetElementType();    // dereference byref type

                if (!parameterType.IsAssignableFrom(argumentType))
                    return false;
            }
            return true;
        }

        private PropertyInfo FindMatchingProperty(string propertyName)
        {
            Type[] implementedTypes = new InterfaceLister().List(type);
            foreach (Type t in implementedTypes)
            {
                PropertyInfo property = FindMatchingPropertyOn(propertyName, t);
                if (property != null)
                {
                    return property;
                }
            }
            return null;
        }

        private PropertyInfo FindMatchingPropertyOn(string propertyName, Type type)
        {
            return type.GetProperty(propertyName, ClassGenerator.ALL_INSTANCE_METHODS);
        }
    }
    public class Method : IMethod
    {
        private static object NO_RETURN_VALUE = new object();

        private ISignature signature;
        private int timesCalled = 0;
        private CallSequence expectations;

        public Method(ISignature signature)
        {
            this.signature = signature;
            expectations = new CallSequence(signature);
        }

        public virtual string Name
        {
            get { return signature.MethodName; }
        }

        public virtual bool HasNoExpectations
        {
            get { return expectations.Count == 0; }
        }

        public virtual void SetExpectation(MockCall aCall)
        {
            expectations.Add(aCall);
        }

        public virtual object Call(params object[] parameters)
        {
            MockCall mockCall = expectations[timesCalled];
            timesCalled++;
            return mockCall.Call(signature.MethodName, parameters);
        }

        public virtual void Verify()
        {
            if (expectations.CountExpectedCalls > timesCalled)
            {
                throw VerifyException.NotEnoughCalls(
                    signature, expectations.CountExpectedCalls, timesCalled);
            }
        }

        /// <summary>
        /// Specialised collection class for Method Calls
        /// </summary>
        public class CallSequence
        {
            private ISignature signature;
            private ArrayList sequence = new ArrayList();

            public CallSequence(ISignature signature)
            {
                this.signature = signature;
            }

            public MockCall this[int timesCalled]
            {
                get
                {
                    if (sequence.Count <= timesCalled)
                    {
                        throw VerifyException.TooManyCalls(signature, sequence.Count, timesCalled + 1);
                    }
                    return (MockCall) sequence[timesCalled];
                }
            }

            public int Count
            {
                get { return sequence.Count; }
            }

            public int CountExpectedCalls
            {
                get
                {
                    int count = 0;
                    foreach (Object mockCall in sequence)
                    {
                        if (! (mockCall is MockNoCall))
                        {
                            count++;
                        }
                    }
                    return count;
                }
            }

            public void Add(MockCall aCall)
            {
                sequence.Add(aCall);
            }
        }
    }
    public class CallMethodWithoutExpectation : IMethod
    {
        private string name;
        private MockCall call;

        public CallMethodWithoutExpectation(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }

        public virtual void SetExpectation(MockCall call)
        {
            this.call = call;
        }

        public virtual object Call(params object[] parameters)
        {
            return call.Call(name, parameters);
        }

        public virtual void Verify() 
        {
            // noop
        }
    }
    public class VerifyException : Exception
    {
        private string reason;
        private string expected;
        private object actual;

        public VerifyException(ISignature signature, string reason)
        {
            this.reason = signature + " " + reason;
        }

        public VerifyException(ISignature signature, string reason, object expected, object actual) : this(signature, reason)
        {
            this.expected = "<" + expected + ">";
            this.actual = actual;
        }

        public VerifyException(ISignature signature, IConstraint constraint, object actual, int index)
        {
            reason = string.Format("{0} called with incorrect parameter ({1})", signature, index + 1);
            this.expected = constraint.Message;
            this.actual = constraint.ExtractActualValue(actual);
        }


        public static VerifyException TooManyCalls(ISignature signature, int expected, int actual)
        {
            if (expected >= actual)
            {
                throw new ArgumentException("expected >= actual");
            }
            return new VerifyException(
                signature, "called too many times", expected, actual);
        }

        public static VerifyException NotEnoughCalls(ISignature signature, int expected, int actual)
        {
            if (expected <= actual)
            {
                throw new ArgumentException("expected <= actual");
            }
            string message = (actual == 0) ? "never called" : "not called enough times";
            return new VerifyException(signature, message, expected, actual);
        }


        public string Reason
        {
            get { return reason; }
        }

        public object Expected
        {
            get { return expected; }
        }

        public object Actual
        {
            get { return actual; }
        }

        public override string Message
        {
            get
            {
                StringBuilder message = new StringBuilder();
                message.AppendFormat("\n\t{0}", reason);

                if (expected != null || actual != null)
                {
                    message.AppendFormat("\n\n\texpected:{0}\n\t but was:<{1}>", expected, actual);
                }
                return message.ToString();
            }
        }
    }

    public class PropertySignature : ISignature
    {
        private PropertyInfo property;

        public PropertySignature(PropertyInfo property)
        {
            this.property = property;
        }

        public string TypeName
        {
            get { return property.DeclaringType.Name; }
        }

        public string MethodName
        {
            get { return property.Name; }
        }

        public Type[] ArgumentTypes
        {
            get { return null; }
        }

        public Type ReturnType
        {
            get { return property.PropertyType; }
        }

        public bool HasAConstraintArgument
        {
            get { return false; }
        }

        public bool IsVirtual
        {
            get { return IsVirtualOrNull(property.GetGetMethod(true)) && IsVirtualOrNull(property.GetSetMethod(true)); }
        }

        private bool IsVirtualOrNull(MethodInfo method)
        {
            return method == null || method.IsVirtual;
        }

        public override string ToString()
        {
            return string.Format("Mock{0}.{1})", TypeName, MethodName);
        }
    }
    public class MethodSignature : ISignature
    {
        private MethodInfo method;

        public MethodSignature(MethodInfo method)
        {
            this.method = method;
        }

        public string TypeName
        {
            get { return method.DeclaringType.Name; }
        }

        public string MethodName
        {
            get { return method.Name; }
        }

        public Type[] ArgumentTypes
        {
            get { return GetParameterTypes(); }
        }

        public Type ReturnType
        {
            get { return method.ReturnType; }
        }

        private Type[] GetParameterTypes()
        {
            ArrayList types = new ArrayList();
            foreach (ParameterInfo parameter in method.GetParameters())
            {
                if(parameter.ParameterType.IsByRef)
                {
                    types.Add(parameter.ParameterType.GetElementType());
                }
                else
                {
                    types.Add(parameter.ParameterType);
                }
            }
            return (Type[]) types.ToArray(typeof (Type));
        }

        public bool HasAConstraintArgument
        {
            get { return false; }
        }

        public bool IsVirtual
        {
            get { return method.IsVirtual; }
        }

        public override string ToString() 
        {
            StringBuilder argumentList = new StringBuilder();
            foreach (Type type in ArgumentTypes)
            {
                if (argumentList.Length > 0) argumentList.Append(", ");
                
                if (typeof(IConstraint).IsAssignableFrom(type))
                {
                    argumentList.Append("<constraint>");
                }
                else
                {
                    argumentList.Append(type.FullName);
                }
            }
            return string.Format("Mock{0}.{1}({2})", TypeName, MethodName, argumentList);
        }
    }
    public class IsNull : IPredicate
    {
        public bool eval(object val)
        {
            return val == null;
        }
    }
    
    public class IsAnything : IPredicate
    {
        public bool eval(object val) 
        {
            return true;
        }
    }

    public class IsIn : IPredicate 
    {
        private object[] inList;
        
        public IsIn(params object[] inList)
        {
            if (inList.Length == 1 && inList[0].GetType().IsArray)
            {
                Array arr = (Array)inList[0];
                this.inList = new object[arr.Length];
                arr.CopyTo(this.inList, 0);
            }
            else
            {
                this.inList = inList;
            }
        }

        public bool eval(object val)
        {
            foreach (object o in inList)
            {
                if (o.Equals(val))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class IsEqual : IPredicate 
    {
        private object compare;
        
        public IsEqual(object compare)
        {
            this.compare = compare;
        }

        public bool eval(object val)
        {
            return compare.Equals(val);
        }
    }
    
    public class IsTypeOf : IPredicate 
    {
        private Type type;
        
        public IsTypeOf(Type type)
        {
            this.type = type;
        }

        public bool eval(object val)
        {
            return val == null ? false : type.IsAssignableFrom(val.GetType()); 
        }
    }
    
    public class Not : IPredicate 
    {
        private IPredicate p;
        
        public Not(IPredicate p)
        {
            this.p = p;
        }
        
        public bool eval(object val) 
        {
            return !p.eval(val);
        }
    }

    public class And : IPredicate
    {
        private IPredicate p1, p2;
        
        public And(IPredicate p1, IPredicate p2) 
        {
            this.p1 = p1;
            this.p2 = p2;
        }
        
        public bool eval(object val) 
        {
            return p1.eval(val) && p2.eval(val);
        }
    }

    public class Or : IPredicate
    {
        private IPredicate p1, p2;
        
        public Or(IPredicate p1, IPredicate p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
        
        public bool eval(object val)
        {
            return p1.eval(val) || p2.eval(val);
        }
    }

    public class NotNull : IPredicate 
    {
        public bool eval(object val)
        {
            return val != null;
        }
    }
    
    public class NotEqual : IPredicate
    {
        private IPredicate p;
        
        public NotEqual(object compare)
        {
            p = new Not(new IsEqual(compare));
        }

        public bool eval(object val)
        {
            return p.eval(val);
        }
    }
    
    public class NotIn : IPredicate 
    {
        private IPredicate p;
        
        public NotIn(params object[] inList)
        {
            p = new Not(new IsIn(inList));
        }

        public bool eval(object val)
        {
            return p.eval(val);
        }
    }
    
    public class IsEqualIgnoreCase : IPredicate 
    {
        private IPredicate p;
        
        public IsEqualIgnoreCase(object compare)
        {
            p = new IsEqual(compare.ToString().ToLower());
        }

        public bool eval(object val)
        {
            return p.eval(val.ToString().ToLower());
        }
    }
    
    public class IsEqualIgnoreWhiteSpace : IPredicate
    {
        private IPredicate p;
        
        public IsEqualIgnoreWhiteSpace(object compare)
        {
            p = new IsEqual(StripSpace(compare.ToString()));
        }

        public bool eval(object val)
        {
            return p.eval(StripSpace(val.ToString()));
        }
        
        public static string StripSpace(string s)
        {
            StringBuilder result = new StringBuilder();
            bool lastWasSpace = true;
            foreach(char c in s)
            {
                if (Char.IsWhiteSpace(c))
                {
                    if (!lastWasSpace)
                    {
                        result.Append(' ');
                    }
                    lastWasSpace = true;                    
                }
                else
                {
                    result.Append(c);
                    lastWasSpace = false;                    
                }
            }
            return result.ToString().Trim();
        }
    }
    
    public class IsMatch : IPredicate 
    {
        private Regex regex;
        
        public IsMatch(Regex regex)
        {
            this.regex = regex;
        }
        
        public IsMatch(String regex) : this(new Regex(regex))
        {
        }
        
        public IsMatch(String regex, bool ignoreCase) : 
            this(new Regex(regex, ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None)) 
        {
        }
        
        public bool eval(object val)
        {
            return val == null ? false : regex.IsMatch(val.ToString());
        }
    }
    
    public class IsCloseTo : IPredicate 
    {
        
        private double expected;
        private double error;
        
        public IsCloseTo(double expected, double error)
        {
            this.expected = expected;
            this.error = error;
        }
        
        public bool eval(object val)
        {
            try
            {
                double actual = Convert.ToDouble(val);
                return Math.Abs(actual - expected) <= error;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
    
    public class Predicate : IPredicate
    {

        public delegate bool Method(object val);
        private Method m;
        
        public Predicate(Method m)
        {
            this.m = m;    
        }
        
        public bool eval(object val)
        {
            return m(val);
        }
    }

    public interface IPredicate
    {
        bool eval(object val);
    }
}
