//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Store2DoorDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int order_id { get; set; }
        public int user_id { get; set; }
        public System.DateTime date { get; set; }
        public string status { get; set; }
        public decimal total_bill { get; set; }
    }
}