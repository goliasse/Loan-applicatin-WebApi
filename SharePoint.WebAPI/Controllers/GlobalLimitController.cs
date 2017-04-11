using SharePoint.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SharePoint.WebAPI.Controllers
{
    public class GlobalLimitController : ApiController
    {        

        public GlobalLimit GetGlobalLimit(string rimid, string classcode)
        {
            AccountInfo acctinfo = new AccountInfo();
            GlobalLimit gLimit = new GlobalLimit();

            gLimit = acctinfo.getRimInfo(rimid,classcode);

            return gLimit;
        }
    }
}
