//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ActivityStatus
    {
        public ActivityStatus()
        {
            this.Activity = new HashSet<Activity>();
        }
    
        public int ID { get; set; }
        public string Status { get; set; }
    
        public virtual ICollection<Activity> Activity { get; set; }
    }
}
