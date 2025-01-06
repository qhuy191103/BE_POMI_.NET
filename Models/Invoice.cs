using System;
using System.Collections.Generic;

namespace restapi.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string ShippingAddress { get; set; }
        public string Notes { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
    }

}