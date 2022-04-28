using System;
using System.Data.SqlClient;
using System.Data;
// Imports System.Data.OracleClient
// Imports System.Data

public class PUB_LIB
{
    private string strCN1;
    // Public Property strCN() As String
    // Get
    // strCN = strCN1
    // End Get
    // Set(ByVal value As String)
    // strCN1 = value
    // End Set
    // End Property
    /// <summary>
    ///     ''' 建構子，告知連線字串
    ///     ''' </summary>
    ///     ''' <param name="strCn">連線字串</param>
    ///     ''' <remarks></remarks>
    public PUB_LIB(string strCn)
    {
        strCN1 = strCn;
    }

    public DataSet Query(string strSQL)
    {
        // Dim strSQL As String
        SqlConnection CN = new SqlConnection();
        CN.ConnectionString = strCN1;
        SqlDataAdapter DA = new SqlDataAdapter();
        SqlCommand CMD = new SqlCommand();
        DataSet DS = new DataSet();
        try
        {
            CMD.CommandText = strSQL;

            CMD.Connection = CN;
            DA.SelectCommand = CMD;

            DA.Fill(DS);
            return DS;
        }
        catch (Exception ex)
        {
            throw new Exception("Error:" + ex.Message);
        }
    }
    public DataSet Query(string strSQL, int intBegin, int intPageSize)
    {
        // Dim strSQL As String
        SqlConnection CN = new SqlConnection();
        CN.ConnectionString = strCN1;
        SqlDataAdapter DA = new SqlDataAdapter();
        SqlCommand CMD = new SqlCommand();
        DataSet DS = new DataSet();
        try
        {
            CMD.CommandText = strSQL;
            CMD.CommandTimeout = 5000000;
            CMD.Connection = CN;
            DA.SelectCommand = CMD;

            DA.Fill(DS, intBegin, intPageSize, "Table0");
            return DS;
        }
        catch (Exception ex)
        {
            throw new Exception("Error:" + ex.Message);
        }
    }
    public DataSet Query(string strSQL, object[,] V_arr)
    {
        SqlConnection CN = new SqlConnection();
        CN.ConnectionString = strCN1;
        SqlDataAdapter DA = new SqlDataAdapter();
        SqlCommand CMD = new SqlCommand();
        DataSet DS = new DataSet();

        try
        {
            CMD.Connection = CN;
            CMD.CommandText = strSQL;
            CMD = SQLBuildSqlCommand1(strSQL, V_arr, CMD);

            DA.SelectCommand = CMD;

            DA.Fill(DS);
            return DS;
        }
        catch (Exception ex)
        {
            throw new Exception("Error:" + ex.Message);
        }
    }
    public DataSet Query(string strSQL, object[,] V_arr, int intBegin, int intPageSize)
    {
        SqlConnection CN = new SqlConnection();
        CN.ConnectionString = strCN1;
        SqlDataAdapter DA = new SqlDataAdapter();
        SqlCommand CMD = new SqlCommand();
        DataSet DS = new DataSet();

        try
        {
            CMD.Connection = CN;
            CMD.CommandText = strSQL;
            CMD = SQLBuildSqlCommand1(strSQL, V_arr, CMD);

            DA.SelectCommand = CMD;

            DA.Fill(DS, intBegin, intPageSize, "Table0");
            return DS;
        }
        catch (Exception ex)
        {
            throw new Exception("Error:" + ex.Message);
        }
    }
    public string QueryFirstRec(string strSQL)
    {
        DataSet DS = new DataSet();
        try
        {
            DS = Query(strSQL);

            return DS.Tables[0].Rows[0][0].ToString();
        }
        catch (Exception ex)
        {
            // Throw New Exception("Error:" & ex.Message)
            return "";
        }
    }
    public string QueryFirstRec(string strSQL, object[,] V_arr)
    {
        DataSet DS = new DataSet();
        try
        {
            DS = Query(strSQL, V_arr);

            return DS.Tables[0].Rows[0][0].ToString();
        }
        catch (Exception ex)
        {
            // Throw New Exception("Error:" & ex.Message)
            return "";
        }
    }


    public bool ExecSQL(string strSQL, ref string errMsg)
    {
        SqlConnection CN = new SqlConnection();
        CN.ConnectionString = strCN1;
        SqlCommand CMD = new SqlCommand();

        try
        {
            CMD.CommandText = strSQL;

            CMD.Connection = CN;
            CMD.Connection.Open();
            CMD.ExecuteNonQuery();
            CMD.Connection.Close();
        }
        catch (Exception ex)
        {
            errMsg = ex.Message;
            CMD.Connection.Close();
            // Throw New Exception("Error:" & ex.Message)
            return false;
        }
        return true;
    }
    public bool ExecSQL(string strSQL, object[,] V_arr, ref string errMsg)
    {
        SqlConnection CN = new SqlConnection();
        CN.ConnectionString = strCN1;
        SqlCommand CMD = new SqlCommand();

        try
        {
            CMD = SQLBuildSqlCommand1(strSQL, V_arr, CMD);
            CMD.Connection = CN;
            // 
            CMD.Connection.Open();

            // 
            CMD.CommandText = strSQL;
            CMD.ExecuteNonQuery();
            CMD.Connection.Close();
        }
        catch (Exception ex)
        {
            errMsg = ex.Message;
            CMD.Connection.Close();
            // Throw New Exception("Error:" & ex.Message)

            return false;
        }
        return true;
    }
    public SqlCommand SQLBuildSqlCommand1(string strSQL)
    {
        SqlConnection connect = new SqlConnection(strCN1);
        SqlCommand command = new SqlCommand(strSQL, connect);
        // 文字傳入
        command.CommandType = CommandType.Text;

        // '帶參數進入
        // Dim Ii As Integer
        // Try
        // If ((parameterNames IsNot Nothing)) Then

        // '參數用二維陣列傳入
        // '(X,0)=@參數名稱
        // '(X,1)=數值
        // Dim parameter As SqlParameter
        // For Ii = 0 To parameterNames.Length / 2 - 1

        // '建立 
        // parameter = New SqlParameter()
        // parameter.ParameterName = parameterNames(Ii, 0)
        // '給予名稱、來源欄位 
        // parameter.SourceColumn = parameterNames(Ii, 0)
        // parameter.Value = parameterNames(Ii, 1)
        // '附加進去
        // command.Parameters.Add(parameter)
        // Next

        // End If
        // Catch ex As Exception
        // Throw New Exception(ex.Message)
        // End Try

        return command;
    }
    public SqlCommand SQLBuildSqlCommand1(string strSQL, object[,] parameterNames, SqlCommand cmd)
    {
        SqlConnection connect = new SqlConnection(strCN1);
        SqlCommand command = new SqlCommand(strSQL, connect);
        // 文字傳入
        command.CommandType = CommandType.Text;

        // 帶參數進入
        int Ii;
        try
        {
            if (((parameterNames != null)))
            {

                // 參數用二維陣列傳入
                // (X,0)=@參數名稱
                // (X,1)=數值
                SqlParameter parameter;
                for (Ii = 0; Ii <= parameterNames.Length / (double)2 - 1; Ii++)
                {

                    // 建立 
                    parameter = new SqlParameter();
                    parameter.ParameterName =(string) parameterNames[Ii, 0];
                    // 給予名稱、來源欄位 
                    parameter.SourceColumn =(string) parameterNames[Ii, 0];
                    if (parameterNames[Ii, 1] == null)
                        parameter.Value = DBNull.Value;
                    else
                        parameter.Value = parameterNames[Ii, 1];

                    // 附加進去
                    command.Parameters.Add(parameter);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return command;
    }
}
