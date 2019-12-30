using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace sfs_model
{
    public class Payment
    {
        public string Title { get; set; }
        public string PlanTitle { get; set; }
        public string PartnerName { get; set; }
        public string PartnerId { get; set; }
        public int PartnerCode { get; set; }
        public decimal? Amount { get; set; }
        public int PaymentType { get; set; }
        public string Comments { get; set; }
        public string Attachment { get; set; }
        [DataType(DataType.Date)]
        public DateTime PaidDate { get; set; }
        public int PlanMemberMappingId { get; set; }
        public List<PaymentType> PType { get; set; }
        public int Result { get; set; }
    }
}
