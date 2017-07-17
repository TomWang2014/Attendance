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
    
    public partial class NetSysUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NetSysUser()
        {
            this.NetTenant = new HashSet<NetTenant>();
        }
    
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public Nullable<int> Gender { get; set; }
        public string IdCardNo { get; set; }
        public int SysRoleId { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreationTime { get; set; }
    
        public virtual NetSysRole NetSysRole { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NetTenant> NetTenant { get; set; }
    }
}