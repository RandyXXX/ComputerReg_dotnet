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
 
    }
}
