using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SharePoint.Core;

namespace SharePoint.WebAPI.Controllers
{
    public class BranchController : ApiController
    {
        public IEnumerable<Branch> GetBranchList()
        {
            AccountInfo acctinfo = new AccountInfo();

            List<Core.Branch> branches = acctinfo.GetBranchList();

            return branches;
        }
    }
}