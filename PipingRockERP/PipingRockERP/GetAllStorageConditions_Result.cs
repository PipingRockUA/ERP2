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
    
    public partial class GetAllStorageConditions_Result
    {
        public int StorageConditionId { get; set; }
        public string StorageCondition { get; set; }
        public string StorageConditionDescription { get; set; }
        public System.DateTime StorageConditionAddedDate { get; set; }
        public System.DateTime StorageConditionChangedDate { get; set; }
        public Nullable<System.DateTime> StorageConditionDeletedDate { get; set; }
        public int StorageConditionModifiedById { get; set; }
        public bool isDeleted { get; set; }
    }
}