using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace sfs_model
{
    public class Plan
    {
      public int PlanCode { get; set; }
      public int PartnerCode { get; set; }
      public string Title { get; set; }
      public string Details { get; set; }
      [DataType(DataType.Date)]
      public DateTime StartDate { get; set; }
      [DataType(DataType.Date)]
      public DateTime EndDate { get; set; }
      public int? NoofMonths { get; set; }
      public int? NoofPartners { get; set; }
      public int? MappedMembers { get; set; }
      public List<PlanType> PType { get; set; }
      public int PlanType { get; set; }
      public float? Commission { get; set; }
      public decimal? PlanValue { get; set; }
      public decimal? MonthlyAmount { get; set; }
      public decimal? AuctionBaseAmount { get; set; }
      public int PlanMemberMappingId { get; set; }
      public int StatusType { get; set; }
      public List<StatusType> SType { get; set; }
      public int Result { get; set; }
  }
}
