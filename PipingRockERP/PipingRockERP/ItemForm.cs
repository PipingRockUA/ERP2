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
    
    public partial class ItemForm
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ItemForm()
        {
            this.Bulks = new HashSet<Bulk>();
        }
    
        public int ItemFormId { get; set; }
        public string ItemFormExternal { get; set; }
        public string ItemFormWarehouse { get; set; }
        public System.DateTime ItemFormAddedDate { get; set; }
        public System.DateTime ItemFormChangedDate { get; set; }
        public Nullable<System.DateTime> ItemFormDeletedDate { get; set; }
        public int ItemFormModifiedById { get; set; }
        public bool isDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bulk> Bulks { get; set; }
    }
}