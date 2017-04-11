using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePoint.Core
{
    public class Loan
    {
        public string FacilityType { get; set; }
        public string acct_no { get; set; }
        public string FacilityAmount { get; set; }
        public string RepaymentFreq { get; set; }
        public string LastRepaymentAmount { get; set; }
        public string NextRepaymentAmount { get; set; }
        public string CurrentBalance { get; set; }
        public string FacilityMaturityDate { get; set; }
    }
}
