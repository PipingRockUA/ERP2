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
    
    public partial class Vendor_RawMaterial
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vendor_RawMaterial()
        {
            this.Vendor_RawMaterial_Allergen = new HashSet<Vendor_RawMaterial_Allergen>();
        }
    
        public int Vendor_RawMaterialId { get; set; }
        public int VendorId { get; set; }
        public int RawMaterialId { get; set; }
        public bool isCurrentVendor { get; set; }
        public bool isRejectedVendor { get; set; }
        public System.DateTime Vendor_RawMaterialAddedDate { get; set; }
        public System.DateTime Vendor_RawMaterialChangedDate { get; set; }
        public Nullable<System.DateTime> Vendor_RawMaterialDeletedDate { get; set; }
        public int Vendor_RawMaterialModifiedById { get; set; }
        public bool isDeleted { get; set; }
    
        public virtual RawMaterial RawMaterial { get; set; }
        public virtual Vendor Vendor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vendor_RawMaterial_Allergen> Vendor_RawMaterial_Allergen { get; set; }
    }
}
