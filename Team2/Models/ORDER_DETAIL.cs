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
    
    public partial class ORDER_DETAIL
    {
        public int ID { get; set; }
        public int ORDER_ID { get; set; }
        public int MENU_ID { get; set; }
        public int QUANTITY { get; set; }
        public int PRICE { get; set; }
    
        public virtual MENU MENU { get; set; }
        public virtual ORDER ORDER { get; set; }
    }
}
