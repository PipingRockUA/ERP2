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
    
    public partial class Quarantine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quarantine()
        {
            this.Bulk_Quarantine = new HashSet<Bulk_Quarantine>();
            this.FinishedGood_Quarantine = new HashSet<FinishedGood_Quarantine>();
        }
    
        public int QuarantineId { get; set; }
        public string Quarantine1 { get; set; }
        public System.DateTime QuarantineAddedDate { get; set; }
        public System.DateTime QuarantineChangedDate { get; set; }
        public Nullable<System.DateTime> QuarantineDeletedDate { get; set; }
        public int QuarantineModifiedById { get; set; }
        public bool isDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bulk_Quarantine> Bulk_Quarantine { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinishedGood_Quarantine> FinishedGood_Quarantine { get; set; }
    }
}
