//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace oracledb
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRODUCT
    {
        public int ID { get; set; }
        public int VENDOR_ID { get; set; }
        public string PRODUCT_NAME { get; set; }
        public int MEASURE_ID { get; set; }
        public decimal PRICE { get; set; }
    
        public virtual MEASURE MEASURE { get; set; }
        public virtual VENDOR VENDOR { get; set; }
    }
}
