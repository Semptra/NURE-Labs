//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lab3
{
    using System;
    using System.Collections.Generic;
    
    public partial class StageEnd
    {
        public System.Guid Id { get; set; }
        public System.DateTime Date { get; set; }
        public System.Guid UserId { get; set; }
        public int Stage { get; set; }
        public int Time { get; set; }
        public bool Win { get; set; }
        public int Income { get; set; }
    
        public virtual User User { get; set; }
    }
}
