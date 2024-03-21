using System;
using System.Collections.Generic;
using System.Text;

using System.Web;
using System.Web.SessionState;

namespace DoFactory.Framework.ViewState
{
    /// <summary>
    /// Viewstate provider that is implemented using session objects.
    /// 
    /// Gof Design Pattern: Strategy.
    /// </summary>
    /// <remarks>
    /// The Strategy Design Pattern ensures that this class is 'pluggable' and 
    /// can fully function as a viewstate provider.
    /// </remarks>
    public class ViewStateProviderSession : ViewStateProviderBase
    {
        /// <summary>
        /// Saves view state information for the web page in a session object.
        /// </summary>
        /// <param name="name">Name of the viewstate.</param>
        /// <param name="viewState">Viewstate.</param>
        public override void SavePageState(string name, object viewState)
        {
            HttpSessionState session = HttpContext.Current.Session;
            session[name] = viewState;
        }

        /// <summary>
        /// Retrieves viewstate information for the web page from session.
        /// </summary>
        /// <param name="name">Name of the viewstate.</param>
        /// <returns>Viewstate.</returns>
        public override object LoadPageState(string name)
        {
            HttpSessionState session = HttpContext.Current.Session;
            return session[name];
        }
    }
}
