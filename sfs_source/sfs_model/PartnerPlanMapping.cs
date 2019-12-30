using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sfs_model
{
    public class PartnerPlanMapping
    {
        public Partner PartnerData { get; set; }
        public Plan PlanData { get; set; }
        public List<Partner> PartnerList { get; set; }
        public List<Plan> PlanList { get; set; }
    }
}
