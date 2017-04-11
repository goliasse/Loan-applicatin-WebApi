using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePoint.Core
{
    public class Account
    {
        public string account_name { get; set; }
        public string acct_no { get; set; }
        public string acct_type { get; set; }
        public string domicile_branch_name { get; set; }
        public string domicile_branch_no { get; set; }
        public string rim_no { get; set; }
        public string rsm_id { get; set; }
        public string rsm_name { get; set; }
        public string rimrsm_id { get; set; }
        public string rimrsm_name { get; set; }
        public string rimrsm_username { get; set; }
        public string rsm_staffno { get; set; }       
 
        //New properties to be returned//
        /**
        Title, Sex, Marital Status, Date of Birth, Address1, Address2, 
        State of Origin, Phone number, Email, next of kin, 
        next of kin relations, next of kin phone
        */
        public string sex { get; set; }
        public string marital_status { get; set; }
        public string dob { get; set; }
        public string home_address { get; set; }  
        //public string address2 { get; set; }  
        public string state_of_origin { get; set; }  
        public string phonenumber { get; set; }  
        public string email { get; set; } 
        public string next_of_kin { get; set; }  
        public string next_of_kin_relations { get; set; }  
        public string next_of_kin_phone { get; set; } 
 
        //New properties to be returned//
        /**03 NOVEMBER 2015**/
        public string bvn { get; set; }
        public string acct_type_desc { get;set;}

         //New properties to be returned//
        /**25 October 2016**/
        public string initiator_branch { get; set; }
        public string initiator_branchcode { get; set; } 
        public string initiator_phoenix_username { get; set; } 
        public string initiator_phoenix_employee_id { get; set; } 
        public string initiator_phoenix_status { get; set; }
    }
}
