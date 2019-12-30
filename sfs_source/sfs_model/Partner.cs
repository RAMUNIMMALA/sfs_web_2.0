using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sfs_model
{
    public class Partner
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PartnerId { get; set; }
        public int PartnerCode { get; set; }
        public int PlanCode { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Image { get; set; }
        public string ContactNo { get; set; }
        public string GenderType { get; set; }
        public string MailId { get; set; }
        public int RoleCode { get; set; }
        public int MappedPlans { get; set; }
        public string RefName { get; set; }
        public string Occupation { get; set; }
        public string PrimaryId { get; set; }
        public int PartnerType { get; set; }
        public int StatusType { get; set; }
        public List<PartnerType> PType { get; set; }
        public List<StatusType> SType { get; set; }
        public List<GenderType> GType { get; set; }
        public List<Plan> PMapping { get; set; }
        public List<PartnerData> PartnerData { get; set; }
        public int Result { get; set; }
        public DateTime RegisteredDate { get; set; }
    }
}
