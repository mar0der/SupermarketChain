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
    
    public partial class VENDOR
    {
        public VENDOR()
        {
            this.PRODUCTS = new HashSet<PRODUCT>();
        }
    
        public int ID { get; set; }
        public string VENDOR_NAME { get; set; }
    
        public virtual ICollection<PRODUCT> PRODUCTS { get; set; }
    }
}
