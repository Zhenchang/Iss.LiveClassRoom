using Iss.LiveClassRoom.Ioc.App_Start;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Ioc {
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            app.MapSignalR(new Microsoft.AspNet.SignalR.HubConfiguration()
            {
                Resolver = new SignalRUnityDependencyResolver(UnityConfig.GetConfiguredContainer())
            });
        }
    }
}
