

IF OBJECT_ID('dbo.zsp_cust_information') IS NOT NULL
    BEGIN
        DROP PROCEDURE dbo.zsp_cust_information
        IF OBJECT_ID('dbo.zsp_cust_information') IS NOT NULL
            PRINT '<<< FAILED DROPPING PROCEDURE dbo.zsp_cust_information >>>'
        ELSE
            PRINT '<<< DROPPED PROCEDURE dbo.zsp_cust_information >>>'
    END
GO
    
    CREATE PROC dbo.zsp_cust_information
        @psCorperateacct_no     VARCHAR(20),
        @initiator_staffnumber  VARCHAR(20)
    AS
        DECLARE
            
            @class_code     SMALLINT

        SELECT @class_code = class_code 
        FROM phoenix..dp_acct 
        WHERE acct_no=@psCorperateacct_no

        IF @class_code=101
            BEGIN
                SELECT top 1
                   a.title_1 ,
                   a.acct_no ,
                   a.acct_type ,
                   d.name_1 As 'BranchName',
				   convert(varchar, d.branch_no) As 'BranchNo',
                   a.rsm_id AS 'AccountRSMID',
                   (SELECT name FROM phoenix..ad_gb_rsm WHERE employee_id = a.rsm_id ) AS 'AccountRSMName' ,
                   b.rsm_id AS 'RIMRSMID' ,
                   b.rim_no AS 'RIMNO' ,
                   (SELECT name FROM phoenix..ad_gb_rsm WHERE employee_id = b.rsm_id ) AS 'RIMRSMName' ,
                   (SELECT user_name FROM phoenix..ad_gb_rsm WHERE employee_id = b.rsm_id )AS 'RIMUSerName' ,
                   (SELECT d.staff_id FROM phoenix..ad_gb_rsm c , zib_applications_users d WHERE c.user_name = d.user_id AND employee_id = b.rsm_id )AS 'StaffNo',

                    --NEW FIELDS
                    k.sex,
                    convert(varchar,k.date_of_birth,106) as date_of_birth,
                    isnull(k.home_address,'NA') as home_address,
                    isnull(k.state_of_origin,'NA') as state_of_origin,
                    (isnull(k.mobile_no,'') +', '+ isnull(k.telephone_no,''))as phone_number,
                    (isnull(k.email_address1,'') +', '+ isnull(k.email_address2,'')+', '+ isnull(k.email_address3,'')+', '+ isnull(k.email_address4,''))as email_address,
                    isnull(b.nxt_kin_name,'NA') as next_of_kin,
                    isnull(kc.bvn,'NA') as bvn,
                    k.acct_holder_type as acct_type_desc,
                    --END NEW FIELDS

                    --NEW MODIFICATION-GET THE INITIATOR'S CURRENT WORKING BRANCH
                    br.short_name as initiator_branch, 
                    RIGHT('000'+CAST(ar.branch_no AS VARCHAR(3)),3) as initiator_branchcode, 
                    ar.user_name  as initiator_phoenix_username, 
                    CONVERT(VARCHAR, ar.employee_id) AS initiator_phoenix_employee_id, 
                    zu.status as initiator_phoenix_status


                FROM    phoenix..dp_acct a ,
                        phoenix..rm_acct b ,
                        phoenix..ad_gb_branch d,
                        zenbase..zib_kyc_cust_information k,
                        zenbase..zib_kyc_corporate_signatories kc,

                        --NEW MODIFICATION-GET THE INITIATOR'S CURRENT WORKING BRANCH
                        phoenix..ad_gb_branch br, 
                        zenbase..zib_applications_users zu,
                        phoenix..ad_gb_rsm ar

                WHERE   a.acct_no =  @psCorperateacct_no  -----'1020041488'
                AND     a.rim_no = b.rim_no
                AND     k.rim_no = a.rim_no
                AND     k.acct_no = a.acct_no
                AND     kc.acct_no=a.acct_no
                AND     a.branch_no = d.branch_no

                        --NEW MODIFICATION-GET THE INITIATOR'S CURRENT WORKING BRANCH
                AND     ar.user_name = zu.user_id
                AND     ar.branch_no = br.branch_no
                AND     zu.staff_id  = @initiator_staffnumber

            RETURN 0
        END
    ELSE
        BEGIN
            SELECT top 1
                   a.title_1 ,
                   a.acct_no ,
                   a.acct_type ,
                   d.name_1 As 'BranchName',
				   convert(varchar, d.branch_no) As 'BranchNo',
                   a.rsm_id AS 'AccountRSMID',
                   (SELECT name FROM phoenix..ad_gb_rsm WHERE employee_id = a.rsm_id ) AS 'AccountRSMName' ,
                   b.rsm_id AS 'RIMRSMID' ,
                   b.rim_no AS 'RIMNO' ,
                   (SELECT name FROM phoenix..ad_gb_rsm WHERE employee_id = b.rsm_id ) AS 'RIMRSMName' ,
                   (SELECT user_name FROM phoenix..ad_gb_rsm WHERE employee_id = b.rsm_id )AS 'RIMUSerName' ,
                   (SELECT d.staff_id FROM phoenix..ad_gb_rsm c , zib_applications_users d WHERE c.user_name = d.user_id AND employee_id = b.rsm_id )AS 'StaffNo',

                    --NEW FIELDS
                    k.sex,
                    convert(varchar,k.date_of_birth,106) as date_of_birth,
                    isnull(k.home_address,'NA') as home_address,
                    isnull(k.state_of_origin,'NA') as state_of_origin,
                    (isnull(k.mobile_no,'') +', '+ isnull(k.telephone_no,''))as phone_number,
                    (isnull(k.email_address1,'') +', '+ isnull(k.email_address2,'')+', '+ isnull(k.email_address3,'')+', '+ isnull(k.email_address4,''))as email_address,
                    isnull(b.nxt_kin_name,'NA') as next_of_kin,
                    isnull(k.bvn,'NA') as bvn,
                    k.acct_holder_type as acct_type_desc,
                        --END NEW FIELDS

                    --NEW MODIFICATION-GET THE INITIATOR'S CURRENT WORKING BRANCH
                    br.short_name as initiator_branch, 
                    RIGHT('000'+CAST(ar.branch_no AS VARCHAR(3)),3) as initiator_branchcode, 
                    ar.user_name  as initiator_phoenix_username, 
                    CONVERT(VARCHAR, ar.employee_id) AS initiator_phoenix_employee_id,
                    zu.status as initiator_phoenix_status

                FROM    phoenix..dp_acct a ,
                        phoenix..rm_acct b ,
                        phoenix..ad_gb_branch d,
                        zenbase..zib_kyc_cust_information k,
                        
                        --NEW MODIFICATION-GET THE INITIATOR'S CURRENT WORKING BRANCH
                        phoenix..ad_gb_branch br,  
                        zenbase..zib_applications_users zu,
                        phoenix..ad_gb_rsm ar

                WHERE   a.acct_no = @psCorperateacct_no
                    AND a.rim_no = b.rim_no
                    AND k.rim_no = a.rim_no
                    AND k.acct_no = a.acct_no
                    AND  a.branch_no = d.branch_no

                        --NEW MODIFICATION-GET THE INITIATOR'S CURRENT WORKING BRANCH
                    AND ar.user_name = zu.user_id
                    AND ar.branch_no = br.branch_no
                    AND zu.staff_id  = @initiator_staffnumber
                RETURN 0
        END
go
GRANT EXECUTE ON dbo.zsp_cust_information TO domino_user
go
GRANT EXECUTE ON dbo.zsp_cust_information TO domino_user
go

IF OBJECT_ID('dbo.zsp_cust_information') IS NOT NULL
    PRINT '<<< CREATED PROCEDURE dbo.zsp_cust_information >>>'
ELSE
    PRINT '<<< FAILED CREATING PROCEDURE dbo.zsp_cust_information >>>'
go
