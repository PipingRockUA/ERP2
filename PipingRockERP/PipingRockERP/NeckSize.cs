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
    
    public partial class NeckSize
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NeckSize()
        {
            this.Bottle2 = new HashSet<Bottle2>();
        }
    
        public int NeckSizeId { get; set; }
        public string NeckSize1 { get; set; }
        public System.DateTime NeckSizeAddedDate { get; set; }
        public System.DateTime NeckSizeChangedDate { get; set; }
        public Nullable<System.DateTime> NeckSizeDeletedDate { get; set; }
        public int NeckSizeModifiedById { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bottle2> Bottle2 { get; set; }
    }
}