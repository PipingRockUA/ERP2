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
    
    public partial class FinishedGoodLocation
    {
        public int FinishedGoodLocationId { get; set; }
        public int FinishedGoodId { get; set; }
        public int LocationId { get; set; }
        public decimal LastCost { get; set; }
        public decimal StandardCost { get; set; }
        public decimal AverageCost { get; set; }
        public decimal FuturePoCost { get; set; }
        public System.DateTime FinishedGoodLocationAddedDate { get; set; }
        public System.DateTime FinishedGoodLocationChangedDate { get; set; }
        public Nullable<System.DateTime> FinishedGoodLocationDeletedDate { get; set; }
        public int FinishedGoodLocationModifiedById { get; set; }
        public bool isDeleted { get; set; }
    }
}
