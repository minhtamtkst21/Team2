//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Team2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NOTIFICATION
    {
        public int ID { get; set; }
        public Nullable<int> ACCOUNT_ID { get; set; }
        public Nullable<int> ORDER_ID { get; set; }
        public Nullable<System.DateTime> DATE { get; set; }
        public string CONTENT { get; set; }
    
        public virtual ACCOUNT ACCOUNT { get; set; }
        public virtual ORDER ORDER { get; set; }
    }
}
