// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppDataContext.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2016/7/6 14:40:55</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Attendance.Infrastructure.Data
{
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// AppDataContext
    /// </summary>
    public class AppDataContext
    {
        /// <summary>
        /// 声明饿汉单列模式的初始化类
        /// </summary>
        private static IDbConnection instance;


        /// <summary>
        /// 定义私有公够着函数
        /// </summary>
        private AppDataContext()
        {
        }

        /// <summary>
        /// 定义全局访问该实例的对象
        /// </summary>
        public static IDbConnection Instance
        {
            get
            {
                instance = GetConnection();

                if (instance.State == ConnectionState.Closed)
                {
                    instance.Open();
                }

                return instance;
            }
        }

        /// <summary>
        /// 获得数据库连接应用池
        /// </summary>
        /// <returns>IDbConnection</returns>
        private static IDbConnection GetConnection()
        {
            return new SqlConnection(string.Empty);
        }
    }
}
