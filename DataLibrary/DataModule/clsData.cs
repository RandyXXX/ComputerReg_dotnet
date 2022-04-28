using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DataLibrary
{

    class clsData
    {
        private string strCN1;
        PUB_LIB pub_lib;
     
        /// <summary>
        ///     ''' 建構子，告知連線字串
        ///     ''' </summary>
        ///     ''' <param name="strCn">連線字串</param>
        ///     ''' <remarks></remarks>
        public clsData(string strCn)
        {
            strCN1 = strCn;
            pub_lib = new PUB_LIB(strCN1);

          
        }
        #region RunSQL
        public DataTable RunSQL(string strSQL)
        {
            return pub_lib.Query(strSQL).Tables[0];


        }

        #endregion
        #region GHR
        public DataTable QueryCompany(string strCountryID)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT DISTINCT a.Company_ID,a.Company_Name_Cht FROM [GHR].[dbo].[vw_MKT_Company] a,[GHR].[dbo].[vw_MKT_SITE] b,vw_address_book2 c WHERE a.Company_ID IS NOT NULL  AND a.Company_Name_Cht <>'' AND a.Company_ID=b.Company_ID AND a.Company_ID=c.Company_ID  AND b.Country_ID=@Country_ID";

            V_arr[0, 0] = "@Country_ID";
            V_arr[0, 1] = strCountryID;
            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public string GetCompany_ID(string strEmpno, string strName)
        {
            string strSQL;
            string[,] V_arr = new string[2, 2];
            strSQL = "SELECT Company_ID FROM vw_address_book2 WHERE Main_User=@Main_User AND USER_NAME=@E_MAIL1";
            V_arr[0, 0] = "@Main_user";
            V_arr[0, 1] = strEmpno;
            V_arr[1, 0] = "@E_MAIL1";
            V_arr[1, 1] = strName;

            return pub_lib.QueryFirstRec(strSQL, V_arr);
        }
        public DataTable GetUserInfo(string strAD, string strUserName)
        {
            string strSQL;
            string[,] V_arr = new string[2, 2];
            strSQL = "Select a.Main_User USER_ID,a.USER_NAME,a.ALIAS_NAME,a.DEPT_ID,a.USER_SITE, " + " a.ROLE_ID, a.LANG, a.CHK_FLAG, a.E_MAIL1, a.ACCOUNT_TYPE," + " a.IS_NEED_LOG, a.DATA_REGION, a.DATA_GROUP, a.DATA_SITE, a.COMPANY_ID, " + " b.SITE_NAME_CHT, b.MANAGER_ID, b.IS_MANAGER_SITE, b.INV_PHASE_GROUP, b.PARENT_SITE, " + " c.COMPANY_NAME_CHT, c.CURRENCY_ID, c.HEAD_OFFICE_SITEID, c.PARENT_COMPANY_ID, d.CURRENCY_ID, " + " e.ROLE_NAME_CHT, f.RESOURCE_CHT" + " From AS_USER a " + "     Left join AS_SITE b on a.USER_SITE = b.SITE_ID and a.COMPANY_ID = b.COMPANY_ID and b.CHK_FLAG='Y' " + "     Left join AS_COMPANY c on a.COMPANY_ID = c.COMPANY_ID " + "     Left join AS_CURRENCY d on c.CURRENCY_ID = d.CURRENCY_ID " + "     Left join AS_ROLE e on a.ROLE_ID = e.ROLE_ID " + "     Left join AS_RESOURCE f on a.ACCOUNT_TYPE = f.RESOURCE_NAME and f.RESOURCE_PROGID='AccountType' " + " Where (a.AD_ACCOUNT) = @strUser AND a.AD_DOMAIN_NAME=@strCompanyID" + "    and a.CHK_FLAG='Y' and c.CHK_FLAG='Y' and e.CHK_FLAG='Y'  " + " Order By a.CREATE_DATE Desc ";
            V_arr[0, 0] = "@strUser";
            V_arr[0, 1] = strUserName;
            V_arr[1, 0] = "@strCompanyID";
            V_arr[1, 1] = strAD;

            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public DataTable GetUserInfoByEmpnoEqual(string strEmpno)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "Select a.Main_User USER_ID,a.USER_NAME,a.ALIAS_NAME,a.DEPT_ID,a.USER_SITE, " + " a.ROLE_ID, a.LANG, a.CHK_FLAG, a.E_MAIL1, a.ACCOUNT_TYPE," + " a.IS_NEED_LOG, a.DATA_REGION, a.DATA_GROUP, a.DATA_SITE, a.COMPANY_ID, " + " b.SITE_NAME_CHT,b.SITE_NAME_CHS, b.MANAGER_ID, b.IS_MANAGER_SITE, b.INV_PHASE_GROUP, b.PARENT_SITE, " + " c.COMPANY_NAME_CHT,c.COMPANY_NAME_CHS, c.CURRENCY_ID, c.HEAD_OFFICE_SITEID, c.PARENT_COMPANY_ID, d.CURRENCY_ID, " 
                + " e.ROLE_NAME_CHT, f.RESOURCE_CHT,g.OFFICE_TEL_EXT,h.BU_CODE_ID,g.AD_DOMAIN_NAME" 
                + " From AS_USER a " 
                + "     Left join AS_SITE b on a.USER_SITE = b.SITE_ID and a.COMPANY_ID = b.COMPANY_ID and b.CHK_FLAG='Y' " 
                + "     Left join AS_COMPANY c on a.COMPANY_ID = c.COMPANY_ID " 
                + "     Left join AS_CURRENCY d on c.CURRENCY_ID = d.CURRENCY_ID " 
                + "     Left join AS_ROLE e on a.ROLE_ID = e.ROLE_ID " 
                + "     Left join AS_RESOURCE f on a.ACCOUNT_TYPE = f.RESOURCE_NAME and f.RESOURCE_PROGID='AccountType' " 
                + "     INNER JOIN vw_ADDRESS_BOOK2 g ON a.USER_ID=g.USER_ID " 
                + "     LEFT JOIN vw_getDeptTree h ON a.DEPT_ID =h.DEPT_ID  AND c.COMPANY_ID =h.COMPANY_ID " 
                + " Where (a.USER_ID   = @strUser  OR a.Main_User = @strUser  ) " 
                + "    and a.CHK_FLAG='Y' and c.CHK_FLAG='Y' and e.CHK_FLAG='Y' " 
                + " Order By a.CREATE_DATE Desc ";


            strSQL = "SELECT * FROM vw_Address_Book_Assets WHERE (USER_ID   = @strUser  OR Main_User = @strUser  ) ";
            V_arr[0, 0] = "@strUser";
            V_arr[0, 1] = strEmpno;


            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public DataTable GetUserInfoByEmpno(string strEmpno)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "Select a.Main_User USER_ID,a.USER_NAME,a.ALIAS_NAME,a.DEPT_ID,a.USER_SITE, " + " a.ROLE_ID, a.LANG, a.CHK_FLAG, a.E_MAIL1, a.ACCOUNT_TYPE," + " a.IS_NEED_LOG, a.DATA_REGION, a.DATA_GROUP, a.DATA_SITE, a.COMPANY_ID, " + " b.SITE_NAME_CHT,b.SITE_NAME_CHS, b.MANAGER_ID, b.IS_MANAGER_SITE, b.INV_PHASE_GROUP, b.PARENT_SITE, " + " c.COMPANY_NAME_CHT,c.COMPANY_NAME_CHS, c.CURRENCY_ID, c.HEAD_OFFICE_SITEID, c.PARENT_COMPANY_ID, d.CURRENCY_ID, " + " e.ROLE_NAME_CHT, f.RESOURCE_CHT,g.OFFICE_TEL_EXT,h.BU_CODE_ID,g.AD_DOMAIN_NAME" + " From AS_USER a " + "     Left join AS_SITE b on a.USER_SITE = b.SITE_ID and a.COMPANY_ID = b.COMPANY_ID and b.CHK_FLAG='Y' " + "     Left join AS_COMPANY c on a.COMPANY_ID = c.COMPANY_ID " + "     Left join AS_CURRENCY d on c.CURRENCY_ID = d.CURRENCY_ID " + "     Left join AS_ROLE e on a.ROLE_ID = e.ROLE_ID " + "     Left join AS_RESOURCE f on a.ACCOUNT_TYPE = f.RESOURCE_NAME and f.RESOURCE_PROGID='AccountType' " + "     INNER JOIN vw_ADDRESS_BOOK2 g ON a.USER_ID=g.USER_ID " + "     LEFT JOIN vw_getDeptTree h ON a.DEPT_ID =h.DEPT_ID  AND c.COMPANY_ID =h.COMPANY_ID " + " Where (a.USER_ID = @strUser OR a.Main_User = @strUser  ) " + "    " + "    and a.CHK_FLAG='Y' and c.CHK_FLAG='Y' and e.CHK_FLAG='Y' " + " Order By a.CREATE_DATE Desc ";
            strSQL = "SELECT * FROM vw_address_book  Where (USER_ID = @strUser OR Main_User = @strUser  ) ";
            V_arr[0, 0] = "@strUser";
            V_arr[0, 1] = strEmpno;


            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public DataTable GetUserInfoByEmpnoLike(string strEmpno)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];

            strSQL = "Select TOP(20) Main_User USER_ID, USER_NAME, USER_NAME_ENG, POSITION_ID, DEPT_ID, DEPT_NAME_CHT, SITE_ID, " +
                "   SITE_NAME_CHT, COUNTRY_ID, COUNTRY_NAME_CHT, COMPANY_ID, COMPANY_NAME_CHT, OFFICE_TEL_EXT, " +
                 "  OFFICE_TEL_EXT2, CELL_PHONE1, CELL_PHONE2, FLOOR, E_MAIL1, CHK_FLAG, RANK, " +
                 "  AD_DOMAIN_NAME, AD_ACCOUNT, SOURCE, Top_Dept, CUST_ID, AD_ACCOUNT2, COMPANY_NAME_ENG, Sex, " +
                 "  ONBOARD_DATE" +
                " FROM      vw_ADDRESS_BOOK " +
                " Where (USER_ID LIKE @strUser + '%' OR Main_User LIKE @strUser + '%'  ) ";

            strSQL = "Select TOP(20) * " +
                "   " +
               " FROM      vw_ADDRESS_BOOK " +
               " Where (USER_ID LIKE @strUser + '%' OR Main_User LIKE @strUser + '%'  ) ";
            /*strSQL = "Select TOP(20) a.Main_User USER_ID,a.USER_NAME,a.ALIAS_NAME,a.DEPT_ID,a.USER_SITE, " + 
                " a.ROLE_ID, a.LANG, a.CHK_FLAG, a.E_MAIL1, a.ACCOUNT_TYPE," + 
                " a.IS_NEED_LOG, a.DATA_REGION, a.DATA_GROUP, a.DATA_SITE, a.COMPANY_ID, " + 
                " b.SITE_NAME_CHT,b.SITE_NAME_CHS, b.MANAGER_ID, b.IS_MANAGER_SITE, b.INV_PHASE_GROUP, b.PARENT_SITE, " + 
                " c.COMPANY_NAME_CHT,c.COMPANY_NAME_CHS, c.CURRENCY_ID, c.HEAD_OFFICE_SITEID, c.PARENT_COMPANY_ID, d.CURRENCY_ID, " + 
                " e.ROLE_NAME_CHT, f.RESOURCE_CHT,g.OFFICE_TEL_EXT,h.BU_CODE_ID,g.AD_DOMAIN_NAME" + 
                " From AS_USER a " + 
                "     Left join AS_SITE b on a.USER_SITE = b.SITE_ID and a.COMPANY_ID = b.COMPANY_ID and b.CHK_FLAG='Y' " + 
                "     Left join AS_COMPANY c on a.COMPANY_ID = c.COMPANY_ID " + 
                "     Left join AS_CURRENCY d on c.CURRENCY_ID = d.CURRENCY_ID " + 
                "     Left join AS_ROLE e on a.ROLE_ID = e.ROLE_ID " + 
                "     Left join AS_RESOURCE f on a.ACCOUNT_TYPE = f.RESOURCE_NAME and f.RESOURCE_PROGID='AccountType' " + 
                "     INNER JOIN vw_ADDRESS_BOOK2 g ON a.USER_ID=g.USER_ID " + 
                "     LEFT JOIN vw_getDeptTree h ON a.DEPT_ID =h.DEPT_ID  AND c.COMPANY_ID =h.COMPANY_ID " + 
                " Where (a.USER_ID LIKE @strUser + '%' OR a.Main_User LIKE @strUser + '%'  ) " + 
                "    " + 
                "    and a.CHK_FLAG='Y' and c.CHK_FLAG='Y' and e.CHK_FLAG='Y' " + 
                " Order By a.CREATE_DATE Desc ";*/
            V_arr[0, 0] = "@strUser";
            V_arr[0, 1] = strEmpno;


            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public DataTable GetUserInfoByEmpno(string strEmpno, string strCompany_ID)
        {
            string strSQL;
            string[,] V_arr = new string[2, 2];
            strSQL = "Select a.Main_User USER_ID,a.USER_NAME,a.ALIAS_NAME,a.DEPT_ID,a.USER_SITE, " + " a.ROLE_ID, a.LANG, a.CHK_FLAG, a.E_MAIL1, a.ACCOUNT_TYPE," + " a.IS_NEED_LOG, a.DATA_REGION, a.DATA_GROUP, a.DATA_SITE, a.COMPANY_ID, " + " b.SITE_NAME_CHT,b.SITE_NAME_CHS, b.MANAGER_ID, b.IS_MANAGER_SITE, b.INV_PHASE_GROUP, b.PARENT_SITE, " + " c.COMPANY_NAME_CHT,c.COMPANY_NAME_CHS, c.CURRENCY_ID, c.HEAD_OFFICE_SITEID, c.PARENT_COMPANY_ID, d.CURRENCY_ID, " + " e.ROLE_NAME_CHT, f.RESOURCE_CHT,g.OFFICE_TEL_EXT,h.BU_CODE_ID,g.AD_DOMAIN_NAME" + " From AS_USER a " + "     Left join AS_SITE b on a.USER_SITE = b.SITE_ID and a.COMPANY_ID = b.COMPANY_ID and b.CHK_FLAG='Y' " + "     Left join AS_COMPANY c on a.COMPANY_ID = c.COMPANY_ID " + "     Left join AS_CURRENCY d on c.CURRENCY_ID = d.CURRENCY_ID " + "     Left join AS_ROLE e on a.ROLE_ID = e.ROLE_ID " + "     Left join AS_RESOURCE f on a.ACCOUNT_TYPE = f.RESOURCE_NAME and f.RESOURCE_PROGID='AccountType' " + "     INNER JOIN vw_ADDRESS_BOOK2 g ON a.USER_ID=g.USER_ID " + "     LEFT JOIN vw_getDeptTree h ON a.DEPT_ID =h.DEPT_ID  AND c.COMPANY_ID =h.COMPANY_ID " + " Where (a.USER_ID = @strUser OR a.Main_User = @strUser  ) " + "   AND a.Company_ID=@Company_ID " + "    and a.CHK_FLAG='Y' and c.CHK_FLAG='Y' and e.CHK_FLAG='Y' " + " Order By a.CREATE_DATE Desc ";
            V_arr[0, 0] = "@strUser";
            V_arr[0, 1] = strEmpno;

            V_arr[1, 0] = "@Company_ID";
            V_arr[1, 1] = strCompany_ID;
            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public DataTable GetUserInfoByName(string strEmpno)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];

            strSQL = "Select TOP(20) Main_User USER_ID, USER_NAME, USER_NAME_ENG, POSITION_ID, DEPT_ID, DEPT_NAME_CHT, SITE_ID, " +
               "   SITE_NAME_CHT, COUNTRY_ID, COUNTRY_NAME_CHT, COMPANY_ID, COMPANY_NAME_CHT, OFFICE_TEL_EXT, " +
                "  OFFICE_TEL_EXT2, CELL_PHONE1, CELL_PHONE2, FLOOR, E_MAIL1, CHK_FLAG, RANK, " +
                "  AD_DOMAIN_NAME, AD_ACCOUNT, SOURCE, Top_Dept, CUST_ID, AD_ACCOUNT2, COMPANY_NAME_ENG, Sex, " +
                "  ONBOARD_DATE" +
               " FROM      vw_ADDRESS_BOOK " +
               " Where (USER_NAME LIKE  '%' + @strUser+'%'  OR  USER_NAME_ENG LIKE '%' + @strUser + '%' ) ";

            strSQL = "Select TOP(20) * " +
              "  " +
             " FROM      vw_ADDRESS_BOOK_Assets " +
             " Where (USER_NAME LIKE  '%' + @strUser+'%'  OR  USER_NAME_ENG LIKE '%' + @strUser + '%' ) ";
            //strSQL = "Select TOP(20) a.Main_User USER_ID,a.USER_NAME,a.ALIAS_NAME,a.DEPT_ID,a.USER_SITE, " + 
            //    " a.ROLE_ID, a.LANG, a.CHK_FLAG, a.E_MAIL1, a.ACCOUNT_TYPE," + 
            //    " a.IS_NEED_LOG, a.DATA_REGION, a.DATA_GROUP, a.DATA_SITE, a.COMPANY_ID, " + 
            //    " b.SITE_NAME_CHT,b.SITE_NAME_CHS, b.MANAGER_ID, b.IS_MANAGER_SITE, b.INV_PHASE_GROUP, b.PARENT_SITE, " + 
            //    " c.COMPANY_NAME_CHT,c.COMPANY_NAME_CHS, c.CURRENCY_ID, c.HEAD_OFFICE_SITEID, c.PARENT_COMPANY_ID, d.CURRENCY_ID, " + 
            //    " e.ROLE_NAME_CHT, f.RESOURCE_CHT,g.OFFICE_TEL_EXT,h.BU_CODE_ID,g.AD_DOMAIN_NAME" + 
            //    " From AS_USER a " + 
            //    "     INNER JOIN vw_ADDRESS_BOOK2 g ON a.USER_ID=g.USER_ID " + 
            //    "     Left join AS_SITE b on a.USER_SITE = b.SITE_ID and a.COMPANY_ID = b.COMPANY_ID and b.CHK_FLAG='Y' " +
            //    "     Left join AS_COMPANY c on a.COMPANY_ID = c.COMPANY_ID " + 
            //    "     Left join AS_CURRENCY d on c.CURRENCY_ID = d.CURRENCY_ID " + 
            //    "     Left join AS_ROLE e on a.ROLE_ID = e.ROLE_ID " + 
            //    "     Left join AS_RESOURCE f on a.ACCOUNT_TYPE = f.RESOURCE_NAME and f.RESOURCE_PROGID='AccountType' " + 
            //    "     LEFT JOIN vw_getDeptTree h ON a.DEPT_ID =h.DEPT_ID  AND c.COMPANY_ID =h.COMPANY_ID " + 
            //    " Where (a.USER_NAME LIKE  '%' + @strUser+'%'  ) " + 
            //    "    and a.CHK_FLAG='Y' and c.CHK_FLAG='Y' and e.CHK_FLAG='Y' " + 
            //    " Order By a.CREATE_DATE Desc ";

            // strSQL = "SELECT * FROM vw_ADDRESS_BOOK2 WHERE USER_NAME LIKE  '%' + @strUser+'%'"

            V_arr[0, 0] = "@strUser";
            V_arr[0, 1] = strEmpno;


            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public DataTable GetUserInfoByEmail(string strEmail)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];

            strSQL = "Select TOP(20) Main_User USER_ID, USER_NAME, USER_NAME_ENG, POSITION_ID, DEPT_ID, DEPT_NAME_CHT, SITE_ID, " +
               "   SITE_NAME_CHT, COUNTRY_ID, COUNTRY_NAME_CHT, COMPANY_ID, COMPANY_NAME_CHT, OFFICE_TEL_EXT, " +
                "  OFFICE_TEL_EXT2, CELL_PHONE1, CELL_PHONE2, FLOOR, E_MAIL1, CHK_FLAG, RANK, " +
                "  AD_DOMAIN_NAME, AD_ACCOUNT, SOURCE, Top_Dept, CUST_ID, AD_ACCOUNT2, COMPANY_NAME_ENG, Sex, " +
                "  ONBOARD_DATE" +
               " FROM      vw_ADDRESS_BOOK " +
               " Where (E_MAIL1 LIKE  '%' + @strEmail+'%'  ) ";

            strSQL = "Select TOP(20) * " +
               " FROM      vw_ADDRESS_BOOK_Assets " +
               " Where (E_MAIL1 LIKE  '%' + @strEmail+'%'  ) ";
            //strSQL = "Select TOP(20) a.Main_User USER_ID,a.USER_NAME,a.ALIAS_NAME,a.DEPT_ID,a.USER_SITE, " + 
            //    " a.ROLE_ID, a.LANG, a.CHK_FLAG, a.E_MAIL1, a.ACCOUNT_TYPE," + 
            //    " a.IS_NEED_LOG, a.DATA_REGION, a.DATA_GROUP, a.DATA_SITE, a.COMPANY_ID, " + 
            //    " b.SITE_NAME_CHT,b.SITE_NAME_CHS, b.MANAGER_ID, b.IS_MANAGER_SITE, b.INV_PHASE_GROUP, b.PARENT_SITE, " + 
            //    " c.COMPANY_NAME_CHT,c.COMPANY_NAME_CHS, c.CURRENCY_ID, c.HEAD_OFFICE_SITEID, c.PARENT_COMPANY_ID, d.CURRENCY_ID, " + 
            //    " e.ROLE_NAME_CHT, f.RESOURCE_CHT,g.OFFICE_TEL_EXT,h.BU_CODE_ID,g.AD_DOMAIN_NAME" + 
            //    " From AS_USER a " + 
            //    "     INNER JOIN vw_ADDRESS_BOOK2 g ON a.USER_ID=g.USER_ID " + 
            //    "     Left join AS_SITE b on a.USER_SITE = b.SITE_ID and a.COMPANY_ID = b.COMPANY_ID and b.CHK_FLAG='Y' " +
            //    "     Left join AS_COMPANY c on a.COMPANY_ID = c.COMPANY_ID " + 
            //    "     Left join AS_CURRENCY d on c.CURRENCY_ID = d.CURRENCY_ID " + 
            //    "     Left join AS_ROLE e on a.ROLE_ID = e.ROLE_ID " + 
            //    "     Left join AS_RESOURCE f on a.ACCOUNT_TYPE = f.RESOURCE_NAME and f.RESOURCE_PROGID='AccountType' " + 
            //    "     LEFT JOIN vw_getDeptTree h ON a.DEPT_ID =h.DEPT_ID  AND c.COMPANY_ID =h.COMPANY_ID " + 
            //    " Where (a.USER_NAME LIKE  '%' + @strUser+'%'  ) " + 
            //    "    and a.CHK_FLAG='Y' and c.CHK_FLAG='Y' and e.CHK_FLAG='Y' " + 
            //    " Order By a.CREATE_DATE Desc ";

            // strSQL = "SELECT * FROM vw_ADDRESS_BOOK2 WHERE USER_NAME LIKE  '%' + @strUser+'%'"

            V_arr[0, 0] = "@strEmail";
            V_arr[0, 1] = strEmail;


            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public DataTable QueryCompanyIDFromEmpno(string strMain_User)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT DISTINCT a.Company_ID,b.Company_Name_Cht,a.Main_User,a.User_Name FROM vw_address_book2 a,[GHR].dbo.[AS_Company] b WHERE a.Company_ID=b.Company_ID AND a.Main_user=@Main_user AND a.Layoff_Date IS NULL";
            V_arr[0, 0] = "@Main_user";
            V_arr[0, 1] = strMain_User;
            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public DataTable QueryCompanyIDFromEmpno2(string strMain_User)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT DISTINCT a.Company_ID,b.Company_Name_Cht,a.Main_User,a.User_Name FROM vw_address_book2 a,[GHR].dbo.[AS_Company] b WHERE a.Company_ID=b.Company_ID AND a.Main_user=@Main_user";
            V_arr[0, 0] = "@Main_user";
            V_arr[0, 1] = strMain_User;
            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        #endregion


        #region Menu
        public DataTable GetPrgInfo(string strFileName)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT a.*,b.PrgSerNo MenuID FROM  Category a,Category b WHERE a.PrgPath LIKE '%' + @strPrgName AND a.MasterNo=b.PrgSerNo";
            V_arr[0, 0] = "@strPrgName";
            V_arr[0, 1] = strFileName;

            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public string GetPrgTitle(string strPrgName)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT PrgName FROM  Category WHERE PrgPath LIKE '%' + @strPrgName ";
            V_arr[0, 0] = "@strPrgName";
            V_arr[0, 1] = strPrgName;

            return pub_lib.QueryFirstRec(strSQL, V_arr);
        }
        public string GetMenuTitle(string strPrgName)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT a.PrgName FROM  Category a,Category b WHERE b.PrgPath LIKE '%' + @strPrgName AND a.PrgSerNo=b.MasterNo";
            V_arr[0, 0] = "@strPrgName";
            V_arr[0, 1] = strPrgName;

            return pub_lib.QueryFirstRec(strSQL, V_arr);
        }
        public DataTable GetMenu(string strEmpno)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            DataSet ds = new DataSet();
            strSQL = "SELECT DISTINCT  * FROM (SELECT  CASE WHEN a.MasterNo=-1 THEN  a.SeqNo ELSE (SELECT aa.SeqNo FROM Category aa WHERE aa.PrgSerNo=a.MasterNo) END SeqNo ,a.PrgPath, a.CategoryDesc,a.PrgSerNo,a.PrgName,a.ICONURL,a.MasterNo,CASE WHEN a.MasterNo=-1 THEN 0 ELSE  a.SeqNo END RealSeq FROM" + "  Category            a INNER JOIN " + "  GroupToPrg   b ON a.PrgSerNo=b.PrgSerNo INNER JOIN " + "  UserGroup    c ON b.GROUPNO=c.GROUPNO INNER JOIN " + "   GroupToUser d ON c.GROUPNO=d.GROUPNO " + " WHERE 1=1  " + "  AND d.EMPNO=@empno " + " )BBB " + " ORDER BY SeqNo ,RealSeq";
            V_arr[0, 0] = "@empno";
            V_arr[0, 1] = strEmpno;
            ds = pub_lib.Query(strSQL, V_arr);
            return ds.Tables[0];
        }
        /// <summary>
        ///     ''' 取得左側Menu
        ///     ''' </summary>
        ///     ''' <param name="strUID" ></param>
        ///     ''' <param name="intMasterID"></param>
        ///     ''' <param name="strErrMsg"></param>  
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public DataTable GetMenu(string strAD, string strUID, int intMasterID, ref string strErrMsg)
        {
            DataSet ds = new DataSet();
            string strSQL;
            string[,] V_arr = new string[3, 2];
            strSQL = "SELECT distinct   a.SeqNo,a.PrgPath, a.CategoryDesc,a.PrgSerNo,a.PrgName,a.ICONURL FROM" + " Category            a INNER JOIN " + " GroupToPrg   b ON a.PrgSerNo=b.PrgSerNo INNER JOIN" + " UserGroup    c ON b.GROUPNO=c.GROUPNO INNER JOIN " + " GroupToUser d ON c.GROUPNO=d.GROUPNO " + " WHERE a.MasterNo=@MasterNo " + " AND d.EMPNO=@strUID " + "  " + " ORDER BY a.SeqNo ";
            V_arr[0, 0] = "@MasterNo";
            V_arr[0, 1] = intMasterID + "";
            V_arr[1, 0] = "@strUID";
            V_arr[1, 1] = strUID;
            V_arr[2, 0] = "@strAD";
            V_arr[2, 1] = strAD;
            ds = pub_lib.Query(strSQL, V_arr);
            return ds.Tables[0];
        }
        /// <summary>
        ///     ''' 取得左側Menu(不管使用者合不合法)
        ///     ''' </summary>
        ///     ''' <param name="intMasterID"></param>
        ///     ''' <param name="strErrMsg"></param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public DataTable GetMenu(int intMasterID, ref string strErrMsg)
        {
            DataSet ds = new DataSet();
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT a.PrgPath, a.CategoryDesc,a.PrgSerNo,a.PrgName,a.ICONURL FROM" + " Category            a  " + "  " + "  " + " WHERE a.MasterNo=@MasterNo " + "" + " ORDER BY a.SeqNo";
            V_arr[0, 0] = "@MasterNo";
            V_arr[0, 1] = intMasterID + "";
            ds = pub_lib.Query(strSQL, V_arr);
            return ds.Tables[0];
        }
        public DataTable GetMenu()
        {
            DataSet ds = new DataSet();
            string strSQL;
            string[,] V_arr = new string[2, 2];
            strSQL = "SELECT a.PrgPath, a.CategoryDesc,a.PrgSerNo,a.PrgName,a.ICONURL,a.SeqNo FROM" + " Category            a  " + " WHERE a.MASTERNO='-1' " + " ORDER BY a.SeqNo";

            ds = pub_lib.Query(strSQL);
            return ds.Tables[0];
        }

        public DataTable GetMenu(int intMasterID)
        {
            DataSet ds = new DataSet();
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT a.PRGSERNO,a.PrgPath, a.CategoryDesc,a.PrgSerNo,a.PrgName" + " ,b.PrgName AS MasterPrgName,a.ICONURL,a.SeqNo FROM" + " Category  a  INNER JOIN " + " Category  b ON a.MasterNo=b.PRGSERNO " + " WHERE a.MasterNo=@MasterNo " + " AND a.MASTERNO<>'-1' " + " ORDER BY a.SeqNo";
            V_arr[0, 0] = "@MasterNo";
            V_arr[0, 1] = intMasterID + "";
            ds = pub_lib.Query(strSQL, V_arr);
            return ds.Tables[0];
        }

        /// <summary>
        ///     ''' 登入
        ///     ''' </summary>
        ///     ''' <param name="strUserID">使用者名稱(員工編號)</param>
        ///     ''' <param name="strPasswd">使用者密碼</param>
        ///     ''' <param name="strErrMsg">錯誤訊息</param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public bool Login(string strUserID, string strPasswd, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[2, 2];
            int intCount = 0;
            strSQL = "SELECT COUNT(*)" + " FROM sec_usr_m" + " WHERE EMPNO=@strUserID " + " AND User_pass=@strUserPass";

            V_arr[0, 0] = "@strUserID";
            V_arr[0, 1] = strUserID;
            V_arr[1, 0] = "@strUserPass";
            V_arr[1, 1] = strPasswd;
            try
            {
                intCount = Convert.ToInt32(pub_lib.QueryFirstRec(strSQL, V_arr));
            }
            catch (Exception Ex)
            {

            }
            if (intCount > 0)
                return true;
            return false;
        }
        /// <summary>
        ///     ''' 登入
        ///     ''' </summary>
        ///     ''' <param name="strUserID">使用者名稱(員工編號)</param>
        ///     ''' <param name="strPasswd">使用者密碼</param>
        ///     ''' <param name="strErrMsg">錯誤訊息</param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public bool LoginAD(string strUserID, string strPasswd, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[2, 2];
            int intCount = 0;
            strSQL = "SELECT COUNT(*)" + " FROM sec_usr_m" + " WHERE USER_ID=@strUserID " + " AND USER_PASS=@strUserPass";

            V_arr[0, 0] = "@strUserID";
            V_arr[0, 1] = strUserID;
            V_arr[1, 0] = "@strUserPass";
            V_arr[1, 1] = strPasswd;

            try
            {
                intCount = 0;
                intCount = Convert.ToInt32(pub_lib.QueryFirstRec(strSQL, V_arr));
            }
            catch (Exception ex)
            {
                intCount = 0;
            }


            if (intCount > 0)
                return true;
            return false;
        }
        /// <summary>
        ///     ''' 找出AD設定的密碼
        ///     ''' </summary>
        ///     ''' <param name="strUserID"></param>
        ///     ''' <param name="strErrMsg"></param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public string GetPassFromAD(string strUserID, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];

            strSQL = "SELECT USER_PASS " + " FROM sec_usr_m" + " WHERE USER_ID=@strUserID " + "  ";

            V_arr[0, 0] = "@strUserID";
            V_arr[0, 1] = strUserID;

            return pub_lib.QueryFirstRec(strSQL, V_arr);
        }



        /// <summary>
        ///     ''' 新增目錄/程式
        ///     ''' </summary>
        ///     ''' <param name="intMasterID">目錄ID</param>
        ///     ''' <param name="strPrgName">目錄/程式名稱</param>
        ///     ''' <param name="strIcon">圖示路徑</param>
        ///     ''' <param name="strPrgPath">程式路徑</param>
        ///     ''' <param name="intSeqNo">順序</param>
        ///     ''' <param name="strPrgDesc">說明</param>
        ///     ''' <param name="strErrMsg">錯誤訊息</param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public bool AddCategory(int intMasterID, string strPrgName, string strIcon, string strPrgPath, int intSeqNo, string strPrgDesc, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[6, 2];
            // Dim intTmp As Integer
            // strSQL = "SELECT MAX(PRGSERNO) FROM " & _
            // " CATEGORY "
            // Try
            // intTmp = Oracle_pub_lib.QueryFirstRec(strSQL)
            // Catch ex As Exception
            // intTmp = 1
            // End Try
            // intTmp = intTmp + 1

            strSQL = "INSERT INTO CATEGORY(PRGNAME,PRGPATH" + ",CATEGORYDESC,MASTERNO,ICONURL,SeqNo) VALUES" + " (@strPRGNAME,@strPRGPATH,@strCATEGORYDESC," + "@strMASTERNO,@strICONURL,@intSeqNo)";

            V_arr[0, 0] = "@strPRGNAME";
            V_arr[0, 1] = strPrgName;
            V_arr[1, 0] = "@strPRGPATH";
            V_arr[1, 1] = strPrgPath;
            V_arr[2, 0] = "@strCATEGORYDESC";
            V_arr[2, 1] = strPrgDesc;
            V_arr[3, 0] = "@strMASTERNO";
            V_arr[3, 1] = intMasterID + "";

            V_arr[4, 0] = "@strICONURL";
            V_arr[4, 1] = strIcon;
            V_arr[5, 0] = "@intSeqNo";
            V_arr[5, 1] = intSeqNo + "";
            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        /// <summary>
        ///     ''' 修改目錄
        ///     ''' </summary>
        ///     ''' <param name="intPrgNo"></param>
        ///     ''' <param name="intMasterID"></param>
        ///     ''' <param name="strPrgName"></param>
        ///     ''' <param name="strIcon"></param>
        ///     ''' <param name="strPrgPath"></param>
        ///     ''' <param name="intSeqNo"></param>
        ///     ''' <param name="strPrgDesc"></param>
        ///     ''' <param name="strErrMsg"></param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public bool UpdateCategory(int intPrgNo, int intMasterID, string strPrgName, string strIcon, string strPrgPath, int intSeqNo, string strPrgDesc, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[7, 2];
            strSQL = "UPDATE CATEGORY SET PRGNAME=@strPRGNAME,PRGPATH=@strPRGPATH" + ",CATEGORYDESC=@strCATEGORYDESC,MASTERNO=@strMASTERNO,SEQNO=@strSEQNO," + "ICONURL=@strICONURL " + " WHERE PRGSERNO=@strPRGSERNO";

            V_arr[0, 0] = "@strPRGNAME";
            V_arr[0, 1] = strPrgName;
            V_arr[1, 0] = "@strPRGPATH";
            V_arr[1, 1] = strPrgPath;
            V_arr[2, 0] = "@strCATEGORYDESC";
            V_arr[2, 1] = strPrgDesc;
            V_arr[3, 0] = "@strMASTERNO";
            V_arr[3, 1] = intMasterID + "";
            V_arr[4, 0] = "@strSEQNO";
            V_arr[4, 1] = intSeqNo + "";
            V_arr[5, 0] = "@strICONURL";
            V_arr[5, 1] = strIcon;

            V_arr[6, 0] = "@strPRGSERNO";
            V_arr[6, 1] = intPrgNo + "";
            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        /// <summary>
        ///     ''' 刪除程式，並且刪除關係
        ///     ''' </summary>
        ///     ''' <param name="intPrgID">程式ID</param>
        ///     ''' <param name="strErrMsg">錯誤訊息</param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public bool DelPrg(int intPrgID, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "DELETE CATEGORY WHERE PRGSERNO=@strPRGSERNO";
            V_arr[0, 0] = "@strPRGSERNO";
            V_arr[0, 1] = intPrgID + "";
            pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
            // GroupToPrg
            strSQL = "DELETE GroupToPrg WHERE PRGSERNO=@strPRGSERNO";
            V_arr[0, 0] = "@strPRGSERNO";
            V_arr[0, 1] = intPrgID + "";
            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }

        /// <summary>
        ///     ''' 取得所有主目錄
        ///     ''' </summary>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public DataTable GetAllCategory()
        {
            string strSQL;
            // Dim ds As New DataSet
            strSQL = "SELECT PRGSERNO,PRGNAME FROM CATEGORY WHERE MASTERNO='-1' " + " ORDER BY SeqNo";

            return pub_lib.Query(strSQL).Tables[0];
        }
        /// <summary>
        ///     ''' 取得目錄/程式所有資訊
        ///     ''' </summary>
        ///     ''' <param name="intPrgSerNo">序號</param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public DataTable GetMenuInfo(int intPrgSerNo)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT PRGSERNO,PRGNAME,SEQNO,MASTERNO,CATEGORYDESC,ICONURL,PRGPATH" + " FROM CATEGORY WHERE PRGSERNO=@intPrgSerNo";
            V_arr[0, 0] = "@intPrgSerNo";
            V_arr[0, 1] = intPrgSerNo + "";

            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        #endregion
        #region 群組
        /// <summary>
        ///     ''' 查詢資料
        ///     ''' </summary>
        ///     ''' <param name="intStartIndex"></param>
        ///     ''' <param name="intPageSize"></param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public DataTable QueryGroup(int intStartIndex, int intPageSize)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT * FROM UserGroup ORDER BY GROUPNO";
            return pub_lib.Query(strSQL, intStartIndex, intPageSize).Tables[0];
        }
        /// <summary>
        ///     ''' 算出有幾筆
        ///     ''' </summary>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public int CountQueryGroup()
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT COUNT(*) FROM UserGroup ";
            try
            {
                return Convert.ToInt32(pub_lib.QueryFirstRec(strSQL));
            }
            catch (Exception Ex)
            {
                return 0;
            }
        }
        /// <summary>
        ///     ''' 新增群組
        ///     ''' </summary>
        ///     ''' <param name="strGroupName"></param>
        ///     ''' <param name="strErrMsg"></param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public bool AddGroup(string strGroupName, ref string strErrMsg)
        {
            string strSQL;
            int intID = 0;
            string[,] V_arr = new string[2, 2];
            strSQL = "SELECT MAX(GROUPNO) FROM UserGroup";
            try
            {
                intID = Convert.ToInt32(pub_lib.QueryFirstRec(strSQL));
            }
            catch (Exception ex)
            {
                intID = 0;
            }
            intID = intID + 1;

            strSQL = "INSERT INTO UserGroup (GROUPNO,GROUPNAME) VALUES " + " (@intGroupNo,@strGroupName)";
            V_arr[0, 0] = "@intGroupNo";
            V_arr[0, 1] = intID + "";
            V_arr[1, 0] = "@strGroupName";
            V_arr[1, 1] = strGroupName;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        /// <summary>
        ///     ''' 修改群組
        ///     ''' </summary>
        ///     ''' <param name="intGroupNo"></param>
        ///     ''' <param name="strGroupName"></param>
        ///     ''' <param name="strErrMsg"></param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public bool UpdateGroup(int intGroupNo, string strGroupName, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[2, 2];
            strSQL = "UPDATE UserGroup SET GROUPNAME=@strGroupName " + " WHERE GROUPNO=@intGroupNo";

            V_arr[0, 0] = "@strGroupName";
            V_arr[0, 1] = strGroupName;
            V_arr[1, 0] = "@intGroupNo";
            V_arr[1, 1] = intGroupNo + "";
            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        /// <summary>
        ///     ''' 刪除群組
        ///     ''' </summary>
        ///     ''' <param name="intGroupNo"></param>
        ///     ''' <param name="strErrMsg"></param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public bool DelGroup(int intGroupNo, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "DELETE UserGroup  " + " WHERE GROUPNO=@intGroupNo";
            V_arr[0, 0] = "@intGroupNo";
            V_arr[0, 1] = intGroupNo + "";
            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        /// <summary>
        ///     ''' 找出群組-程式關係
        ///     ''' </summary>
        ///     ''' <param name="intGrpNo">群組ID</param>
        ///     ''' <param name="intStartIndex">起始筆數</param>
        ///     ''' <param name="intPageSize">要幾筆</param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public DataTable QueryGroupPrg(int intGrpNo, int intStartIndex, int intPageSize)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT a.PRGSERNO,a.GROUPNO,b.PRGNAME FROM " + " GroupToPrg a INNER JOIN " + " CATEGORY           b ON a.PRGSERNO=b.PRGSERNO " + " WHERE a.GROUPNO=@intGROUPNO" + " ORDER BY a.PRGSERNO";
            V_arr[0, 0] = "@intGROUPNO";
            V_arr[0, 1] = intGrpNo + "";
            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        /// <summary>
        ///     ''' 算出群組-程式關係資料量
        ///     ''' </summary>
        ///     ''' <param name="intGrpNo"></param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public int CountGroupPrg(int intGrpNo)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT COUNT(*) FROM " + " GroupToPrg a INNER JOIN" + " CATEGORY           b ON a.PRGSERNO=b.PRGSERNO" + " WHERE a.GROUPNO=@intGROUPNO" + " ";
            V_arr[0, 0] = "@intGROUPNO";
            V_arr[0, 1] = intGrpNo + "";
            try
            {
                return Convert.ToInt32(pub_lib.QueryFirstRec(strSQL, V_arr));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        /// <summary>
        ///     ''' 新增群組-程式關係
        ///     ''' </summary>
        ///     ''' <param name="intGrpNo">群組ID</param>
        ///     ''' <param name="intPrgSerNo">程式ID</param>
        ///     ''' <param name="strErrMsg">錯誤訊息</param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public bool AddPrgForGrp(int intGrpNo, int intPrgSerNo, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[2, 2];
            int intTmp = 0;
            strSQL = "SELECT COUNT(*) FROM " + " GroupToPrg WHERE GROUPNO=@intGROUPNO" + " AND PRGSERNO=@intPRGSERNO";
            V_arr[0, 0] = "@intGROUPNO";
            V_arr[0, 1] = intGrpNo + "";
            V_arr[1, 0] = "@intPRGSERNO";
            V_arr[1, 1] = intPrgSerNo + "";
            try
            {
                intTmp = Convert.ToInt32(pub_lib.QueryFirstRec(strSQL, V_arr));
            }
            catch (Exception Ex)
            {
            }
            if (intTmp > 0)
            {
                strErrMsg = "Data Repeat!";
                return false;
            }

            strSQL = "INSERT INTO GroupToPrg(GROUPNO,PRGSERNO)" + " VALUES(@intGROUPNO,@intPRGSERNO)";
            V_arr[0, 0] = "@intGROUPNO";
            V_arr[0, 1] = intGrpNo + "";
            V_arr[1, 0] = "@intPRGSERNO";
            V_arr[1, 1] = intPrgSerNo + "";
            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        /// <summary>
        ///     ''' 刪除群組-程式關係
        ///     ''' </summary>
        ///     ''' <param name="intGrpNo">群組ID</param>
        ///     ''' <param name="intPrgSerNo">程式ID</param>
        ///     ''' <param name="strErrMsg">錯誤訊息</param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public bool DelPrgGroup(int intGrpNo, int intPrgSerNo, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[2, 2];

            strSQL = " DELETE  " + " GroupToPrg WHERE GROUPNO=@intGROUPNO" + " AND PRGSERNO=@intPRGSERNO";
            V_arr[0, 0] = "@intGROUPNO";
            V_arr[0, 1] = intGrpNo + "";
            V_arr[1, 0] = "@intPRGSERNO";
            V_arr[1, 1] = intPrgSerNo + "";
            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        /// <summary>
        ///     ''' 查出群組-使用者關係
        ///     ''' </summary>
        ///     ''' <param name="intGroupNo">群組NO</param>
        ///     ''' <param name="intStartIndex" >起始index</param>
        ///     ''' <param name="intPageSize">筆數</param> 
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public DataTable QueryGroupToUser(int intGroupNo, int intStartIndex, int intPageSize)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT * FROM GroupToUser WHERE GroupNo=@intGroupNo " + " ORDER BY GroupNo,EMPNO ";
            V_arr[0, 0] = "@intGroupNo";
            V_arr[0, 1] = intGroupNo + "";

            return pub_lib.Query(strSQL, V_arr, intStartIndex, intPageSize).Tables[0];
        }
        /// <summary>
        ///     ''' 算出有幾筆
        ///     ''' </summary>
        ///     ''' <param name="intGroupNo"></param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public int CountQueryGroupToUser(int intGroupNo)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT COUNT(*) FROM GroupToUser WHERE GroupNo=@intGroupNo " + " ";
            V_arr[0, 0] = "@intGroupNo";
            V_arr[0, 1] = intGroupNo + "";
            try
            {
                return Convert.ToInt32(pub_lib.QueryFirstRec(strSQL, V_arr));
            }
            catch (Exception Ex)
            {
                return 0;
            }
        }
        /// <summary>
        ///     ''' 新增群組-使用者關係
        ///     ''' </summary>
        ///     ''' <param name="intGroupNo">群組ID</param>
        ///     ''' <param name="strEMPNO">使用者工號</param>
        ///     ''' <param name="strErrMsg">錯誤訊息</param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public bool AddGroupToUser(int intGroupNo, string strAD, string strEMPNO, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[2, 2];
            string strTmp;
            strSQL = "SELECT COUNT(*) FROM GroupToUser WHERE" + " GroupNo=@intGroupNo AND EMPNO=@strEmpno";
            V_arr[0, 0] = "@intGroupNo";
            V_arr[0, 1] = intGroupNo + "";
            V_arr[1, 0] = "@strEmpno";
            V_arr[1, 1] = strEMPNO;
            strTmp = pub_lib.QueryFirstRec(strSQL, V_arr);
            if (Convert.ToInt32(strTmp) > 0)
            {
                // 已經有了
                strErrMsg = "GroupNo And EMPNO already exist!";
                return false;
            }
            V_arr = new string[3, 2];
            strSQL = "INSERT INTO GroupToUser (GroupNo,EMPNO,AD)" + " VALUES (@intGroupNo,@strEmpno,@strAD)";
            V_arr[0, 0] = "@intGroupNo";
            V_arr[0, 1] = intGroupNo + "";
            V_arr[1, 0] = "@strEmpno";
            V_arr[1, 1] = strEMPNO;
            V_arr[2, 0] = "@strAD";
            V_arr[2, 1] = strAD;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        /// <summary>
        ///     ''' 刪除群組-使用者關係
        ///     ''' </summary>
        ///     ''' <param name="intGroupNo"></param>
        ///     ''' <param name="strEMPNO"></param>
        ///     ''' <param name="strErrMsg"></param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public bool DelGroupToUser(int intGroupNo, string strEMPNO, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[2, 2];
            strSQL = "DELETE GroupToUser " + " WHERE GroupNo=@intGroupNo AND EMPNO=@strEmpno";
            V_arr[0, 0] = "@intGroupNo";
            V_arr[0, 1] = intGroupNo + "";
            V_arr[1, 0] = "@strEmpno";
            V_arr[1, 1] = strEMPNO;
            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        public int IsUserAdmin(string strEmpno)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT COUNT(*) FROM GroupToUser WHERE GROUPNO=1 AND EMPNO=@strEmpno ";
            V_arr[0, 0] = "@strEmpno";
            V_arr[0, 1] = strEmpno;
            try
            {
                return Convert.ToInt32(pub_lib.QueryFirstRec(strSQL, V_arr));
            }
            catch (Exception Ex)
            {
                return 0;
            }
        }
        public DataTable GetDeptType()
        {
            string strSQL;
            strSQL = " SELECT Code_desc,Code_val FROM Code_d WHERE Code_Kind='DeptType' ";
            return pub_lib.Query(strSQL).Tables[0];
        }
        public DataTable GetDeptType(string strCodeKind)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            V_arr[0, 0] = "@strCodeKind";
            V_arr[0, 1] = strCodeKind;
            strSQL = " SELECT Code_desc,Code_val FROM Code_d WHERE Code_Kind=@strCodeKind ";
            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }

        #endregion
        #region 其他





        /// <summary>
        ///     ''' 取得群組名稱
        ///     ''' </summary>
        ///     ''' <param name="intGrpNo"></param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public string GetGroupName(int intGrpNo)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT GroupName FROM UserGroup " + " WHERE GROUPNO=@intGroupNo";
            V_arr[0, 0] = "@intGroupNo";
            V_arr[0, 1] = intGrpNo + "";

            return pub_lib.QueryFirstRec(strSQL, V_arr);
        }

        #endregion
        #region e-mail log
        public bool AddEmailLog(string strEmpno, string strMailContent, ref string strErrMsg)
        {
            string strSQL = "INSERT INTO mail_log ( empno, mail_content) VALUES (@empno, @mail_content)";
            string[,] V_arr = new string[2, 2];
            V_arr[0, 0] = "@empno";
            V_arr[0, 1] = strEmpno;

            V_arr[1, 0] = "@mail_content";
            V_arr[1, 1] = strMailContent;
            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }


        #endregion
        #region Item_m
        public DataTable QueryItemEmpno(string MID)
        {
            string strSQL = "";
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT * FROM ( SELECT  DISTINCT   a.Empno,(SELECT TOP 1 b.User_Name FROM  [GHR].dbo.vw_Address_Book b WHERE a.empno=b.main_user) User_Name,ISNULL((SELECT TOP 1 CONVERT(NVARCHAR(20),c.Add_DateTime,120)  FROM mail_log c WHERE a.empno=c.empno ORDER BY c.ID DESC),'') Last_Send ,(SELECT COUNT(*) FROM vw_item_d aa WHERE  aa.empno=a.empno AND aa.actual_user=''  ) TT  FROM Item_d a  WHERE a.MID=@MID  AND a.BATNO =(SELECT MAX(BATNO) FROM Item_d ) ) ABC  WHERE TT > 2  ORDER BY Last_Send ASC";
            V_arr[0, 0] = "@MID";
            V_arr[0, 1] = MID;
            return pub_lib.Query(strSQL, V_arr).Tables[0];

        }
        public DataTable QueryItem_m()
        {
            string strSQL;
            strSQL = "SELECT * FROM Item_m WHERE stats='00' ";
            return pub_lib.Query(strSQL).Tables[0];
        }
        public DataTable QueryItem_m(string strid)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT * FROM Item_m WHERE id=@ID ";
            V_arr[0, 0] = "@id";
            V_arr[0, 1] = strid;
            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public DataTable QueryItem_m(string strItem_Name, string strSeq, string strstats)
        {
            string strSQL;
            string[,] V_arr = new string[2, 2];
            strSQL = "SELECT * FROM Item_m WHERE 1=1  AND Item_Name LIKE '%' + @Item_Name + '%'   AND stats LIKE '%' + @stats + '%'  ";
            V_arr[0, 0] = "@Item_Name";
            V_arr[0, 1] = strItem_Name;

            // V_arr[1, 0] = "@Seq";
            // V_arr[1, 1] = strSeq;

            V_arr[1, 0] = "@stats";
            V_arr[1, 1] = strstats;

            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public int CountItem_m(string strItem_Name, string strSeq, string strstats)
        {
            string strSQL;
            string[,] V_arr = new string[3, 2];
            strSQL = "SELECT COUNT(*) FROM Item_m WHERE 1=1  AND Item_Name LIKE '%' + @Item_Name + '%'  AND Seq LIKE '%' + @Seq + '%'  AND stats LIKE '%' + @stats + '%'  AND stats='00' ";
            V_arr[0, 0] = "@Item_Name";
            V_arr[0, 1] = strItem_Name;

            V_arr[1, 0] = "@Seq";
            V_arr[1, 1] = strSeq;

            V_arr[2, 0] = "@stats";
            V_arr[2, 1] = strstats;
            try
            {
                return Convert.ToInt32(pub_lib.QueryFirstRec(strSQL, V_arr));
            }
            catch (Exception Ex)
            {
                return 0;
            }
        }
        public DataTable QueryItem_m(string strItem_Name, string strSeq, string strstats, int intPage, int intPageSize)
        {
            string strSQL;
            string[,] V_arr = new string[3, 2];
            strSQL = "SELECT * FROM Item_m WHERE 1=1  AND Item_Name LIKE '%' + @Item_Name + '%'  AND Seq LIKE '%' + @Seq + '%'  AND stats LIKE '%' + @stats + '%'  AND stats='00' ";
            V_arr[0, 0] = "@Item_Name";
            V_arr[0, 1] = strItem_Name;

            V_arr[1, 0] = "@Seq";
            V_arr[1, 1] = strSeq;

            V_arr[2, 0] = "@stats";
            V_arr[2, 1] = strstats;

            return pub_lib.Query(strSQL, V_arr, intPage, intPageSize).Tables[0];
        }
        public bool AddItem_m(string strItem_Name, string strSeq, string strstats, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[3, 2];
            strSQL = " INSERT INTO Item_m (Item_Name,Seq,stats) values (@Item_Name,@Seq,@stats)";
            V_arr[0, 0] = "@Item_Name";
            V_arr[0, 1] = strItem_Name;

            V_arr[1, 0] = "@Seq";
            V_arr[1, 1] = strSeq;

            V_arr[2, 0] = "@stats";
            V_arr[2, 1] = strstats;


            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        public bool UpdItem_m(string strid, string strItem_Name, string strSeq, string strstats, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[4, 2];
            strSQL = "UPDATE Item_m SET  Item_Name=@Item_Name,Seq=@Seq,stats=@stats  WHERE id=@id";
            V_arr[0, 0] = "@id";
            V_arr[0, 1] = strid;

            V_arr[1, 0] = "@Item_Name";
            V_arr[1, 1] = strItem_Name;

            V_arr[2, 0] = "@Seq";
            V_arr[2, 1] = strSeq;

            V_arr[3, 0] = "@stats";
            V_arr[3, 1] = strstats;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        public bool DelItem_m(string strid, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "UPDATE Item_m set stats='XX'  WHERE  id=@id";
            V_arr[0, 0] = "@id";
            V_arr[0, 1] = strid;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        #endregion

        #region QueryItem_d
        public DataTable QueryItem_d()
        {
            string strSQL;
            strSQL = "SELECT * FROM Item_d WHERE stats='00' ";
            return pub_lib.Query(strSQL).Tables[0];
        }
        public DataTable QueryItem_dByEmpno(string strEmpno)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT a.id, a.MID, a.empno, a.Item_No, a.Item_Desc, a.Price, CONVERT(NVARCHAR(10),a.BuyDateTime,120) BuyDateTime, a.Qty, a.Actual_User, a.Usage, a.stats,b.code_desc FROM Item_d a,code_d b WHERE 1=1 AND a.empno = @empno  AND b.code_kind='Item_d' AND a.stats=b.code_val AND a.BATNO =(SELECT MAX(BATNO) FROM Item_d ) ";
            V_arr[0, 0] = "@empno";
            V_arr[0, 1] = strEmpno;
            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public DataTable QueryItem_d(string strid)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT * FROM Item_d WHERE stats='00'  AND id = @id  ";
            V_arr[0, 0] = "@id";
            V_arr[0, 1] = strid;
            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public DataTable QueryItem_dByRelayKey(string strMID)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT * FROM Item_d WHERE 1=1  AND MID = @MID  ";
            V_arr[0, 0] = "@MID";
            V_arr[0, 1] = strMID;
            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public DataTable QueryItem_d(string strMID, string strempno, string strItem_No, string strItem_Desc, string strPrice, string strBuyDateTime, string strQty, string strActual_User, string strUsage, string strstats)
        {
            string strSQL;
            string[,] V_arr = new string[10, 2];
            strSQL = "SELECT * FROM Item_d WHERE 1=1  AND MID = @MID   AND empno LIKE '%' + @empno + '%'  AND Item_No LIKE '%' + @Item_No + '%'  AND Item_Desc LIKE '%' + @Item_Desc + '%'  AND Price LIKE '%' + @Price + '%'  AND BuyDateTime LIKE '%' + @BuyDateTime + '%'  AND Qty LIKE '%' + @Qty + '%'  AND Actual_User LIKE '%' + @Actual_User + '%'  AND Usage LIKE '%' + @Usage + '%'  AND stats LIKE '%' + @stats + '%'  AND stats='00' ";
            V_arr[0, 0] = "@MID";
            V_arr[0, 1] = strMID;

            V_arr[1, 0] = "@empno";
            V_arr[1, 1] = strempno;

            V_arr[2, 0] = "@Item_No";
            V_arr[2, 1] = strItem_No;

            V_arr[3, 0] = "@Item_Desc";
            V_arr[3, 1] = strItem_Desc;

            V_arr[4, 0] = "@Price";
            V_arr[4, 1] = strPrice;

            V_arr[5, 0] = "@BuyDateTime";
            V_arr[5, 1] = strBuyDateTime;

            V_arr[6, 0] = "@Qty";
            V_arr[6, 1] = strQty;

            V_arr[7, 0] = "@Actual_User";
            V_arr[7, 1] = strActual_User;

            V_arr[8, 0] = "@Usage";
            V_arr[8, 1] = strUsage;

            V_arr[9, 0] = "@stats";
            V_arr[9, 1] = strstats;

            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public int CountItem_dByRelayKey(string strMID)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT COUNT(*) FROM Item_d WHERE 1=1  AND MID = @MID  AND BATNO =(SELECT MAX(BATNO) FROM Item_d )  ";
            V_arr[0, 0] = "@MID";
            V_arr[0, 1] = strMID;
            try
            {
                return Convert.ToInt32(pub_lib.QueryFirstRec(strSQL, V_arr));
            }
            catch (Exception Ex)
            {
                return 0;
            }
        }
        public DataTable QueryItem_dByRelayKey(string strMID, int intPage, int intPageSize)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT * FROM Item_d WHERE 1=1  AND MID = @MID  AND BATNO =(SELECT MAX(BATNO) FROM Item_d ) ";
            V_arr[0, 0] = "@MID";
            V_arr[0, 1] = strMID;
            return pub_lib.Query(strSQL, V_arr, intPage, intPageSize).Tables[0];
        }
        public bool AddItem_d(string strMID, string strempno, string strItem_No, string strItem_Desc, string strPrice, string strBuyDateTime, string strQty, string strActual_User, string strUsage, string strstats, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[10, 2];
            strSQL = " INSERT INTO Item_d (MID,empno,Item_No,Item_Desc,Price,BuyDateTime,Qty,Actual_User,Usage,stats) values (@MID,@empno,@Item_No,@Item_Desc,@Price,@BuyDateTime,@Qty,@Actual_User,@Usage,@stats)";
            V_arr[0, 0] = "@MID";
            V_arr[0, 1] = strMID;

            V_arr[1, 0] = "@empno";
            V_arr[1, 1] = strempno;

            V_arr[2, 0] = "@Item_No";
            V_arr[2, 1] = strItem_No;

            V_arr[3, 0] = "@Item_Desc";
            V_arr[3, 1] = strItem_Desc;

            V_arr[4, 0] = "@Price";
            V_arr[4, 1] = strPrice;

            V_arr[5, 0] = "@BuyDateTime";
            V_arr[5, 1] = strBuyDateTime;

            V_arr[6, 0] = "@Qty";
            V_arr[6, 1] = strQty;

            V_arr[7, 0] = "@Actual_User";
            V_arr[7, 1] = strActual_User;

            V_arr[8, 0] = "@Usage";
            V_arr[8, 1] = strUsage;

            V_arr[9, 0] = "@stats";
            V_arr[9, 1] = strstats;


            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        public bool UpdItem_d(string strid, string strActual_User, string strUsage, string strstats, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[4, 2];
            strSQL = "UPDATE Item_d SET  Actual_User=@Actual_User,Usage=@Usage,stats=@stats  WHERE id=@id";
            V_arr[0, 0] = "@id";
            V_arr[0, 1] = strid;



            V_arr[1, 0] = "@Actual_User";
            V_arr[1, 1] = strActual_User;

            V_arr[2, 0] = "@Usage";
            V_arr[2, 1] = strUsage;

            V_arr[3, 0] = "@stats";
            V_arr[3, 1] = strstats;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        public bool UpdItem_d(string strid, string strMID, string strempno, string strItem_No, string strItem_Desc, string strPrice, string strBuyDateTime, string strQty, string strActual_User, string strUsage, string strstats, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[11, 2];
            strSQL = "UPDATE Item_d SET  MID=@MID,empno=@empno,Item_No=@Item_No,Item_Desc=@Item_Desc,Price=@Price,BuyDateTime=@BuyDateTime,Qty=@Qty,Actual_User=@Actual_User,Usage=@Usage,stats=@stats  WHERE id=@id AND BATNO =(SELECT MAX(BATNO) FROM Item_d ) ";
            V_arr[0, 0] = "@id";
            V_arr[0, 1] = strid;

            V_arr[1, 0] = "@MID";
            V_arr[1, 1] = strMID;

            V_arr[2, 0] = "@empno";
            V_arr[2, 1] = strempno;

            V_arr[3, 0] = "@Item_No";
            V_arr[3, 1] = strItem_No;

            V_arr[4, 0] = "@Item_Desc";
            V_arr[4, 1] = strItem_Desc;

            V_arr[5, 0] = "@Price";
            V_arr[5, 1] = strPrice;

            V_arr[6, 0] = "@BuyDateTime";
            V_arr[6, 1] = strBuyDateTime;

            V_arr[7, 0] = "@Qty";
            V_arr[7, 1] = strQty;

            V_arr[8, 0] = "@Actual_User";
            V_arr[8, 1] = strActual_User;

            V_arr[9, 0] = "@Usage";
            V_arr[9, 1] = strUsage;

            V_arr[10, 0] = "@stats";
            V_arr[10, 1] = strstats;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        public bool UpdItem_d(string strid, string strActual_User, string strUsage, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[3, 2];
            strSQL = "UPDATE Item_d SET  Actual_User=@Actual_User,Usage=@Usage  WHERE id=@id";
            V_arr[0, 0] = "@id";
            V_arr[0, 1] = strid;



            V_arr[1, 0] = "@Actual_User";
            V_arr[1, 1] = strActual_User;

            V_arr[2, 0] = "@Usage";
            V_arr[2, 1] = strUsage;



            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        public bool DelItem_d(string strid, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "UPDATE Item_d set stats='XX'  WHERE  id=@id AND BATNO =(SELECT MAX(BATNO) FROM Item_d )  ";
            V_arr[0, 0] = "@id";
            V_arr[0, 1] = strid;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }

        #endregion
        #region Code_d
        public DataTable QueryCode_dByID(string strID)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT * FROM  code_d WHERE code_no=@strID";
            V_arr[0, 0] = "@strID";
            V_arr[0, 1] = strID;

            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public DataTable QueryCode_d(string strCodeKind)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT * FROM  code_d WHERE Code_Kind=@strCodeKind";
            V_arr[0, 0] = "@strCodeKind";
            V_arr[0, 1] = strCodeKind;

            return pub_lib.Query(strSQL, V_arr).Tables[0];

        }
        public DataTable QueryCode_d(string strCodeKind, string strOrderColumn)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT * FROM  code_d WHERE Code_Kind=@strCodeKind ORDER BY CONVERT(INT,attr1)";
            V_arr[0, 0] = "@strCodeKind";
            V_arr[0, 1] = strCodeKind;

            return pub_lib.Query(strSQL, V_arr).Tables[0];

        }
        public bool UpdCode_d(string strCodeKind, string strCode_val, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[2, 2];
            strSQL = " UPDATE code_d SET code_val=@strCode_val WHERE code_kind=@strCodeKind";

            V_arr[0, 0] = "@strCode_val";
            V_arr[0, 1] = strCode_val;

            V_arr[1, 0] = "@strCodeKind";
            V_arr[1, 1] = strCodeKind;


            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        public bool UpdCode_d(string strCodeKind, string strCode_val, string strAttr1, ref string strErrMsg)
        {
            string strSQL = "";
            string[,] V_arr = new string[3, 2];
            strSQL = " UPDATE code_d SET code_val=@strCode_val,attr1=@strAttr1 WHERE code_kind=@strCodeKind";

            V_arr[0, 0] = "@strCode_val";
            V_arr[0, 1] = strCode_val;

            V_arr[1, 0] = "@strAttr1";
            V_arr[1, 1] = strAttr1;

            V_arr[2, 0] = "@strCodeKind";
            V_arr[2, 1] = strCodeKind;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        public DataTable QueryOptionsFromCode_d(string strCodeKind)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT * FROM Code_d where code_kind=@strCodeKind";
            V_arr[0, 0] = "@strCodeKind";
            V_arr[0, 1] = strCodeKind;

            return pub_lib.Query(strSQL, V_arr).Tables[0];

        }
        public DataTable QueryOptionsFromCode_dWithOrder(string strCodeKind)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT * FROM Code_d where code_kind=@strCodeKind ORDER BY CONVERT(INT,attr1)";
            V_arr[0, 0] = "@strCodeKind";
            V_arr[0, 1] = strCodeKind;

            return pub_lib.Query(strSQL, V_arr).Tables[0];

        }
        public DataTable QueryOptionsFromTableWithWhere(string strTableName, string strDisplayColumn, string strValueColumn, string strWhere, string strOrderColumn)
        {
            string strSQL;

            strSQL = "SELECT " + strDisplayColumn + "," + strValueColumn + " FROM " + strTableName + (!strWhere.Equals("") ? " WHERE " + strWhere : "") + (!strOrderColumn.Equals("") ? " ORDER BY " + strValueColumn : "");


            return pub_lib.Query(strSQL).Tables[0];
        }
        public DataTable QueryOptionsFromTable(string strTableName, string strDisplayColumn, string strValueColumn)
        {
            string strSQL = "";

            strSQL = "SELECT " + strDisplayColumn + "," + strValueColumn + " FROM " + strTableName; //& " ORDER BY " & strValueColumn


            return pub_lib.Query(strSQL).Tables[0];
        }
        public DataTable QueryOptionsFromTable(string strTableName, string strDisplayColumn, string strValueColumn, string strOrderColumn)
        {
            string strSQL = "";

            strSQL = "SELECT " + strDisplayColumn + "," + strValueColumn + " FROM " + strTableName + " ORDER BY " + strValueColumn;


            return pub_lib.Query(strSQL).Tables[0];
        }

        public DataTable GenCheckBoxFromTable(string strTable, string strColumn, string strWhere)
        {
            string strSQL = "";
            strSQL = "SELECT " + strColumn + " FROM " + strTable + " ";
            if (!strWhere.Equals(""))
            {
                strSQL = strSQL + " WHERE " + strWhere;
            }
            return pub_lib.Query(strSQL).Tables[0];
        }
        #endregion
        #region Response_d
        public DataTable QueryResponse_d()
        {
            string strSQL;
            strSQL = "SELECT * FROM Response_d  ";
            return pub_lib.Query(strSQL).Tables[0];
        }
        public DataTable QueryResponse_dByCode(string strCode)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT * FROM Response_d WHERE Response_Code=@Response_Code ";
            V_arr[0, 0] = "@Response_Code";
            V_arr[0, 1] = strCode;
            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public DataTable QueryResponse_d(string strid)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "SELECT * FROM Response_d WHERE id=@ID ";
            V_arr[0, 0] = "@id";
            V_arr[0, 1] = strid;
            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public DataTable QueryResponse_d(string strResponse_Name, string strResponse_Desc, string strResponse_Code, string strResponse_String)
        {
            string strSQL;
            string[,] V_arr = new string[4, 2];
            strSQL = "SELECT * FROM Response_d WHERE 1=1  AND Response_Name LIKE '%' + @Response_Name + '%'  AND Response_Desc LIKE '%' + @Response_Desc + '%'  AND Response_Code LIKE '%' + @Response_Code + '%'  AND Response_String LIKE '%' + @Response_String + '%'  ";
            V_arr[0, 0] = "@Response_Name";
            V_arr[0, 1] = strResponse_Name;

            V_arr[1, 0] = "@Response_Desc";
            V_arr[1, 1] = strResponse_Desc;

            V_arr[2, 0] = "@Response_Code";
            V_arr[2, 1] = strResponse_Code;

            V_arr[3, 0] = "@Response_String";
            V_arr[3, 1] = strResponse_String;

            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public int CountResponse_d(string strResponse_Name, string strResponse_Desc, string strResponse_Code, string strResponse_String)
        {
            string strSQL;
            string[,] V_arr = new string[4, 2];
            strSQL = "SELECT COUNT(*) FROM Response_d WHERE 1=1  AND Response_Name LIKE '%' + @Response_Name + '%'  AND Response_Desc LIKE '%' + @Response_Desc + '%'  AND Response_Code LIKE '%' + @Response_Code + '%'  AND Response_String LIKE '%' + @Response_String + '%'   ";
            V_arr[0, 0] = "@Response_Name";
            V_arr[0, 1] = strResponse_Name;

            V_arr[1, 0] = "@Response_Desc";
            V_arr[1, 1] = strResponse_Desc;

            V_arr[2, 0] = "@Response_Code";
            V_arr[2, 1] = strResponse_Code;

            V_arr[3, 0] = "@Response_String";
            V_arr[3, 1] = strResponse_String;

            return Convert.ToInt32(pub_lib.QueryFirstRec(strSQL, V_arr));
        }
        public DataTable QueryResponse_d(string strResponse_Name, string strResponse_Desc, string strResponse_Code, string strResponse_String, int intPage, int intPageSize)
        {
            string strSQL;
            string[,] V_arr = new string[4, 2];
            strSQL = "SELECT * FROM Response_d WHERE 1=1  AND Response_Name LIKE '%' + @Response_Name + '%'  AND Response_Desc LIKE '%' + @Response_Desc + '%'  AND Response_Code LIKE '%' + @Response_Code + '%'  AND Response_String LIKE '%' + @Response_String + '%' ";
            V_arr[0, 0] = "@Response_Name";
            V_arr[0, 1] = strResponse_Name;

            V_arr[1, 0] = "@Response_Desc";
            V_arr[1, 1] = strResponse_Desc;

            V_arr[2, 0] = "@Response_Code";
            V_arr[2, 1] = strResponse_Code;

            V_arr[3, 0] = "@Response_String";
            V_arr[3, 1] = strResponse_String;

            return pub_lib.Query(strSQL, V_arr, intPage, intPageSize).Tables[0];
        }
        public bool AddResponse_d(string strResponse_Name, string strResponse_Desc, string strResponse_Code, string strResponse_String, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[4, 2];
            strSQL = " INSERT INTO Response_d (Response_Name,Response_Desc,Response_Code,Response_String) values (@Response_Name,@Response_Desc,@Response_Code,@Response_String)";
            V_arr[0, 0] = "@Response_Name";
            V_arr[0, 1] = strResponse_Name;

            V_arr[1, 0] = "@Response_Desc";
            V_arr[1, 1] = strResponse_Desc;

            V_arr[2, 0] = "@Response_Code";
            V_arr[2, 1] = strResponse_Code;

            V_arr[3, 0] = "@Response_String";
            V_arr[3, 1] = strResponse_String;


            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        public bool UpdResponse_d(string strid, string strResponse_Name, string strResponse_Desc, string strResponse_Code, string strResponse_String, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[5, 2];
            strSQL = "UPDATE Response_d SET  Response_Name=@Response_Name,Response_Desc=@Response_Desc,Response_Code=@Response_Code,Response_String=@Response_String  WHERE id=@id";
            V_arr[0, 0] = "@id";
            V_arr[0, 1] = strid;

            V_arr[1, 0] = "@Response_Name";
            V_arr[1, 1] = strResponse_Name;

            V_arr[2, 0] = "@Response_Desc";
            V_arr[2, 1] = strResponse_Desc;

            V_arr[3, 0] = "@Response_Code";
            V_arr[3, 1] = strResponse_Code;

            V_arr[4, 0] = "@Response_String";
            V_arr[4, 1] = strResponse_String;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        public bool DelResponse_d(string strid, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];
            strSQL = "DELETE Response_d   WHERE  id=@id";
            V_arr[0, 0] = "@id";
            V_arr[0, 1] = strid;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        #endregion
        #region 登入資訊-API
        public int IsAppGrant(string strAppName, string strKey)
        {
            string strSQL;
            string[,] V_arr = new string[2, 2];
            strSQL = "SELECT COUNT(*) FROM  ALLOWAPPACCOUNTS WHERE APPACCOUNT=@acc AND  APPPASSWORD=@pwd";

            V_arr[0, 0] = "@acc";
            V_arr[0, 1] = strAppName;

            V_arr[1, 0] = "@pwd";
            V_arr[1, 1] = strKey;
            try
            {
                return Convert.ToInt32(pub_lib.QueryFirstRec(strSQL, V_arr));
            }
            catch (Exception ex)
            {
            }
            return 0;
        }
        public bool AddLoginFile(string empno, string MobileSystem, string PackageName, string VersionName, string VersionCode, string ServerKey, string DeviceId, string Model, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[8, 2];
            strSQL = "  INSERT INTO  User_login(empno, MobileSystem, PackageName, VersionName, VersionCode, ServerKey,DeviceId,Model) " + " VALUES (@empno, @MobileSystem, @PackageName, @VersionName, @VersionCode, @ServerKey,@DeviceId,@Model)";
            V_arr[0, 0] = "@empno";
            V_arr[0, 1] = empno;

            V_arr[1, 0] = "@MobileSystem";
            V_arr[1, 1] = MobileSystem;

            V_arr[2, 0] = "@PackageName";
            V_arr[2, 1] = PackageName;

            V_arr[3, 0] = "@VersionName";
            V_arr[3, 1] = VersionName;

            V_arr[4, 0] = "@VersionCode";
            V_arr[4, 1] = VersionCode;


            V_arr[5, 0] = "@ServerKey";
            V_arr[5, 1] = ServerKey;


            V_arr[6, 0] = "@DeviceId";
            V_arr[6, 1] = DeviceId;

            V_arr[7, 0] = "@Model";
            V_arr[7, 1] = Model;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        public DataTable CheckLogin(string ServerKey)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];


            V_arr[0, 0] = "@ServerKey";
            V_arr[0, 1] = ServerKey;
            strSQL = "SELECT * FROM User_login WHERE  ServerKey=@ServerKey AND stats='00' ";
            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public DataTable CheckVerson(string VersionCode)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];


            V_arr[0, 0] = "@VersionCode";
            V_arr[0, 1] = VersionCode;
            strSQL = "SELECT * FROM User_login WHERE  VersionCode=@VersionCode AND stats='00' ";
            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public bool DeleteLoginFile(string empno, ref string strErrMsg)
        {
            string strSQL;
            string[,] V_arr = new string[1, 2];


            V_arr[0, 0] = "@empno";
            V_arr[0, 1] = empno;
            strSQL = "UPDATE User_login SET stats='XX' WHERE  empno=@empno AND stats='00' ";

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        #endregion
        #region 離職員工
        public bool AddQuitEmpno(string strEmpno,string strInsid,ref string strErrMsg) {
            string strSQL = "";
            string[,] V_arr = new string[2, 2];
            strSQL = "INSERT INTO quit_empno_d(Quit_Empno,insid) VALUES(@empno,@insid)";

            V_arr[0, 0] = "@empno";
            V_arr[0, 1] = strEmpno;


            V_arr[1, 0] = "@insid";
            V_arr[1, 1] = strInsid;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);   

        
        }
        #endregion
        #region tiptop
 


        #endregion
    }
}
