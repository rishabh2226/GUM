//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GUM.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class PaymentDetail
    {
        public long PaymentReferenceNumber { get; set; }
        public int PaymentMethodID { get; set; }
        public long OrderID { get; set; }
        public string PaymentStatus { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}