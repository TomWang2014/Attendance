//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Attendance.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class NetTenant
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string FrontImage { get; set; }
        public string Logo { get; set; }
        public string Descriptions { get; set; }
        public bool IsDelete { get; set; }
        public int CreateUser { get; set; }
        public System.DateTime CreateTime { get; set; }
        public Nullable<int> OperUser { get; set; }
        public Nullable<System.DateTime> OperTime { get; set; }
        public int UserId { get; set; }
    
        public virtual NetSysUser NetSysUser { get; set; }
    }
}
