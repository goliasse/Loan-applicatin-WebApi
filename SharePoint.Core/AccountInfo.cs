using System.Data;
using Sybase.Data.AseClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePoint.Core
{
    public class AccountInfo
    {
        public List<Loan> getApplicantLoans(string acct_no)
        {
            List<Loan> loans = new List<Loan>();
            Loan loan = null;            
            AseConnection conn = null;
            AseCommand command = null;
            AseDataReader reader = null;
            StringBuilder s = new StringBuilder();
            StringBuilder output = new StringBuilder();
            string cnstring = ConfigurationManager.ConnectionStrings["phoenixConnectionString"].ConnectionString;
            string sqltext = "zsp_cust_exist_loans";
            //string sqltext = "select (select description from phoenix..ad_ln_cls where class_code = a.class_code) as 'FacilityType',a.acct_no";
            //sqltext += ",a.amt as 'FacilityAmount'";
            //sqltext += ",a.period as 'RepaymentFreq'";
            //sqltext += ",a.last_pmt_amt as 'LastRepaymentAmount'";
            //sqltext += ",a.nxt_pmt_amt as 'NextRepaymentAmount'";
            //sqltext += ",a.col_bal as 'CurrentBalance'";
            //sqltext += ",a.mat_dt as 'FacilityMaturityDate'";
            //sqltext += " from phoenix..ln_display a, phoenix..dp_acct b ";
            //sqltext += "where b.acct_no = @applicantacct_no ";// -----'1020041488'
            //sqltext += "and a.rim_no = b.rim_no ";
            //sqltext += "and a.class_code != 585 ";
            //sqltext += "and a.col_bal != 0 ";
            //sqltext += "and a.status != 'Closed' ";

            try
            {
                using (conn = new AseConnection(cnstring))
                {
                    conn.Open();

                    using (command = new AseCommand(sqltext, conn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        //command.Parameters.AddWithValue("@applicantacct_no", acct_no);
                        //AseParameter param = new AseParameter();
                        //param.ParameterName = "@applicantacct_no";
                        //param.Value = acct_no;

                        AseParameter param = new AseParameter();
                        param.ParameterName = "@psApplicantacct_no";
                        param.Value = acct_no;
                        
                        command.Parameters.Add(param);
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            loan = new Loan();
                            loan.FacilityType = reader["FacilityType"].ToString();
                            loan.acct_no = reader["acct_no"].ToString();
                            loan.FacilityAmount = reader["FacilityAmount"].ToString();
                            loan.RepaymentFreq = reader["RepaymentFreq"].ToString();
                            loan.LastRepaymentAmount = reader["LastRepaymentAmount"].ToString();
                            loan.NextRepaymentAmount = reader["NextRepaymentAmount"].ToString();
                            loan.FacilityMaturityDate = reader["FacilityMaturityDate"].ToString();
                            loan.CurrentBalance = reader["CurrentBalance"].ToString();
                            loans.Add(loan);
                        }
                    }
                }
            }
            catch (Exception ex)
            {                
                throw;
            }
            finally
            {
                command.Dispose();

                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }


            return loans;
        }
        public Account getCorporateAccountInfo(string acct_no, string initiator_number)
        {
            List<Loan> loans = new List<Loan>();
            Account acct = null;
            AseConnection conn = null;
            AseCommand command = null;
            AseDataReader reader = null;
            StringBuilder s = new StringBuilder();
            StringBuilder output = new StringBuilder();
            string cnstring = ConfigurationManager.ConnectionStrings["phoenixConnectionString"].ConnectionString;

            string sqltext = "zsp_cust_information";

            //string sqltext = "select title_1,acct_no,acct_type,a.rsm_id as 'AccountRSMID'";
            //sqltext += ",(select name from phoenix..ad_gb_rsm where employee_id = a.rsm_id) as 'AccountRSMName'";
            //sqltext += ",b.rsm_id as 'RIMRSMID'";
            //sqltext += ",(select name from phoenix..ad_gb_rsm where employee_id = b.rsm_id) as 'RIMRSMName'";
            //sqltext += ",(select user_name from phoenix..ad_gb_rsm where employee_id = b.rsm_id) as 'RIMUSerName'";
            //sqltext += ",(select d.staff_id from phoenix..ad_gb_rsm c, zib_applications_users d where c.user_name = d.user_id and ";
            //sqltext += "employee_id = b.rsm_id) as 'StaffNo' ";
            //sqltext += "from phoenix..dp_acct a, phoenix..rm_acct b ";
            //sqltext += "where a.acct_no = @corperateacct_no ";// -----'1020041488'
            //sqltext += "and a.rim_no = b.rim_no ";           

            try
            {
                using (conn = new AseConnection(cnstring))
                {
                    conn.Open();

                    using (command = new AseCommand(sqltext, conn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@psCorperateacct_no", acct_no);
                        command.Parameters.AddWithValue("@initiator_staffnumber", initiator_number);
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            acct                        = new Account();
                            acct.account_name           = reader["title_1"].ToString();
                            acct.acct_no                = reader["acct_no"].ToString();
                            acct.acct_type              = reader["acct_type"].ToString();
                            acct.domicile_branch_no     = reader["BranchNo"].ToString();
                            acct.domicile_branch_name   = reader["BranchName"].ToString();
                            acct.rim_no                 = reader["RIMNO"].ToString();
                            acct.rsm_id                 = reader["AccountRSMID"].ToString();
                            acct.rsm_name               = reader["AccountRSMName"].ToString();
                            acct.rimrsm_id              = reader["RIMRSMID"].ToString();
                            acct.rimrsm_name            = reader["RIMRSMName"].ToString();
                            acct.rimrsm_username        = reader["RIMUSerName"].ToString();
                            acct.rsm_staffno            = reader["StaffNo"].ToString();    
                            
                            //New properties to be returned//
                            acct.sex                = reader["sex"].ToString();
                            //acct.marital_status   = reader["familystatus"].ToString();
                            acct.dob                = reader["date_of_birth"].ToString();
                            acct.home_address       = reader["home_address"].ToString();
                            acct.state_of_origin    = reader["state_of_origin"].ToString();
                            acct.phonenumber        = reader["phone_number"].ToString();
                            acct.email              = reader["email_address"].ToString();
                            acct.next_of_kin        = reader["next_of_kin"].ToString();
                            
                            //New property to be returned - bvn - 03 NOV 2015//
                            acct.bvn                = reader["bvn"].ToString();
                            acct.acct_type_desc     = reader["acct_type_desc"].ToString();

                            //New property to be returned - bvn - 25 OCT 2016//
                            acct.initiator_branch               = reader["initiator_branch"].ToString();
                            acct.initiator_branchcode           = reader["initiator_branchcode"].ToString();
                            acct.initiator_phoenix_username     = reader["initiator_phoenix_username"].ToString();
                            acct.initiator_phoenix_employee_id  = reader["initiator_phoenix_employee_id"].ToString();
                            acct.initiator_phoenix_status       = reader["initiator_phoenix_status"].ToString();
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                command.Dispose();

                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }


            return acct;
        }
        public GlobalLimit getRimInfo(string schemerimno, string schemeclasscode)
        {           
            GlobalLimit gLimit = null;
            AseConnection conn = null;
            AseCommand command = null;
            AseDataReader reader = null;
            StringBuilder s = new StringBuilder();
            StringBuilder output = new StringBuilder();
            string cnstring = ConfigurationManager.ConnectionStrings["phoenixConnectionString"].ConnectionString;

            string sqltext = "zsp_loans_GlobalLimit";
            //string sqltext = "select sum(amt) as 'GlobalLimit'";
            //sqltext += ",sum(undisbursed) as 'GlobalBalance'";
            //sqltext += ",rsm_id as 'RIMRSMID' ";
            //sqltext += "from phoenix..ln_umb ";
            //sqltext += "where rim_no = @SchemeRimNo ";
            //sqltext += "and class_code = @SchemeClassCode ";
            //sqltext += "and status = 'Active' ";            

            try
            {
                using (conn = new AseConnection(cnstring))
                {
                    conn.Open();

                    using (command = new AseCommand(sqltext, conn))
                    {
                        //command.Parameters.AddWithValue("@SchemeRimNo", int.Parse(schemerimno));
                        //command.Parameters.AddWithValue("@SchemeClassCode", int.Parse(schemeclasscode));
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@pnSchemeRimNo", int.Parse(schemerimno));
                        command.Parameters.AddWithValue("@pnSchemeClassCode", int.Parse(schemeclasscode));
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            gLimit = new GlobalLimit();
                            gLimit.Limit = reader["GlobalLimit"].ToString();
                            gLimit.GlobalBalance = reader["GlobalBalance"].ToString();                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                command.Dispose();

                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }


            return gLimit;
        }

        public List<Branch> GetBranchList()
        {
            List<Branch> brList = new List<Branch>();
            AseCommand cmd = null;
            AseConnection conn = null;
            string cnstring = ConfigurationManager.ConnectionStrings["phoenixConnectionString"].ConnectionString;

            try
            {
                conn = new AseConnection(cnstring);
                string sqlquery = "select branch_no,name_1 from phoenix..ad_gb_branch where status = 'Active' order by name_1";

                cmd = new AseCommand(sqlquery, conn);
                cmd.CommandTimeout = 0;
                conn.Open();
                AseDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    Branch br = new Branch();
                    br.BranchNo = Convert.ToInt16(reader["branch_no"].ToString());
                    br.BranchName = reader["name_1"].ToString();
                    brList.Add(br);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd.Dispose();

                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            return brList;
        }
    }
}
