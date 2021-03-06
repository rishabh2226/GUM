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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.Invoices = new HashSet<Invoice>();
            this.OrderDetails = new HashSet<OrderDetail>();
            this.PaymentDetails = new HashSet<PaymentDetail>();
        }
    
        public long OrderID { get; set; }
        public long UserID { get; set; }
        public long OrderNumber { get; set; }
        public System.DateTime OrderDate { get; set; }
        public System.DateTime ShipDate { get; set; }
        public long ShipperID { get; set; }
        public int PaymentMethodID { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingPinCode { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string OrderStatus { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual Shipper Shipper { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }
    }
}
