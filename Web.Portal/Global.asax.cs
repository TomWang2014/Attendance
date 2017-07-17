namespace Attendance.Web.Portal
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;


    using Attendance.Infrastructure.Mvc;
    using Attendance.Infrastructure.Unity.Ioc;
    using Attendance.Web.Portal.Toolkits;
    using System.Web.Http;
    using System.IO;
    using Attendance.Application;
 

    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication:HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();                
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // 实现自定义的依赖注入控制器
            var container = IocManager.Instance.GetContainer();
            var factory = new UnityControllerFactory(container);
            ControllerBuilder.Current.SetControllerFactory(factory);

            // 运行应用程序初始化操作
            AppInit.Run();
            ToolkitsHelper.InitAllFunc();

            // 启用EF性能调试工具
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();

        
        }
    }
}