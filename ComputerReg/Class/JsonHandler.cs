using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Text;

public class JsonHandler
{
    public DataTable ParseToDataTable(string strJString)
    {
        System.Xml.Linq.XDocument xdoc = (System.Xml.Linq.XDocument)JsonConvert.DeserializeXNode(strJString, "root");

        DataSet ds = new DataSet();
        System.Xml.XmlReader xmlr;
        xmlr = xdoc.CreateReader();

        string strTmp2 = "";

        ds.ReadXml(xmlr);
        return ds.Tables[0];
        /* DataTable dt = new DataTable();
         int Ii, Jj;
         string[] V_arr;
         string[] V_arr1;
         string[] V_arr2;
         string[] V_arr3;
         int intRowNum = 0;
         if (strJString.Equals(""))
         {
             return new DataTable();
         }
         //去頭去尾
         strJString = strJString.Substring(1, strJString.Length - 2);
         //找出有幾段
         //V_arr = strJString.ToString().Split("},{");
         V_arr =strJString.Split(new []{"},{"},StringSplitOptions.None);
         intRowNum = V_arr.Length - 1;

         V_arr1 = V_arr[0].Split(',');


         //找欄位
         for (Ii = 0; Ii < V_arr1.Length ; Ii++)
         {
             V_arr2 = V_arr1[Ii].Split(':');
             string strColumnName = "";
             strColumnName = V_arr2[0].Substring(1, V_arr2[0].Length - 2); //V_arr2(0).Replace("{", "")
             //剩下 "Message"
             strColumnName = strColumnName.Substring(0, strColumnName.Length - 2);
             dt.Columns.Add(strColumnName, typeof(string));
         }
         //塞值

         for (Ii = 0; Ii < intRowNum ; Ii++)
         {
             V_arr3 = V_arr[Ii].Split(',');
             dt.Rows.Add();
             for (Jj = 0; Jj < V_arr3.Length ; Jj++)
             {
                 string ColumnVal = "";
                 int intS = 0;
                 int intE = 0;
                 intS = V_arr3[Jj].IndexOf("\"");
                 intE = V_arr3[Jj].IndexOf("\"", intS + 1);
                 intS = V_arr3[Jj].IndexOf("\"", intE + 1);
                 intE = V_arr3[Jj].Substring(intS + 1).IndexOf("\"");
                 ColumnVal = V_arr3[Jj].Substring(intS + 1, intE - 1);
                 dt.Rows[dt.Rows.Count - 1][Jj] = ColumnVal;

             }
         }


         return dt;*/
    }
    public DataTable ParseToDataTableTypeII(string strJString)
    {
        DataTable dt = new DataTable();

        return dt;
    }
    public JObject DataTableToJson(DataTable dt, string strHeader = "", string strHeaderText = "")
    {
        int Ii = 0;
        int Jj = 0;
        string strRetuen = "";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        System.Text.StringBuilder JSONString = new System.Text.StringBuilder();
        if (strHeader.Equals(""))
        {
            if (strHeaderText.Equals(""))
            {
                sb.Append("{");
            }
            sb.Append("\"" + (dt.Namespace.Equals("") ? dt.TableName : dt.Namespace) + "\"" + ":[");
            //strRetuen = "{ " & """" & If(dt.Namespace = "", dt.TableName, dt.Namespace) & """" & ":[" '& vbCrLf
        }
        else
        {
            if (!strHeaderText.Equals(""))
            {
                sb.Append("{");
            }
            sb.Append(strHeader + ",\"" + (dt.Namespace.Equals("") ? dt.TableName : dt.Namespace) + "\"" + ":[");
            //strRetuen = "{ " & strHeader & ",""" & If(dt.Namespace = "", dt.TableName, dt.Namespace) & """" & ":[" '& vbCrLf
        }

        //JSONString.Append("{ ")
        for (Ii = 0; Ii < dt.Rows.Count; Ii++)
        {
            for (Jj = 0; Jj < dt.Columns.Count; Jj++)
            {
                if (Jj == 0)
                {
                    sb.Append("{");
                    //strRetuen = strRetuen & "{"
                }
                sb.Append("\"" + dt.Columns[Jj].ColumnName + "\":" + "\"" + dt.Rows[Ii][Jj] + "\"");
                //strRetuen = strRetuen & """" & dt.Columns(Jj).ColumnName & """:" & """" & dt.Rows(Ii).Item(Jj) & """"
                if (Jj != dt.Columns.Count - 1)
                {
                    sb.Append(",");
                    sb.Append("\r\n");
                    //strRetuen = strRetuen & "," & vbCrLf
                }
                else
                {
                    sb.Append("\r\n");
                    //strRetuen = strRetuen & vbCrLf
                }

                if (Jj == dt.Columns.Count - 1)
                {
                    sb.Append("}");
                    //strRetuen = strRetuen & "}"

                }


            }
            if (Ii != dt.Rows.Count - 1)
            {
                sb.Append(",");
                sb.Append("\r\n");
                //strRetuen = strRetuen & "," & vbCrLf
            }



        }
        sb.Append("]");
        if (strHeaderText.Equals(""))
        {
            sb.Append("}");
        }
        //strRetuen = strRetuen & "] }"

        //client.Encoding = System.Text.Encoding.UTF8
        if (strHeaderText.Equals(""))
        {
            return JObject.Parse(sb.ToString());
        }
        else
        {
            return JObject.Parse("{" + strHeaderText + sb.ToString() + "}");
        }

    }

    public string DataTableToJSONWithStringBuilder(DataTable table)
    {
        var JSONString = new StringBuilder();
        //if (table.Rows.Count > 0)
        //{
            JSONString.Append("[");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                JSONString.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j < table.Columns.Count - 1)
                    {
                        JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                    }
                    else if (j == table.Columns.Count - 1)
                    {
                        JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                    }
                }
                if (i == table.Rows.Count - 1)
                {
                    JSONString.Append("}");
                }
                else
                {
                    JSONString.Append("},");
                }
            }
            JSONString.Append("]");
        //}
        return JSONString.ToString();
    }
    public string DataSetToJSONWithStringBuilder(DataSet ds)
    {
        var JSONString = new StringBuilder();
        int Ii;
        for (Ii = 0; Ii < ds.Tables.Count; Ii++)
        {
            if (Ii == 0)
            {
                JSONString.Append("{");
            }
            //if (ds.Tables[Ii].Rows.Count > 0)
            //{
                JSONString.Append("\""+ds.Tables[Ii].TableName + "\"" +":[");
                for (int i = 0; i < ds.Tables[Ii].Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < ds.Tables[Ii].Columns.Count; j++)
                    {
                        if (j < ds.Tables[Ii].Columns.Count - 1)
                        {
                            JSONString.Append("\"" + ds.Tables[Ii].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[Ii].Rows[i][j].ToString() + "\",");
                        }
                        else if (j == ds.Tables[Ii].Columns.Count - 1)
                        {
                            JSONString.Append("\"" + ds.Tables[Ii].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[Ii].Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == ds.Tables[Ii].Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                if (Ii == ds.Tables.Count - 1) {
                    JSONString.Append("]");
                } else {
                    JSONString.Append("],");
                }

            //}
            if (Ii == ds.Tables.Count - 1)
            {
                JSONString.Append("}");
            }
        }
        return JSONString.ToString();
    }
}
