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

        public DataTable GetMenu(string strEmpno)
        {
            return clsdata.GetMenu(strEmpno);
        }
        /// <summary>
        ///     ''' 取得menu的HTML標籤
        ///     ''' </summary>
        ///     ''' <param name="strEMPNO">員工編號</param>
        ///     ''' <param name="strErrMsg">錯誤訊息</param>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        //public string GetAllMenu(string strAD, string strEMPNO, string strCurrent, string strPath, string strShowNoLogout, string isCnUser, ref string strErrMsg)
        //{
        //    DataTable DT_first = new DataTable();
        //    // Dim DT_sec As New DataTable
        //    // Dim DT As New DataTable
        //    string strHTML;
        //    bool bolSingle = false;
        //    int Ii, Jj, Kk, intPrgID;
        //    DataTable dtPath = new DataTable();
        //    int intMID, intDID;
        //    bool bolOpenMaster = false;
        //    DataView dv;

        //    //// 如果有帶部門，就新增主管權限給他，如果沒有，就移除主管權限
        //    //if ((clsData.IfIsDeptLeader(strEMPNO)))
        //    //{
        //    //    if ((clsData.ISLeaderGrant(strEMPNO) == 0))
        //    //        clsData.AddLeaderGrant(strEMPNO, strErrMsg);
        //    //}
        //    //else if ((clsData.ISLeaderGrant(strEMPNO) > 0))
        //    //    clsData.RemoveLeaderGrant(strEMPNO, strErrMsg);
        //    //// 如果沒有權限，塞一般人給他
        //    //if ((clsData.ISNormalGrant(strEMPNO) == 0))
        //    //{
        //    //    // 如果是中嘉登入
        //    //    if (isCnUser == "Y")
        //    //        clsData.AddGroupToUser(7, strAD, strEMPNO, strErrMsg);
        //    //    else
        //    //        clsData.AddGroupToUser(2, strAD, strEMPNO, strErrMsg);
        //    //}

        //    DT_first = clsdata.GetMenu(strAD, strEMPNO, -1, ref strErrMsg);
        //    if (DT_first.Rows.Count == 0)
        //    {
        //        clsdata.AddGroupToUser(2, strAD, strEMPNO, ref strErrMsg);
        //    }
        //    intMID = 0;
        //    intDID = 0;
        //    dtPath = clsdata.GetPrgInfo(strCurrent);
        //    if (dtPath.Rows.Count > 0)
        //    {
        //        intMID = Convert.ToInt32(dtPath.Rows[0]["MenuID"].ToString());
        //        intDID = Convert.ToInt32(dtPath.Rows[0]["PrgSerno"].ToString());
        //    }

        //    // DT_first = clsData.GetMenu(strAD, strEMPNO, -1, strErrMsg)
        //    DT_first = clsdata.GetMenu(strEMPNO);

        //    // DT_first = clsData.GetMenu(-1, strErrMsg)


        //    strHTML = "<div class=\"menu_section\">" + "\r\n";
        //    //strHTML = strHTML + "<h3>General</h3>";
        //    strHTML = strHTML + "<ul class=\"nav side-menu\">" + "\r\n";
        //    strHTML = strHTML + "" + "\r\n";
        //    // DT = DT_first
        //    for (Ii = 0; Ii <= DT_first.Rows.Count - 1; Ii++)
        //    {
        //        if (!DT_first.Rows[Ii]["PrgPath"].ToString().Equals(""))
        //            continue;
        //        intPrgID = Convert.ToInt32(DT_first.Rows[Ii]["PrgSerNo"].ToString());

        //        if ((DT_first.Rows.Count == 1))
        //            bolSingle = true;
        //        bolOpenMaster = false;
        //        if (Convert.ToInt32(DT_first.Rows[Ii]["PrgSerNo"].ToString()) == intMID)
        //            bolOpenMaster = true;

        //        bolSingle = false;
        //        // for(Ii=0;Ii<dataMenu.size();Ii++){

        //        if ((bolOpenMaster))
        //        {
        //            strHTML = strHTML + "    <li class=\"active\"><a><i class=\"" + DT_first.Rows[Ii][5].ToString() + "\"></i>" + DT_first.Rows[Ii][4].ToString() + "<span class=\"fa fa-chevron-down\"></span></a>" + "\r\n";
        //        }
        //        else
        //        {
        //            strHTML = strHTML + "    <li><a><i class=\"" + DT_first.Rows[Ii][5].ToString() + "\"></i>" + DT_first.Rows[Ii][4].ToString() + "<span class=\"fa fa-chevron-down\"></span></a>" + "\r\n";
        //            //strHTML = strHTML + " <ul class=\"sub-menu\">" + "\r\n";
        //        }
        //        strHTML = strHTML + " <ul class=\"nav child_menu\" " + (bolOpenMaster ? "style=\"display: block;\"" : "") + ">" + "\r\n";
        //        dv = DT_first.DefaultView;
        //        dv.RowFilter = " SeqNo='" + DT_first.Rows[Ii]["SeqNo"].ToString() + "'";

        //        // DT_sec = clsData.GetMenu(strAD, strEMPNO, DT_first.Rows[Ii]["PrgSerNo"), strErrMsg)


        //        // dataMenu2=db_helper.Query(strSQL,V_arr2);
        //        for (Jj = 0; Jj < dv.ToTable().Rows.Count; Jj++)   // DT_sec.Rows.Count - 1
        //        {
        //            if (dv.ToTable().Rows[Jj]["PrgPath"].ToString().Trim().Equals(""))
        //                continue;

        //            strHTML = strHTML + "<li " + (intDID == Convert.ToInt32(dv.ToTable().Rows[Jj]["PrgSerNo"].ToString()) ? "class=\"current-page\"" : "") + "> <a href='" + strPath + dv.ToTable().Rows[Jj][1].ToString() + "?t=" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "'>" + dv.ToTable().Rows[Jj][4].ToString() + "</a> </li>" + "\r\n";
        //        }

        //        strHTML = strHTML + "</ul>" + "\r\n";
        //        strHTML = strHTML + "</li>    " + "\r\n";
        //    }
        //    strHTML = strHTML + "</ul>" + "\r\n";
        //    strHTML = strHTML + "</div>" + "\r\n";
        //    /* strHTML = strHTML + "<li class=\"active\"> ";
        //     if (strShowNoLogout != "Y")
        //         strHTML = strHTML + "<a href=\"#\" onclick=\"AskLogout();\"> <i class=\"fa fa-sign-out\"></i> <span class=\"title\">登出</span> <span class=\"selected\"></span>  </a> " + "\r\n";

        //     */
        //    return strHTML;
        //}
        #endregion
        #region "登入"
        public DataTable Login(string Type, string AD, string Account, string Pass)
        {
            if (Type.Equals("1"))
            {
                return clsdata.LoginAD(AD, Account, Pass);
            }
            else
            {
                return clsdata.LoginLDAP(Account, Pass);
            }
        }
        #endregion
        #region "Group"
        public DataTable QueryAllGroup()
        {
            return clsdata.QueryAllGroup();
        }
        public bool AddGroup(string GROUPNO, string GroupDesc, string GroupName, ref string strErrMsg)
        {
            return clsdata.AddGroup(GROUPNO, GroupDesc, GroupName, ref strErrMsg);
        }
        public bool UPDGroup(string SeqNo, string GROUPNO, string GroupDesc, string GroupName, ref string strErrMsg)
        {
            return clsdata.UPDGroup(SeqNo, GROUPNO, GroupDesc, GroupName, ref strErrMsg);
        }
        public bool DELGroup(string SeqNo, ref string strErrMsg)
        {
            return clsdata.DELGroup(SeqNo, ref strErrMsg);
        }
        public DataSet QRYGP(string GROUPNO)
        {
            return clsdata.QRYGP(GROUPNO);
        }
        public bool AddGP(string PrgSerNo, string GROUPNO, ref string strErrMsg)
        {
            return clsdata.AddGP(PrgSerNo, GROUPNO, ref strErrMsg);
        }
        public bool DelGP(string SeqNo, ref string strErrMsg)
        {
            return clsdata.DelGP(SeqNo, ref strErrMsg);
        }
        public DataTable QRYGU(string GROUPNO)
        {
            return clsdata.QRYGU(GROUPNO);
        }
        public bool ADDGU(string GROUPNO, string EMPNO, string AD, ref string strErrMsg)
        {
            return clsdata.ADDGU(GROUPNO, EMPNO, AD, ref strErrMsg);
        }
        public bool DELGU(string SerNo, ref string strErrMsg)
        {
            return clsdata.DELGU(SerNo, ref strErrMsg);
        }
        public DataTable Q1UserGroup(string SeqNo)
        {
            return clsdata.Q1UserGroup(SeqNo);
        }
        #endregion
        #region Category
        public DataTable QryCategory()
        {
            return clsdata.QryCategory();
        }
        public bool AddCategory(string PrgPath, string CategoryDesc, string PrgName, string MasterNo, string SeqNo, ref string strErrMsg)
        {
            return clsdata.AddCategory(PrgPath, CategoryDesc, PrgName, MasterNo, SeqNo, ref strErrMsg);

        }
        public bool UpdCategory(string PrgSerNo, string PrgPath, string CategoryDesc, string PrgName, string MasterNo, string SeqNo, ref string strErrMsg)
        {
            return clsdata.UpdCategory(PrgSerNo, PrgPath, CategoryDesc, PrgName, MasterNo, SeqNo, ref strErrMsg);

        }
        public bool DelCategory(string PrgSerNo, ref string strErrMsg)
        {
            return clsdata.DelCategory(PrgSerNo, ref strErrMsg);

        }
        public DataTable Q1Category(string PrgSerNo)
        {
            return clsdata.Q1Category(PrgSerNo);

        }
        public DataTable QueryMenuCategory()
        {
            return clsdata.QueryMenuCategory();
        }
        #endregion
        #region RegAllComputer
        public DataSet InitRegAllComputer()
        {
            return clsdata.InitRegAllComputer();
        }
        public bool AddRegAllComputer(string empno, string Computer_Name, string fingerprint, string host_name, ref string strErrMsg)
        {
            return clsdata.AddRegAllComputer(empno, Computer_Name, fingerprint, host_name, ref strErrMsg);
        }
        public bool UpdRegAllComputer(string Computer_Name, string fingerprint, string host_name, string id, ref string strErrMsg)
        {
            return clsdata.UpdRegAllComputer(Computer_Name, fingerprint, host_name, id, ref strErrMsg);
        }
        public DataSet QryRegAllComputer(string Empno, string own_type, string stats, string Mac, string FAJ, int Page)
        {
            int intTotal = clsdata.QryCountRegAllComputer(Empno, own_type, stats, Mac, FAJ);
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataSet ds = new DataSet();
            dt = clsdata.QryRegAllComputer(Empno, own_type, stats, Mac, FAJ, Page);
            dt.TableName = "Data";

            dt2.Columns.Add("TotalCount", typeof(int));
            dt2.Rows.Add();
            dt2.Rows[0]["TotalCount"] = intTotal;
            dt2.TableName = "TotalPages";


            ds.Tables.Add(dt.Copy());
            ds.Tables.Add(dt2.Copy());

            return ds;
        }
        public DataTable Q1RegAllComputer(string id)
        {
            return clsdata.Q1RegAllComputer(id);
        }
        public bool DelRegAllComputer(string id,ref string strErrMsg)
        {
            return clsdata.DelRegAllComputer(id,ref strErrMsg);
        }
        public bool DelMac(string id,ref string strErrMsg)
        {
            return clsdata.DelMAC (id,ref strErrMsg);
        }
        public bool AddMac(string own_type, string HostName, string FajNo, string remark, string UseEmpno, string AddEmpno, DataTable dtMac,ref string strErrMsg)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            int Ii = 0;
            if(!clsdata.AddMAC_1(own_type, HostName, FajNo, remark,UseEmpno,AddEmpno,ref strErrMsg))
            {
                throw new Exception(strErrMsg);
            }
            //找ID
            dt1 = clsdata.AddMac_2(own_type, HostName, FajNo, remark, UseEmpno, AddEmpno);
            for (Ii = 0; Ii < dtMac.Rows.Count; Ii++)
            {
                if(!clsdata.AddMAC_3(dtMac.Rows[Ii]["name"].ToString(), dtMac.Rows[Ii]["Mac_Address"].ToString(),ref strErrMsg))
                {
                    throw new Exception(strErrMsg);
                }
                //找ID
                dt2 = clsdata.AddMAC_4(dtMac.Rows[Ii]["name"].ToString(), dtMac.Rows[Ii]["Mac_Address"].ToString());

                if(!clsdata.AddMAC_5(dt2.Rows[0]["id"].ToString(),dt1.Rows[0]["id"].ToString(),ref strErrMsg))
                {
                    throw new Exception(strErrMsg);

                }

            }
            return true;


        }
        public DataSet CheckMac(string host_name, string FAJ)
        {
            return clsdata.CheckMac(host_name, FAJ);
        }
        public bool UpdMac(string id, string own_type, string HostName, string FajNo, string remark, DataTable dtMac,ref string strErrMsg)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            int Ii = 0;
            if(!clsdata.UpdMac_1(id,own_type,HostName,FajNo,remark,ref strErrMsg))
            {
                throw new Exception(strErrMsg);
            }
            if(!clsdata.UpdMac_2(id,ref strErrMsg))
            {
                throw new Exception(strErrMsg);
            }
            if (!clsdata.UpdMac_3(id, ref strErrMsg))
            {
                throw new Exception(strErrMsg);
            }
            for (Ii = 0; Ii < dtMac.Rows.Count; Ii++)
            {
                if (!clsdata.UpdMac_4(dtMac.Rows[Ii]["name"].ToString(), dtMac.Rows[Ii]["Mac_Address"].ToString(), ref strErrMsg))
                {
                    throw new Exception(strErrMsg);
                }
                dt2 = clsdata.UpdMac_5(dtMac.Rows[Ii]["name"].ToString(), dtMac.Rows[Ii]["Mac_Address"].ToString());
                if(!clsdata.UpdMac_6(dt2.Rows[0]["id"].ToString(),id,ref strErrMsg))
                {
                    throw new Exception(strErrMsg);
                }
            }
            return true;
       }

        #endregion
    }
}
