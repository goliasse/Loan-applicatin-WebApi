using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SharePoint.Core;

namespace SharePoint.WebAPI.Controllers
{
    public class ApplicantLoansController : ApiController
    {
         
             //IHttpActionResult
        public IEnumerable<Loan> GetApplicantLoans(string accountid)
        {
            AccountInfo acctinfo = new AccountInfo();

            List<Core.Loan> loans = acctinfo.getApplicantLoans(accountid);

            //if (loans == null)
            //{
            //    return NotFound();
            //}
            //return Ok(loans);

            return loans;
        }
    }
}
