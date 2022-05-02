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
            strSQL = "SELECT DISTINCT  a.* FROM vw_menu a,GroupToPrg b ,GroupToUser c WHERE a.PrgSerNo=b.PrgSerNo AND b.GROUPNo =c.GROUPNo AND c.Empno=@empno ORDER BY a.BigSeq,a.MasterNo,a.SeqNo ";
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

        #region 登入
        public DataTable LoginAD(string AD, string Account, string Pass)
        {
            string[,] V_arr = new string[3, 2];
            string strSQL = "SELECT a.id, a.AD, a.Account, a.stats,(SELECT TOP 1 b.USER_Name FROM [EHR].[dbo].[vw_ADDRESS_BOOK2] b WHERE  b.AD_Domain_Name=a.AD AND b.AD_Account=a.Account ) Real_Name, a.Pass, a.E_mail FROM User_Data a WHERE a.AD=@AD AND a.Account=@Account AND a.Pass=@Pass AND a.stats='00'";

            V_arr[0, 0] = "@AD";
            V_arr[0, 1] = AD;

            V_arr[1, 0] = "@Account";
            V_arr[1, 1] = Account;

            V_arr[2, 0] = "@Pass";
            V_arr[2, 1] = Pass;

            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public DataTable LoginLDAP(string E_mail, string Pass)
        {
            string[,] V_arr = new string[2, 2];
            string strSQL = "SELECT a.id, a.AD, a.Account, a.stats,(SELECT TOP 1 b.USER_Name FROM [EHR].[dbo].[vw_ADDRESS_BOOK2] b WHERE  b.E_Mail1=a.E_mail  ) Real_Name, a.Pass, a.E_mail  FROM User_Data WHERE E_mail=@E_mail AND  Pass=@Pass  AND stats='00'";

            V_arr[0, 0] = "@E_mail";
            V_arr[0, 1] = E_mail;

            V_arr[1, 0] = "@Pass";
            V_arr[1, 1] = Pass;

            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        #endregion
        #region Group
        public DataTable QueryAllGroup()
        {
            string strSQL = "SELECT * FROM UserGroup ORDER BY SeqNo";

            return pub_lib.Query(strSQL).Tables[0];
        }
        public bool AddGroup(string GROUPNO, string GroupDesc, string GroupName, ref string strErrMsg)
        {
            string[,] V_arr = new string[3, 2];
            string strSQL = "INSERT INTO UserGroup (GROUPNO, GroupName, GroupDesc ) VALUES (@GROUPNO, @GroupName, @GroupDesc)";
            V_arr[0, 0] = "@GROUPNO";
            V_arr[0, 1] = GROUPNO;

            V_arr[1, 0] = "@GroupName";
            V_arr[1, 1] = GroupDesc;

            V_arr[2, 0] = "@GroupDesc";
            V_arr[2, 1] = GroupName;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);

        }
        public bool UPDGroup(string SeqNo, string GROUPNO, string GroupDesc, string GroupName, ref string strErrMsg)
        {
            string[,] V_arr = new string[4, 2];
            string strSQL = "UPDATE UserGroup SET GROUPNO =@GROUPNO , GroupName=@GroupName, GroupDesc=@GroupDesc  WHERE SeqNo=@SeqNo";
            V_arr[0, 0] = "@GROUPNO";
            V_arr[0, 1] = GROUPNO;

            V_arr[1, 0] = "@GroupName";
            V_arr[1, 1] = GroupDesc;

            V_arr[2, 0] = "@GroupDesc";
            V_arr[2, 1] = GroupName;

            V_arr[3, 0] = "@SeqNo";
            V_arr[3, 1] = SeqNo;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);

        }
        public bool DELGroup(string SeqNo, ref string strErrMsg)
        {
            string[,] V_arr = new string[1, 2];
            string strSQL = "DELETE UserGroup WHERE SeqNo=@SeqNo";

            V_arr[0, 0] = "@SeqNo";
            V_arr[0, 1] = SeqNo;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);

        }
        public DataSet QRYGP(string GROUPNO)
        {
            string[,] V_arr = new string[1, 2];
            string strSQL = "SELECT a.Seqno ID,b.*,CASE WHEN b.MasterNo=-1 THEN '目錄' ELSE '程式' END TypeDesc FROM GroupToPrg a, Category b WHERE a.PrgSerNo=b.PrgSerNo AND a.GROUPNO=@GROUPNO ORDER BY b.SeqNo";
            V_arr[0, 0] = "@GROUPNO";
            V_arr[0, 1] = GROUPNO;
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataSet ds = new DataSet();
            dt1 = pub_lib.Query(strSQL, V_arr).Tables[0];
            dt1.TableName = "AllDatas";
            strSQL = "SELECT b.*,CASE WHEN b.MasterNo=-1 THEN '目錄' ELSE '程式' END TypeDesc,  CASE WHEN b.MasterNo=-1 THEN  SeqNo ELSE (SELECT TOP 1 a.SeqNo FROM Category a WHERE a.PrgSerNo=b.MasterNo) END BigSeq FROM Category b WHERE 1=1 AND PrgSerNo NOT IN ( SELECT PrgSerNo FROM GroupToPrg WHERE GROUPNO=@GROUPNO)  ORDER BY  BigSeq DESC ,MasterNo ASC,SeqNo  DESC";

            dt2 = pub_lib.Query(strSQL, V_arr).Tables[0];
            dt2.TableName = "PartDatas";
            ds.Tables.Add(dt1.Copy());
            ds.Tables.Add(dt2.Copy());
            return ds;

        }
        public bool AddGP(string PrgSerNo, string GROUPNO, ref string strErrMsg)
        {
            string strSQL = "INSERT INTO GroupToPrg (PrgSerNo,GROUPNO) VALUES (@PrgSerNo,@GROUPNO)";
            string[,] V_arr = new string[2, 2];
            V_arr[0, 0] = "@PrgSerNo";
            V_arr[0, 1] = PrgSerNo;

            V_arr[1, 0] = "@GROUPNO";
            V_arr[1, 1] = GROUPNO;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);

        }
        public bool DelGP(string SeqNo, ref string strErrMsg)
        {
            string strSQL = "DELETE GroupToPrg WHERE Seqno=@SeqNo";
            string[,] V_arr = new string[1, 2];
            V_arr[0, 0] = "@SeqNo";
            V_arr[0, 1] = SeqNo;


            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);

        }
        public DataTable QRYGU(string GROUPNO)
        {
            string strSQL = "SELECT * FROM GroupToUser  WHERE GROUPNO=@GROUPNO ORDER BY Serno";
            string[,] V_arr = new string[1, 2];
            V_arr[0, 0] = "@GROUPNO";
            V_arr[0, 1] = GROUPNO;
            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }
        public bool ADDGU(string GROUPNO, string EMPNO, string AD, ref string strErrMsg)
        {
            string strSQL = "INSERT INTO GroupToUser (EMPNO,GROUPNO,AD) VALUES (@EMPNO,@GROUPNO,@AD)";
            string[,] V_arr = new string[3, 2];
            V_arr[0, 0] = "@EMPNO";
            V_arr[0, 1] = EMPNO;

            V_arr[1, 0] = "@GROUPNO";
            V_arr[1, 1] = GROUPNO;

            V_arr[2, 0] = "@AD";
            V_arr[2, 1] = AD;


            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);

        }
        public bool DELGU(string SerNo, ref string strErrMsg)
        {
            string strSQL = "DELETE GroupToUser WHERE Serno=@SerNo";
            string[,] V_arr = new string[1, 2];
            V_arr[0, 0] = "@SerNo";
            V_arr[0, 1] = SerNo;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);

        }
        public DataTable Q1UserGroup(string SeqNo)
        {
            string strSQL = "SELECT * FROM UserGroup WHERE SeqNo=@SeqNo";
            string[,] V_arr = new string[1, 2];
            V_arr[0, 0] = "@SeqNo";
            V_arr[0, 1] = SeqNo;

            return pub_lib.Query(strSQL, V_arr).Tables[0];
        }

        #endregion
        #region Category
        public DataTable QryCategory()
        {
            string strSQL = "SELECT * FROM vw_menu ORDER BY BigSeq,MasterNo,SeqNo";

            return pub_lib.Query(strSQL).Tables[0];
        }
        public bool AddCategory(string PrgPath, string CategoryDesc, string PrgName, string MasterNo, string SeqNo, ref string strErrMsg)
        {
            string[,] V_arr = new string[5, 2];
            string strSQL = "INSERT INTO Category(PrgPath, CategoryDesc, PrgName, MasterNo, SeqNo) VALUES (@PrgPath, @CategoryDesc, @PrgName, @MasterNo, @SeqNo)";

            V_arr[0, 0] = "@PrgPath";
            V_arr[0, 1] = PrgPath;

            V_arr[1, 0] = "@CategoryDesc";
            V_arr[1, 1] = CategoryDesc;

            V_arr[2, 0] = "@PrgName";
            V_arr[2, 1] = PrgName;

            V_arr[3, 0] = "@MasterNo";
            V_arr[3, 1] = MasterNo;

            V_arr[4, 0] = "@SeqNo";
            V_arr[4, 1] = SeqNo;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        public bool UpdCategory(string PrgSerNo,string PrgPath, string CategoryDesc, string PrgName, string MasterNo, string SeqNo, ref string strErrMsg) {

            string[,] V_arr = new string[6, 2];
         string   strSQL = "UPDATE Category SET PrgPath=@PrgPath , CategoryDesc=@CategoryDesc , PrgName=@PrgName , MasterNo=@MasterNo, SeqNo=@SeqNo WHERE PrgSerNo=@PrgSerNo ";

            V_arr[0, 0] = "@PrgPath";
            V_arr[0, 1] = PrgPath;

            V_arr[1, 0] = "@CategoryDesc";
            V_arr[1, 1] = CategoryDesc;

            V_arr[2, 0] = "@PrgName";
            V_arr[2, 1] = PrgName;

            V_arr[3, 0] = "@MasterNo";
            V_arr[3, 1] = MasterNo;

            V_arr[4, 0] = "@SeqNo";
            V_arr[4, 1] = SeqNo;

            V_arr[5, 0] = "@PrgSerNo";
            V_arr[5, 1] = PrgSerNo;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);

        }
        public bool DelCategory(string PrgSerNo, ref string strErrMsg)
        {
            string[,] V_arr = new string[1, 2];

            string strSQL = "DELETE  Category   WHERE PrgSerNo=@PrgSerNo";


            V_arr[0, 0] = "@PrgSerNo";
            V_arr[0, 1] = PrgSerNo;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        public DataTable Q1Category(string PrgSerNo)
        {
            string[,] V_arr = new string[1, 2];

            string strSQL = "SELECT * FROM  Category where PrgSerNo=@PrgSerNo";


            V_arr[0, 0] = "@PrgSerNo";
            V_arr[0, 1] = PrgSerNo;

            return pub_lib.Query(strSQL, V_arr).Tables[0]; 
        }
        public DataTable QueryMenuCategory()
        {
            string strSQL = "SELECT * FROM  Category where MasterNo=-1 ORDER BY SeqNo";
            return pub_lib.Query(strSQL).Tables[0];
        }
        #endregion
        #region RegAllComputer
        public DataSet InitRegAllComputer()
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataSet ds = new DataSet();

            string strSQL = "SELECT * FROM code_d WHERE code_kind='own_type'  ORDER BY CONVERT(INT,attr1) DESC";
            dt1 =pub_lib.Query(strSQL).Tables[0];
            dt1.TableName = "own_type";


            strSQL = "SELECT * FROM code_d WHERE code_kind='Computer_status'   ORDER BY CONVERT(INT,attr1) DESC";
            dt2 = pub_lib.Query(strSQL).Tables[0];
            dt2.TableName="Computer_status";

            ds.Tables.Add(dt1.Copy());
            ds.Tables.Add(dt2.Copy());
            return ds;
        }
        public bool AddRegAllComputer(string empno,string Computer_Name,string fingerprint,string host_name,ref string strErrMsg)
        {
            string[,] V_arr = new string[4, 1];
          string   strSQL = "INSTERT INTO  user_Computer (empno, Computer_Name,  fingerprint, host_name) VALUES (@empno, @Computer_Name, @fingerprint, @host_name)";
            V_arr[0, 0] = "@empno";
            V_arr[0, 1] = empno;

            V_arr[1, 0] = "@Computer_Name";
            V_arr[1, 1] = Computer_Name;

            V_arr[2, 0] = "@fingerprint";
            V_arr[2, 1] = fingerprint;

            V_arr[3, 0] = "@host_name";
            V_arr[3, 1] = host_name;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);

        }
        public bool UpdRegAllComputer(string Computer_Name,string fingerprint,string host_name,string id,ref string  strErrMsg)
        {
            string[,] V_arr = new string[4, 1];
            string strSQL = " UPDATE user_Computer SET Computer_Name=@Computer_Name ,  fingerprint=@fingerprint , host_name=@host_name WHERE id=@id ";

           

            V_arr[0, 0] = "@Computer_Name";
            V_arr[0, 1] = Computer_Name;

            V_arr[1, 0] = "@fingerprint";
            V_arr[1, 1] = fingerprint;

            V_arr[2, 0] = "@host_name";
            V_arr[2, 1] = host_name;

            V_arr[3, 0] = "@id";
            V_arr[3, 1] = id;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        public int QryCountRegAllComputer(string Empno,string own_type,string stats,string Mac,string FAJ)
        {
            DataTable dt1 = new DataTable();
         
            string[,] V_arr = new string[5,2];
            int TotalPages = 0;
            string strSQL = "SELECT COUNT(*) TT FROM user_Computer WHERE empno LIKE '%' + @empno + '%' AND own_type LIKE '%' + @own_type + '%'  AND stats LIKE '%' + @stats + '%' AND id IN ( SELECT aa.User_Computer_id FROM  Computer_MacAddress aa ,MacAddress_d bb WHERE  Mac_Address LIKE '%' +@Mac +'%' AND aa.MacAddress_id =bb.id ) AND FAJ_number LIKE '%' + @FAJ + '%' ";
            V_arr[0, 0] = "@empno";
            V_arr[0, 1] = Empno;

            V_arr[1, 0] = "@own_type";
            V_arr[1, 1] = own_type;

            V_arr[2, 0] = "@stats";
            V_arr[2, 1] = stats;

            V_arr[3, 0] = "@Mac";
            V_arr[3, 1] = Mac;

            V_arr[4, 0] = "@FAJ";
            V_arr[4, 1] = FAJ;
            dt1 = pub_lib.Query(strSQL, V_arr).Tables[0];
            TotalPages = (Convert.ToInt32(dt1.Rows[0]["TT"].ToString()) % 10 == 0 ? Convert.ToInt32(dt1.Rows[0]["TT"].ToString()) / 10 : (Convert.ToInt32(dt1.Rows[0]["TT"].ToString()) / 10) + 1);
            return TotalPages;

        }
        public DataTable QryRegAllComputer(string Empno, string own_type, string stats, string Mac, string FAJ, int Page)
        {
            DataTable dt1 = new DataTable();

            string[,] V_arr = new string[5, 2];
            string strSQL = "SELECT a.*, (SELECT TOP 1 b.user_name FROM [EHR].[dbo].[vw_address_book2] b WHERE a.empno COLLATE   Chinese_Taiwan_Stroke_CI_AS  =b.main_user  COLLATE   Chinese_Taiwan_Stroke_CI_AS  ) user_name ,c.code_desc stats_desc,d.code_desc own_type_desc ,(SELECT + '{' + '\"name\":' + '\"' +  aa.name + '\"' + ',' + '\"value\":' + '\"' + aa.Mac_Address + '\"' + '},' FROM MacAddress_d aa,Computer_MacAddress bb WHERE aa.id=bb.MacAddress_id AND bb.User_Computer_id =a.ID FOR XML PATH('') )   Mac_Address_d FROM user_Computer a,code_d c,code_d d   WHERE a.empno LIKE '%' + @empno + '%' AND a.own_type LIKE '%' + @own_type + '%' AND a.stats LIKE '%' + @stats + '%' AND a.stats=c.code_val AND c.code_kind='Computer_status' AND a.[Own_Type] COLLATE Chinese_Taiwan_Stroke_CI_AS=d.[code_val] COLLATE Chinese_Taiwan_Stroke_CI_AS AND d.code_kind='own_type' AND a.id IN ( SELECT aa.User_Computer_id FROM  Computer_MacAddress aa ,MacAddress_d bb WHERE  Mac_Address LIKE '%' +@Mac +'%'  AND aa.MacAddress_id =bb.id ) AND a.FAJ_number LIKE '%' + @FAJ + '%' ORDER BY id DESC";
            strSQL = strSQL + " OFFSET ("+ (Page - 1) * 10 + ") ROWS FETCH NEXT (10) ROWS ONLY ";
            strSQL = strSQL + "";
            V_arr[0, 0] = "@empno";
            V_arr[0, 1] = Empno;

            V_arr[1, 0] = "@own_type";
            V_arr[1, 1] = own_type;

            V_arr[2, 0] = "@stats";
            V_arr[2, 1] = stats;

            V_arr[3, 0] = "@Mac";
            V_arr[3, 1] = Mac;

            V_arr[4, 0] = "@FAJ";
            V_arr[4, 1] = FAJ;

            //V_arr[5, 0] = "@Skip";
            //V_arr[5, 1] = (Page - 1) * 10 + "";

            //V_arr[6, 0] = "@Take";
            //V_arr[6, 1] = "10";

          return  pub_lib.Query(strSQL,V_arr).Tables[0];
        }
        public DataTable Q1RegAllComputer(string id)
        {
            string[,] V_arr = new string[1, 2];
            string strSQL = "SELECT a.*, (SELECT TOP 1 b.user_name FROM [EHR].[dbo].[vw_address_book2] b WHERE a.empno=b.main_user) user_name ,c.code_desc stats_desc,d.code_desc own_type_desc ,(SELECT + '{' + '\"name\":' + '\"' +  aa.name + '\"' + ',' + '\"value\":' + '\"' + aa.Mac_Address + '\"' + '},' FROM MacAddress_d aa,Computer_MacAddress bb WHERE aa.id=bb.MacAddress_id AND bb.User_Computer_id =a.ID FOR XML PATH('') )   Mac_Address_d FROM user_Computer a,code_d c,code_d d   WHERE  a.stats=c.code_val AND c.code_kind='Computer_status' AND a.[Own_Type] =d.[code_val]  AND d.code_kind='own_type' AND id=@id";

            V_arr[0, 0] = "@id";
            V_arr[0, 1] = id;

            return pub_lib.Query(strSQL, V_arr).Tables[0];

        }
        public bool DelRegAllComputer(string id,ref string strErrMsg)
        {
            string[,] V_arr = new string[1, 2];
            string strSQL = "UPDATE  user_Computer SET stats='XX' WHERE id=@id";

            V_arr[0, 0] = "@id";
            V_arr[0, 1] = id;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);

        }
        public bool DelMAC(string id, ref string strErrMsg)
        {
            string[,] V_arr = new string[1, 2];
            string strSQL = "UPDATE  MacAddress_d SET stats='XX' WHERE id=@id";

            V_arr[0, 0] = "@id";
            V_arr[0, 1] = id;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        public bool AddMAC_1(string own_type,string HostName,string FajNo,string remark,string UseEmpno,string AddEmpno,ref string strErrMsg)
        {
            string[,] V_arr = new string[6, 2];
            string  strSQL = "INSERT INTO user_Computer (empno, Own_Type,  host_name, FAJ_number,remark,Add_Empno) VALUES ( @empno, @Own_Type,  @host_name, @FAJ_number,@remark ,@Add_Empno) ";

            V_arr[0, 0] = "@empno";
            V_arr[0, 1] = UseEmpno;

            V_arr[1, 0] = "@Own_Type";
            V_arr[1, 1] = own_type;

            V_arr[2, 0] = "@host_name";
            V_arr[2, 1] = HostName;

            V_arr[3, 0] = "@FAJ_number";
            V_arr[3, 1] = FajNo;

            V_arr[4, 0] = "@remark";
            V_arr[4, 1] = remark;

            V_arr[5, 0] = "@Add_Empno";
            V_arr[5, 1] = AddEmpno;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);

        }
        public DataTable AddMac_2(string own_type, string HostName, string FajNo, string remark, string UseEmpno, string AddEmpno)
        {
            string[,] V_arr = new string[6, 2];
           string strSQL = "SELECT * FROM user_Computer WHERE empno=@empno AND Own_Type=@Own_Type AND  host_name=@host_name AND FAJ_number=@FAJ_number  AND Add_Empno=@Add_Empno";
            V_arr[0, 0] = "@empno";
            V_arr[0, 1] = UseEmpno;

            V_arr[1, 0] = "@Own_Type";
            V_arr[1, 1] = own_type;

            V_arr[2, 0] = "@host_name";
            V_arr[2, 1] = HostName;

            V_arr[3, 0] = "@FAJ_number";
            V_arr[3, 1] = FajNo;

            V_arr[4, 0] = "@remark";
            V_arr[4, 1] = remark;

            V_arr[5, 0] = "@Add_Empno";
            V_arr[5, 1] = AddEmpno;

            return pub_lib.Query(strSQL, V_arr).Tables[0];


        }
        public bool AddMAC_3(string name,string Mac_Address,ref string strErrMsg)
        {
            string[,] V_arr = new string[2, 2];
            string strSQL = " INSERT INTO MacAddress_d (name,Mac_Address) VALUES (@name,@Mac_Address) ";

            V_arr[0, 0] = "@name";
            V_arr[0, 1] = name;

            V_arr[1, 0] = "@Mac_Address";
            V_arr[1, 1] = Mac_Address;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);

        }
        public DataTable AddMAC_4(string name, string Mac_Address)
        {
            string[,] V_arr = new string[2, 2];
            string   strSQL = "SELECT * FROM MacAddress_d WHERE name=@name AND Mac_Address=@Mac_Address ";
            V_arr[0, 0] = "@name";
            V_arr[0, 1] = name;

            V_arr[1, 0] = "@Mac_Address";
            V_arr[1, 1] = Mac_Address;

            return pub_lib.Query(strSQL, V_arr).Tables[0]; 
        }
        public bool AddMAC_5(string MacAddress_id,string User_Computer_id,ref string strErrMsg)
        {
            string[,] V_arr = new string[2, 2];
            string  strSQL = " INSERT INTO Computer_MacAddress ( MacAddress_id, User_Computer_id) VALUES ( @MacAddress_id, @User_Computer_id) ";

            V_arr[0, 0] = "@MacAddress_id";
            V_arr[0, 1] = MacAddress_id;

            V_arr[1, 0] = "@User_Computer_id";
            V_arr[1, 1] = User_Computer_id;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        public DataSet CheckMac(string host_name,string FAJ)
        {
            string strSQL = "";
            string[,] V_arr = new string[1, 2];
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataSet ds = new DataSet();
            strSQL = "SELECT * FROM user_Computer WHERE host_name =@Computer_Name AND stats='00' ";
            V_arr[0, 0] = "@Computer_Name";
            V_arr[0, 1] = host_name;
            dt1 = pub_lib.Query(strSQL).Tables[0];
            dt1.TableName = "check1";

            strSQL = "SELECT * FROM user_Computer WHERE FAJ_number =@FAJ AND stats='00' AND FAJ_number NOT IN ('NA','無資產')";
            V_arr[0, 0] = "@FAJ";
            V_arr[0, 1] = FAJ;
            dt2 = pub_lib.Query(strSQL).Tables[0];
            dt2.TableName = "check2";

            ds.Tables.Add(dt1.Copy());
            ds.Tables.Add(dt2.Copy());
            return ds;

        }
        public bool UpdMac_1(string id,string own_type, string HostName, string FajNo, string remark, ref string strErrMsg)
        {
            string[,] V_arr = new string[5, 2];
            string strSQL = "UPDATE  user_Computer SET  Own_Type=@Own_Type,  host_name=@host_name, FAJ_number=@FAJ_number,remark=@remark where id=@id ";


      

            V_arr[0, 0] = "@Own_Type";
            V_arr[0, 1] = own_type;

            V_arr[1, 0] = "@host_name";
            V_arr[1, 1] = HostName;

            V_arr[2, 0] = "@FAJ_number";
            V_arr[2, 1] = FajNo;

            V_arr[3, 0] = "@remark";
            V_arr[3, 1] = remark;

            V_arr[4, 0] = "@id";
            V_arr[4, 1] = id;
            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);

        }
        public bool UpdMac_2(string id, ref string strErrMsg)
        {
            //先全砍 MacAddress_d、Computer_MacAddress
            string[,] V_arr = new string[1, 2];
            string strSQL = "DELETE   MacAddress_d WHERE id IN ( SELECT MacAddress_id FROM  Computer_MacAddress WHERE User_Computer_id=@id )";
            V_arr[0, 0] = "@id";
            V_arr[0, 1] = id;
            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);

        }
        public bool UpdMac_3(string id, ref string strErrMsg)
        {
            //先全砍 MacAddress_d、Computer_MacAddress
            string[,] V_arr = new string[1, 2];
            string strSQL = "DELETE   Computer_MacAddress WHERE User_Computer_id =@id";
            V_arr[0, 0] = "@id";
            V_arr[0, 1] = id;
            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        public bool UpdMac_4(string name ,string Mac_Address,ref string strErrMsg)
        {
            //新增
            string[,] V_arr = new string[2, 2];
            string strSQL = " INSERT INTO MacAddress_d (name,Mac_Address) VALUES (@name,@Mac_Address) ";
            V_arr[0, 0] = "@name";
            V_arr[0, 1] = name;
            V_arr[1, 0] = "@Mac_Address";
            V_arr[1, 1] = Mac_Address;
            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);

        }
        public DataTable UpdMac_5(string name,string Mac_Address)
        {
            string[,] V_arr = new string[2, 2];
            string strSQL = "SELECT * FROM MacAddress_d WHERE name=@name AND Mac_Address=@Mac_Address ";
            V_arr[0, 0] = "@name";
            V_arr[0, 1] = name;
            V_arr[1, 0] = "@Mac_Address";
            V_arr[1, 1] = Mac_Address;

          return  pub_lib.Query(strSQL).Tables[0];
        }
        public bool UpdMac_6(string MacAddress_id, string User_Computer_id,ref string strErrMsg)
        {
            //建立關聯
            string[,] V_arr = new string[2, 2];
           string  strSQL = " INSERT INTO Computer_MacAddress ( MacAddress_id, User_Computer_id) VALUES ( @MacAddress_id, @User_Computer_id) ";

            V_arr[0, 0] = "@MacAddress_id";
            V_arr[0, 1] = MacAddress_id;
            V_arr[1, 0] = "@User_Computer_id";
            V_arr[1, 1] = User_Computer_id;

            return pub_lib.ExecSQL(strSQL, V_arr, ref strErrMsg);
        }
        #endregion
    }
}
