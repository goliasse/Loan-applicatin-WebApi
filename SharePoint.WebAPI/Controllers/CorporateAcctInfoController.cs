using SharePoint.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SharePoint.WebAPI.Controllers
{
    public class CorporateAcctInfoController : ApiController
    {
        public Account GetApplicantLoans(string accountid, string initiator_number)
        {
            AccountInfo acctinfo = new AccountInfo();
            Account acct = new Account();

            acct = acctinfo.getCorporateAccountInfo(accountid, initiator_number);

            //if (loans == null)
            //{
            //    return NotFound();
            //}
            //return Ok(loans);

            return acct;
        }
    }
}
