using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DataLibrary
{
    public class clsBusiness
    {
        private string strCN1;
        clsData clsdata;
        public clsBusiness(string strCn)
        {
            clsdata = new clsData(strCn);
        }
        #region RunSQL
        public DataTable RunSQL(string strSQL)
        {
            return clsdata.RunSQL(strSQL);


        }

        #endregion
        #region e-mail log
        public bool AddEmailLog(string strEmpno, string strMailContent, ref string strErrMsg)
        {
            return clsdata.AddEmailLog(strEmpno, strMailContent, ref strErrMsg);
        }
        #endregion
        #region Item_m
        public DataTable QueryItemEmpno(string MID)
        {
            return clsdata.QueryItemEmpno(MID);
        }
        public DataTable QueryItem_m()
        {
            return clsdata.QueryItem_m();
        }

        public DataTable QueryItem_m(string strid)
        {
            return clsdata.QueryItem_m(strid);
        }

        public DataTable QueryItem_m(string strItem_Name, string strSeq, string strstats)
        {
            return clsdata.QueryItem_m(strItem_Name, strSeq, strstats);
        }
        public int CountItem_m(string strItem_Name, string strSeq, string strstats)
        {
            return clsdata.CountItem_m(strItem_Name, strSeq, strstats);
        }
        public DataTable QueryItem_m(string strItem_Name, string strSeq, string strstats, int intPage, int intPageSize)
        {
            return clsdata.QueryItem_m(strItem_Name, strSeq, strstats, intPage, intPageSize);
        }
        public bool AddItem_m(string strItem_Name, string strSeq, string strstats, ref string strErrMsg)
        {
            return clsdata.AddItem_m(strItem_Name, strSeq, strstats, ref strErrMsg);
        }

        public bool UpdItem_m(string strid, string strItem_Name, string strSeq, string strstats, ref string strErrMsg)
        {
            return clsdata.UpdItem_m(strid, strItem_Name, strSeq, strstats, ref strErrMsg);
        }
        public bool DelItem_m(string strid, ref string strErrMsg)
        {
            return clsdata.DelItem_m(strid, ref strErrMsg);
        }
        #endregion

        #region Item_d
        public DataTable QueryItem_d()
        {
            return clsdata.QueryItem_d();
        }
        public DataTable QueryItem_dByEmpno(string strEmpno)
        {
            return clsdata.QueryItem_dByEmpno(strEmpno);
        }
        public DataTable QueryItem_d(string strid)
        {
            return clsdata.QueryItem_d(strid);
        }
        public DataTable QueryItem_dByRelayKey(string strMID)
        {
            return clsdata.QueryItem_dByRelayKey(strMID);
        }

        public DataTable QueryItem_d(string strMID, string strempno, string strItem_No, string strItem_Desc, string strPrice, string strBuyDateTime, string strQty, string strActual_User, string strUsage, string strstats)
        {
            return clsdata.QueryItem_d(strMID, strempno, strItem_No, strItem_Desc, strPrice, strBuyDateTime, strQty, strActual_User, strUsage, strstats);
        }
        public int CountItem_dByRelayKey(string strMID)
        {
            return clsdata.CountItem_dByRelayKey(strMID);
        }
        public DataTable QueryItem_dByRelayKey(string strMID, int intPage, int intPageSize)
        {
            return clsdata.QueryItem_dByRelayKey(strMID, intPage, intPageSize);
        }
        public bool AddItem_d(string strMID, string strempno, string strItem_No, string strItem_Desc, string strPrice, string strBuyDateTime, string strQty, string strActual_User, string strUsage, string strstats, ref string strErrMsg)
        {
            return clsdata.AddItem_d(strMID, strempno, strItem_No, strItem_Desc, strPrice, strBuyDateTime, strQty, strActual_User, strUsage, strstats, ref strErrMsg);
        }
        public bool UpdItem_d(string strid, string strActual_User, string strUsage, ref string strErrMsg)
        {
            return clsdata.UpdItem_d(strid, strActual_User, strUsage, ref strErrMsg);
        }
        public bool UpdItem_d(string strid, string strActual_User, string strUsage, string strstats, ref string strErrMsg)
        {
            return clsdata.UpdItem_d(strid, strActual_User, strUsage, strstats, ref strErrMsg);
        }
        public bool UpdItem_d(string strid, string strMID, string strempno, string strItem_No, string strItem_Desc, string strPrice, string strBuyDateTime, string strQty, string strActual_User, string strUsage, string strstats, ref string strErrMsg)
        {
            return clsdata.UpdItem_d(strid, strMID, strempno, strItem_No, strItem_Desc, strPrice, strBuyDateTime, strQty, strActual_User, strUsage, strstats, ref strErrMsg);
        }
        public bool DelItem_d(string strid, ref string strErrMsg)
        {
            return clsdata.DelItem_d(strid, ref strErrMsg);
        }
        #endregion

        #region GHR
        public DataTable QueryCompany(string strCountry_ID)
        {
            return clsdata.QueryCompany(strCountry_ID);
        }
        public string GetCompany_ID(string strEmpno, string strName)
        {
            return clsdata.GetCompany_ID(strEmpno, strName);
        }
        public DataTable QueryCompanyIDFromEmpno(string strMain_User, bool QueryLayoff = false)
        {
            if (QueryLayoff)
                return clsdata.QueryCompanyIDFromEmpno2(strMain_User);
            else
                return clsdata.QueryCompanyIDFromEmpno(strMain_User);
        }
        public DataTable GetUserInfo(string strAD, string strUserName)
        {
            return clsdata.GetUserInfo(strAD, strUserName);
        }
        public DataTable GetUserInfo(string strEmpno)
        {
            return clsdata.GetUserInfoByEmpno(strEmpno);
        }
        public DataTable GetUserInfoByEmpno(string strEmpno)
        {
            return clsdata.GetUserInfoByEmpnoLike(strEmpno);
        }
        public DataTable GetUserInfoByName(string strEmpno)
        {
            return clsdata.GetUserInfoByName(strEmpno);
        }
        public DataTable GetUserInfoByEmpnoEqual(string strEmpno)
        {
            return clsdata.GetUserInfoByEmpnoEqual(strEmpno);

        }
        public DataTable GetUserInfoByEmail(string strEmail)
        {
            return clsdata.GetUserInfoByEmail(strEmail);

        }
        #endregion

        #region Code_d

        public DataTable QueryCode_dByID(string strID)
        {
            return clsdata.QueryCode_dByID(strID);
        }
        public DataTable QueryCode_d(string strCodeKind)
        {
            return clsdata.QueryCode_d(strCodeKind);
        }
        public DataTable QueryCode_d(string strCodeKind, string strOrderColumn)
        {
            return clsdata.QueryCode_d(strCodeKind, strOrderColumn);
        }
        public bool UpdCode_d(string strCodeKind, string strCode_val, ref string strErrMsg)
        {
            return clsdata.UpdCode_d(strCodeKind, strCode_val, ref strErrMsg);
        }
        public bool UpdCode_d(string strCodeKind, string strCode_val, string strAttr1, ref string strErrMsg)
        {
            return clsdata.UpdCode_d(strCodeKind, strCode_val, strAttr1, ref strErrMsg);
        }
        public DataTable GenOptionsFromCode_dToDt(string strCodeKind)
        {
            int Ii;
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("value");
            dt2.Columns.Add("text");
            dt = clsdata.QueryOptionsFromCode_d(strCodeKind);
            for (Ii = 0; Ii <= dt.Rows.Count - 1; Ii++)
            {
                dt2.Rows.Add();
                dt2.Rows[dt2.Rows.Count - 1]["value"] = dt.Rows[Ii]["code_val"];
                dt2.Rows[dt2.Rows.Count - 1]["text"] = dt.Rows[Ii]["code_desc"];
            }


            return dt2;
        }
        public string GenOptionsFromCode_d(string strCodeKind)
        {
            int Ii;
            DataTable dt = new DataTable();
            var strReturn = "";
            dt = clsdata.QueryOptionsFromCode_d(strCodeKind);
            for (Ii = 0; Ii < dt.Rows.Count; Ii++)
                strReturn = strReturn + "<option value='" + dt.Rows[Ii]["Code_Val"].ToString().Trim() + "'>" + dt.Rows[Ii]["Code_desc"].ToString().Trim() + "</option>" + "\r\n";
            return strReturn;
        }
        public string GenOptionsFromCode_dWithAll(string strCodeKind)
        {
            int Ii;
            DataTable dt = new DataTable();
            var strReturn = "";
            dt = clsdata.QueryOptionsFromCode_d(strCodeKind);
            strReturn = strReturn + "<option value=''>全部</option>";
            for (Ii = 0; Ii <= dt.Rows.Count - 1; Ii++)
                strReturn = strReturn + "<option value='" + dt.Rows[Ii]["Code_Val"].ToString().Trim() + "'>" + dt.Rows[Ii]["Code_desc"].ToString().Trim() + "</option>" + "\r\n";
            return strReturn;
        }
        public string GenOptionsFromCode_dWithOrder(string strCodeKind)
        {
            int Ii;
            DataTable dt = new DataTable();
            var strReturn = "";
            dt = clsdata.QueryOptionsFromCode_dWithOrder(strCodeKind);
            for (Ii = 0; Ii <= dt.Rows.Count - 1; Ii++)
                strReturn = strReturn + "<option value='" + dt.Rows[Ii]["Code_Val"].ToString().Trim() + "'>" + dt.Rows[Ii]["Code_desc"].ToString().Trim() + "</option>" + "\r\n";
            return strReturn;
        }
        public string GenOptionsFromCode_dWithOrder(string strCodeKind, string strSelectValue)
        {
            int Ii;
            DataTable dt = new DataTable();
            string strReturn = "";
            string strSelected;
            dt = clsdata.QueryOptionsFromCode_dWithOrder(strCodeKind);
            for (Ii = 0; Ii <= dt.Rows.Count - 1; Ii++)
            {
                strSelected = "";
                if (strSelectValue == dt.Rows[Ii]["Code_Val"].ToString().Trim())
                    strSelected = "selected";
                strReturn = strReturn + "<option value='" + dt.Rows[Ii]["Code_Val"].ToString().Trim() + "'  " + strSelected + ">" + dt.Rows[Ii]["Code_desc"].ToString().Trim() + "</option>" + "\r\n";
            }
            return strReturn;
        }
        public string GenOptionsFromCode_d(string strCodeKind, string strSelectValue)
        {
            int Ii;
            DataTable dt = new DataTable();
            var strReturn = "";
            string strSelected;
            dt = clsdata.QueryOptionsFromCode_d(strCodeKind);
            for (Ii = 0; Ii <= dt.Rows.Count - 1; Ii++)
            {
                strSelected = "";
                if (strSelectValue == dt.Rows[Ii]["Code_Val"].ToString().Trim())
                    strSelected = "selected";

                strReturn = strReturn + "<option value='" + dt.Rows[Ii]["Code_Val"].ToString().Trim() + "' " + strSelected + " >" + dt.Rows[Ii]["Code_desc"].ToString().Trim() + "</option>" + "\r\n";
            }
            return strReturn;
        }
        /// <summary>
        ///     ''' 
        ///     ''' </summary>
        ///     ''' <param name="strTableName"></param>
        ///     ''' <param name="strDisplayColumn"></param>
        ///     ''' <param name="strValueColumn"></param>
        ///     ''' <param name="strWhere"></param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public string QueryOptionsFromTableWithWhere(string strTableName, string strDisplayColumn, string strValueColumn, string strWhere, string strOrderColumn = "", string strSelectValue = "")
        {
            int Ii;
            DataTable dt = new DataTable();
            var strReturn = "";
            string Selected;
            // If strOrderColumn = "" Then
            dt = clsdata.QueryOptionsFromTableWithWhere(strTableName, strDisplayColumn, strValueColumn, strWhere, strOrderColumn);
            // Else
            // dt = clsdata.QueryOptionsFromTableWithWhere(strTableName, strDisplayColumn, strValueColumn, strOrderColumn)
            // End If

            for (Ii = 0; Ii <= dt.Rows.Count - 1; Ii++)
            {
                Selected = "";
                if (strSelectValue == dt.Rows[Ii][strValueColumn].ToString().Trim())
                    Selected = "selected";

                strReturn = strReturn + "<option value='" + dt.Rows[Ii][strValueColumn].ToString().Trim() + "' " + Selected + " >" + dt.Rows[Ii][strDisplayColumn].ToString().Trim() + "</option>" + "\r\n";
            }
            return strReturn;
        }
        /// <summary>
        ///     ''' 從別的Table產生出Options
        ///     ''' </summary>
        ///     ''' <param name="strTableName">Table名稱</param>
        ///     ''' <param name="strDisplayColumn">顯示欄位名稱</param>
        ///     ''' <param name="strValueColumn">Option的值</param>
        ///     ''' <param name="strOrderColumn">選擇性填入：排序欄位，預設空</param>
        ///     ''' <param name="strSelectValue">選擇性填入：Select 預設值，預設空</param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public string GenOptionsFromTable(string strTableName, string strDisplayColumn, string strValueColumn, string strOrderColumn = "", string strSelectValue = "")
        {
            int Ii;
            DataTable dt = new DataTable();
            var strReturn = "";
            string Selected;
            if (strOrderColumn == "")
                dt = clsdata.QueryOptionsFromTable(strTableName, strDisplayColumn, strValueColumn);
            else
                dt = clsdata.QueryOptionsFromTable(strTableName, strDisplayColumn, strValueColumn, strOrderColumn);

            for (Ii = 0; Ii <= dt.Rows.Count - 1; Ii++)
            {
                Selected = "";
                if (strSelectValue == dt.Rows[Ii][strValueColumn].ToString().Trim())
                    Selected = "selected";

                strReturn = strReturn + "<option value='" + dt.Rows[Ii][strValueColumn].ToString().Trim() + "' " + Selected + " >" + dt.Rows[Ii][strDisplayColumn].ToString().Trim() + "</option>" + "\r\n";
            }
            return strReturn;
        }
        public string GenCheckBoxFromTable(string strTable, string strColumn, string strWhere, string strControlHeader, string strValueColumn, string strDisplayColumn, string[] strCheck = null)
        {
            DataTable dt = new DataTable();
            int Ii, Jj, Kk;
            string strHtml;
            bool bolCheck;
            Kk = 0;
            dt = clsdata.GenCheckBoxFromTable(strTable, strColumn, strWhere);
            strHtml = "";
            if (strCheck == null)
            {
                for (Ii = 0; Ii <= dt.Rows.Count - 1; Ii++)
                {
                    strHtml = strHtml + "<input type=\"checkbox\" id=\"" + strControlHeader + "_" + dt.Rows[Ii][strValueColumn] + "\"  value=\"" + dt.Rows[Ii][strValueColumn] + "\">" + dt.Rows[Ii][strDisplayColumn] + " ";
                    Kk = Kk + 1;
                    if (Kk > 3)
                    {
                        strHtml = strHtml + "<br>";
                        Kk = 0;
                    }
                }
            }
            else
                for (Ii = 0; Ii <= dt.Rows.Count - 1; Ii++)
                {
                    bolCheck = false;
                    for (Jj = 0; Jj <= strCheck.Length - 1; Jj++)
                    {
                        if (dt.Rows[Ii][strValueColumn].ToString() == strCheck[Jj].ToString())
                        {
                            bolCheck = true;
                            break;
                        }
                        else
                        {
                        }
                    }
                    strHtml = strHtml + "<input type=\"checkbox\" id=\"" + strControlHeader + "_" + dt.Rows[Ii][strValueColumn].ToString() + "\"  value=\"" + dt.Rows[Ii][strValueColumn] + "\" " + (bolCheck ? " CHECKED " : "") + " >" + dt.Rows[Ii][strDisplayColumn] + "";
                    Kk = Kk + 1;
                    if (Kk > 3)
                    {
                        strHtml = strHtml + "<br>";
                        Kk = 0;
                    }
                }


            return strHtml;
        }

        #endregion
        #region menu
        public DataTable GetPrgInfo(string strFileName)
        {
            return clsdata.GetPrgInfo(strFileName);
        }
        public string GetPrgTitle(string strPrgName)
        {
            return clsdata.GetPrgTitle(strPrgName);
        }
        public string GetMenuTitle(string strPrgName)
        {
            return clsdata.GetMenuTitle(strPrgName);
        }
        /// <summary>
        ///     ''' 取得menu的HTML標籤
        ///     ''' </summary>
        ///     ''' <param name="strEMPNO">員工編號</param>
        ///     ''' <param name="strErrMsg">錯誤訊息</param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public string GetAllMenu(string strAD, string strEMPNO, string strCurrent, string strPath, string strShowNoLogout, string isCnUser, ref string strErrMsg)
        {
            DataTable DT_first = new DataTable();
            // Dim DT_sec As New DataTable
            // Dim DT As New DataTable
            string strHTML;
            bool bolSingle = false;
            int Ii, Jj, Kk, intPrgID;
            DataTable dtPath = new DataTable();
            int intMID, intDID;
            bool bolOpenMaster = false;
            DataView dv;

            //// 如果有帶部門，就新增主管權限給他，如果沒有，就移除主管權限
            //if ((clsData.IfIsDeptLeader(strEMPNO)))
            //{
            //    if ((clsData.ISLeaderGrant(strEMPNO) == 0))
            //        clsData.AddLeaderGrant(strEMPNO, strErrMsg);
            //}
            //else if ((clsData.ISLeaderGrant(strEMPNO) > 0))
            //    clsData.RemoveLeaderGrant(strEMPNO, strErrMsg);
            //// 如果沒有權限，塞一般人給他
            //if ((clsData.ISNormalGrant(strEMPNO) == 0))
            //{
            //    // 如果是中嘉登入
            //    if (isCnUser == "Y")
            //        clsData.AddGroupToUser(7, strAD, strEMPNO, strErrMsg);
            //    else
            //        clsData.AddGroupToUser(2, strAD, strEMPNO, strErrMsg);
            //}

            DT_first = clsdata.GetMenu(strAD, strEMPNO, -1, ref strErrMsg);
            if (DT_first.Rows.Count == 0)
            {
                clsdata.AddGroupToUser(2, strAD, strEMPNO, ref strErrMsg);
            }
            intMID = 0;
            intDID = 0;
            dtPath = clsdata.GetPrgInfo(strCurrent);
            if (dtPath.Rows.Count > 0)
            {
                intMID = Convert.ToInt32(dtPath.Rows[0]["MenuID"].ToString());
                intDID = Convert.ToInt32(dtPath.Rows[0]["PrgSerno"].ToString());
            }

            // DT_first = clsData.GetMenu(strAD, strEMPNO, -1, strErrMsg)
            DT_first = clsdata.GetMenu(strEMPNO);

            // DT_first = clsData.GetMenu(-1, strErrMsg)


            strHTML = "<div class=\"menu_section\">" + "\r\n";
            //strHTML = strHTML + "<h3>General</h3>";
            strHTML = strHTML + "<ul class=\"nav side-menu\">" + "\r\n";
            strHTML = strHTML + "" + "\r\n";
            // DT = DT_first
            for (Ii = 0; Ii <= DT_first.Rows.Count - 1; Ii++)
            {
                if (!DT_first.Rows[Ii]["PrgPath"].ToString().Equals(""))
                    continue;
                intPrgID = Convert.ToInt32(DT_first.Rows[Ii]["PrgSerNo"].ToString());

                if ((DT_first.Rows.Count == 1))
                    bolSingle = true;
                bolOpenMaster = false;
                if (Convert.ToInt32(DT_first.Rows[Ii]["PrgSerNo"].ToString()) == intMID)
                    bolOpenMaster = true;

                bolSingle = false;
                // for(Ii=0;Ii<dataMenu.size();Ii++){

                if ((bolOpenMaster))
                {
                    strHTML = strHTML + "    <li class=\"active\"><a><i class=\"" + DT_first.Rows[Ii][5].ToString() + "\"></i>" + DT_first.Rows[Ii][4].ToString() + "<span class=\"fa fa-chevron-down\"></span></a>" + "\r\n";
                }
                else
                {
                    strHTML = strHTML + "    <li><a><i class=\"" + DT_first.Rows[Ii][5].ToString() + "\"></i>" + DT_first.Rows[Ii][4].ToString() + "<span class=\"fa fa-chevron-down\"></span></a>" + "\r\n";
                    //strHTML = strHTML + " <ul class=\"sub-menu\">" + "\r\n";
                }
                strHTML = strHTML + " <ul class=\"nav child_menu\" " + (bolOpenMaster ? "style=\"display: block;\"" : "") + ">" + "\r\n";
                dv = DT_first.DefaultView;
                dv.RowFilter = " SeqNo='" + DT_first.Rows[Ii]["SeqNo"].ToString() + "'";

                // DT_sec = clsData.GetMenu(strAD, strEMPNO, DT_first.Rows[Ii]["PrgSerNo"), strErrMsg)


                // dataMenu2=db_helper.Query(strSQL,V_arr2);
                for (Jj = 0; Jj < dv.ToTable().Rows.Count; Jj++)   // DT_sec.Rows.Count - 1
                {
                    if (dv.ToTable().Rows[Jj]["PrgPath"].ToString().Trim().Equals(""))
                        continue;

                    strHTML = strHTML + "<li " + (intDID == Convert.ToInt32(dv.ToTable().Rows[Jj]["PrgSerNo"].ToString()) ? "class=\"current-page\"" : "") + "> <a href='" + strPath + dv.ToTable().Rows[Jj][1].ToString() + "?t=" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "'>" + dv.ToTable().Rows[Jj][4].ToString() + "</a> </li>" + "\r\n";
                }

                strHTML = strHTML + "</ul>" + "\r\n";
                strHTML = strHTML + "</li>    " + "\r\n";
            }
            strHTML = strHTML + "</ul>" + "\r\n";
            strHTML = strHTML + "</div>" + "\r\n";
            /* strHTML = strHTML + "<li class=\"active\"> ";
             if (strShowNoLogout != "Y")
                 strHTML = strHTML + "<a href=\"#\" onclick=\"AskLogout();\"> <i class=\"fa fa-sign-out\"></i> <span class=\"title\">登出</span> <span class=\"selected\"></span>  </a> " + "\r\n";

             */
            return strHTML;
        }
        #endregion
        #region Response_d
        public DataTable QueryResponse_d()
        {
            return clsdata.QueryResponse_d();
        }
        public string QueryResponse_dByCode(string strCode)
        {
            DataTable dt = new DataTable();
            dt = clsdata.QueryResponse_dByCode(strCode);
            try
            {
                return dt.Rows[0]["Response_String"].ToString();
            }
            catch (Exception ex)
            {
            }
            return "";
        }
        public DataTable QueryResponse_d(string strid)
        {
            return clsdata.QueryResponse_d(strid);
        }

        public DataTable QueryResponse_d(string strResponse_Name, string strResponse_Desc, string strResponse_Code, string strResponse_String)
        {
            return clsdata.QueryResponse_d(strResponse_Name, strResponse_Desc, strResponse_Code, strResponse_String);
        }
        public int CountResponse_d(string strResponse_Name, string strResponse_Desc, string strResponse_Code, string strResponse_String)
        {
            return clsdata.CountResponse_d(strResponse_Name, strResponse_Desc, strResponse_Code, strResponse_String);
        }
        public DataTable QueryResponse_d(string strResponse_Name, string strResponse_Desc, string strResponse_Code, string strResponse_String, int intPage, int intPageSize)
        {
            return clsdata.QueryResponse_d(strResponse_Name, strResponse_Desc, strResponse_Code, strResponse_String, intPage, intPageSize);
        }
        public bool AddResponse_d(string strResponse_Name, string strResponse_Desc, string strResponse_Code, string strResponse_String, ref string strErrMsg)
        {
            return clsdata.AddResponse_d(strResponse_Name, strResponse_Desc, strResponse_Code, strResponse_String, ref strErrMsg);
        }

        public bool UpdResponse_d(string strid, string strResponse_Name, string strResponse_Desc, string strResponse_Code, string strResponse_String, ref string strErrMsg)
        {
            return clsdata.UpdResponse_d(strid, strResponse_Name, strResponse_Desc, strResponse_Code, strResponse_String, ref strErrMsg);
        }
        public bool DelResponse_d(string strid, ref string strErrMsg)
        {
            return clsdata.DelResponse_d(strid, ref strErrMsg);
        }
        #endregion
        #region API-登入資訊
        public bool IsAppGrant(string strAppName, string strKey)
        {
            if (clsdata.IsAppGrant(strAppName, strKey) > 0)
                return true;
            return false;
        }
        public bool AddLoginFile(string empno, string MobileSystem, string PackageName, string VersionName, string VersionCode, string ServerKey, string DeviceId, string Model, ref string strErrMsg)
        {
            clsdata.DeleteLoginFile(empno, ref strErrMsg);

            return clsdata.AddLoginFile(empno, MobileSystem, PackageName, VersionName, VersionCode, ServerKey, DeviceId, Model, ref strErrMsg);
        }
        public bool CheckVerson(string ServerKey)
        {
            if (clsdata.CheckVerson(ServerKey).Rows.Count > 0)
                return true;
            return false;
        }
        public bool CheckLogin(string ServerKey)
        {
            if (clsdata.CheckLogin(ServerKey).Rows.Count > 0)
                return true;
            return false;
        }
        #endregion
        #region 離職員工
        public bool AddQuitEmpno(string strEmpno, string strInsid, ref string strErrMsg)
        {
            return clsdata.AddQuitEmpno(strEmpno, strInsid, ref strErrMsg);
        }
        #endregion
        #region 寫入tiptop
        
        #endregion
    }
}
