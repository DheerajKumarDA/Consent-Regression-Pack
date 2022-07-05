using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNUnitExtentReport.Data
{
    public class DecisionOptInOptOutData
    {
        public string CustomerIdNumber { get; set; }
        public Int64 PartyId { get; set; }
        public DateTime ModifiedDate { get; set; }

        public string Brand { get; set; }

        public string IdNumber { get; set; }
        public string HoldingCompanyId { get; set; }
        public string Registered { get; set; }

        public int BrandId { get; set; }

        public string Product { get; set; }

        public string Channel { get; set; }

        public string AcceptanceStatus { get; set; }

        public string AcceptanceStatusType { get; set; }

        public string TestPack { get; set; }
    }
}

