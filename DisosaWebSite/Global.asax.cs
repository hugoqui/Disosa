using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DisosaWebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            keepAliveThread.Start();
        }

        static Thread keepAliveThread = new Thread(KeepAlive);

        protected void Application_End()
        {
            keepAliveThread.Abort();
        }

        static void KeepAlive()
        {
            while (true)
            {
                WebRequest req = WebRequest.Create("http://www.disosagt.com");
                req.GetResponse();
                try
                {
                    Thread.Sleep(48000);
                }
                catch (ThreadAbortException)
                {
                    break;
                }
            }
        }
    }
}
