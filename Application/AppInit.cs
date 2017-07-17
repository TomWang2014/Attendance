using Attendance.Infrastructure.AutoMapper;
using Attendance.Infrastructure.Unity.Aop;
using Attendance.Infrastructure.Unity.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Application
{
    public class AppInit
    {
        /// <summary>
        /// 运行初始化操作
        /// </summary>
        public static void Run()
        {
            // 启动Ioc
            var ico = IocManager.Instance;

            // 启动Aop
            var interceptionInitializer = new InterceptionRegister();
            interceptionInitializer.Initialize();

            // 启动automapper
            var initializer = new AutoMapperInitializer();
            initializer.Initialize();
        }
    }
}
