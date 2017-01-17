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
    
    public partial class ItemStatu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ItemStatu()
        {
            this.Bulks = new HashSet<Bulk>();
            this.FinishedGoods = new HashSet<FinishedGood>();
            this.RawMaterials = new HashSet<RawMaterial>();
        }
    
        public int ItemStatusId { get; set; }
        public string ItemStatus { get; set; }
        public string ItemStatusDescription { get; set; }
        public System.DateTime ItemStatusAddedDate { get; set; }
        public System.DateTime ItemStatusChangedDate { get; set; }
        public Nullable<System.DateTime> ItemStatusDeletedDate { get; set; }
        public int ItemStatusModifiedById { get; set; }
        public bool isDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bulk> Bulks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinishedGood> FinishedGoods { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RawMaterial> RawMaterials { get; set; }
    }
}
