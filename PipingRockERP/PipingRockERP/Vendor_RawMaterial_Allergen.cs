//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PipingRockERP
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vendor_RawMaterial_Allergen
    {
        public int Vendor_RawMaterial_AllergenId { get; set; }
        public int Vendor_RawMaterialId { get; set; }
        public int AllergenId { get; set; }
        public System.DateTime Vendor_RawMaterial_AllergenAddedDate { get; set; }
        public System.DateTime Vendor_RawMaterial_AllergenChangedDate { get; set; }
        public Nullable<System.DateTime> Vendor_RawMaterial_AllergenDeletedDate { get; set; }
        public int Vendor_RawMaterial_AllergenModifiedById { get; set; }
        public bool isDeleted { get; set; }
    
        public virtual Allergen Allergen { get; set; }
        public virtual Vendor_RawMaterial Vendor_RawMaterial { get; set; }
    }
}
