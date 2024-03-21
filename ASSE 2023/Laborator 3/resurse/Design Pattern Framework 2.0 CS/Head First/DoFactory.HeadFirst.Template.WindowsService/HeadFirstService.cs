using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;

namespace DoFactory.HeadFirst.Template.WindowsService
{
    public class HeadFirstService : System.ServiceProcess.ServiceBase
    {
        private System.ComponentModel.Container components = null;

        public HeadFirstService()
        {
            InitializeComponent();

            // TODO: Add any initialization after the InitComponent call
        }

        // The main entry point for the process
        static void Main()
        {
            System.ServiceProcess.ServiceBase[] ServicesToRun;
    
            ServicesToRun = new System.ServiceProcess.ServiceBase[] { new HeadFirstService() };

            System.ServiceProcess.ServiceBase.Run(ServicesToRun);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "HeadFirstService";
        }

        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if (components != null) 
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        // Design Patterns: Template method

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
        }
 
        // Design Patterns: Template method

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
