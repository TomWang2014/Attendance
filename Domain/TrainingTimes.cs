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
    
    public partial class TrainingTimes
    {
        public int Id { get; set; }
        public int TrainingId { get; set; }
        public System.DateTime TrainingDate { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public Nullable<int> Type { get; set; }
        public System.DateTime CreateTime { get; set; }
    
        public virtual Training Training { get; set; }
    }
}
