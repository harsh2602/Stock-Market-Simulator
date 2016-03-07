using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StockMarketSimulator.Startup))]
namespace StockMarketSimulator
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
