using DataLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ComputerReg.Controllers
{
    [Route("[controller]")]
    public class RegALLComputerController : Controller
    {
        IConfiguration configuration;
        public RegALLComputerController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public outRegALLComputer DataHandler(inRegALLComputer inU)
        {
            outRegALLComputer o = new outRegALLComputer();
            clsBusiness clsData = new clsBusiness(configuration.GetValue<string>("ConnectionStrings:sqlConnect"));
            AES.AES aes = new AES.AES();
            string strTmp = aes.AES_Decrypt(inU.ServerKey, configuration.GetValue<string>("AppSetting:AES_header") + "_" + inU.fingerprint);
            if (strTmp.Equals(""))
            {
                throw new HttpException(400, "請登入");
            }
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string strErrMsg = "";
            bool bolResult = false;
            JsonHandler JH = new JsonHandler();
            o.Message = "OK";
            switch (inU.Way)
            {
                case "INIT":

                    ds = clsData.InitRegAllComputer();
                    o.Data = JH.DataSetToJSONWithStringBuilder(ds); 

                    //strSQL = "SELECT * FROM code_d WHERE code_kind='own_type'  ORDER BY CONVERT(INT,attr1) DESC";
                    //let dt4 = await querySQL(strSQL, sqlA);
                    //Datas.own_type = dt4.recordset;


                    //strSQL = "SELECT * FROM code_d WHERE code_kind='Computer_status'   ORDER BY CONVERT(INT,attr1) DESC";
                    //let dt5 = await querySQL(strSQL, sqlA);
                    //Datas.Computer_status = dt5.recordset;

                    break;
                case "ADD":
                    //clsData.AddRegAllComputer(inU.)
                    //strSQL = "INSTERT INTO  user_Computer (empno, Computer_Name,  fingerprint, host_name) VALUES (@empno, @Computer_Name, @fingerprint, @host_name)";
                    //Computer_Name = request.body.Computer_Name;
                    //fingerprint = request.body.fingerprint;
                    //host_name = request.body.host_name;
                    //let Para1 = new Array;
                    //Para1.push({ name: "empno", value: empno });
                    //Para1.push({ name: "Computer_Name", value: Computer_Name });
                    //Para1.push({ name: "fingerprint", value: fingerprint });
                    //Para1.push({ name: "host_name", value: host_name });

                    //await querySQLWithParas(strSQL, sqlA, Para1);

                    break;
                case "UPD":
                    //strSQL = " UPDATE user_Computer SET Computer_Name=@Computer_Name ,  fingerprint=@fingerprint , host_name=@host_name WHERE id=@id ";
                    //id = request.body.id;
                    //Computer_Name = request.body.Computer_Name;
                    //fingerprint = request.body.fingerprint;
                    //host_name = request.body.host_name;
                    //let Para2 = new Array;
                    //Para2.push({ name: "id", value: id });
                    //Para2.push({ name: "Computer_Name", value: Computer_Name });
                    //Para2.push({ name: "fingerprint", value: fingerprint });
                    //Para2.push({ name: "host_name", value: host_name });

                    //await querySQLWithParas(strSQL, sqlA, Para2);



                    break;
                case "QRY":
                    ds = clsData.QryRegAllComputer(inU.QueryEmpno, inU.own_type, inU.stats, inU.QueryMacAddress, inU.QueryFajNo, inU.Page);
                    o.Data = JH.DataSetToJSONWithStringBuilder(ds);


                    //        QueryEmpno = request.body.QueryEmpno;
                    //        own_type = request.body.own_type;
                    //        Page = parseInt(request.body.Page);
                    //        stats = request.body.stats;
                    //        QueryMacAddress = request.body.QueryMacAddress;
                    //        QueryFajNo = request.body.QueryFajNo;
                    //        //console.log("sql.NVarChar=" + sqlP.NVarChar + " ,sql.Int=" + sqlP.Int);

                    //        strSQL = "SELECT COUNT(*) TT FROM user_Computer WHERE empno LIKE '%' + @empno + '%' AND own_type LIKE '%' + @own_type + '%'  AND stats LIKE '%' + @stats + '%' AND id IN ( SELECT aa.User_Computer_id FROM  Computer_MacAddress aa ,MacAddress_d bb WHERE  Mac_Address LIKE '%' +@Mac +'%' AND aa.MacAddress_id =bb.id ) AND FAJ_number LIKE '%' + @FAJ + '%' ";
                    //        //console.log("SQL:" + strSQL);
                    //        let Para3 = new Array;
                    //        Para3.push({ name: "empno", value: QueryEmpno, typeof: sqlP.NVarChar});
                    //        Para3.push({ name: "own_type", value: own_type, typeof: sqlP.NVarChar });
                    //        Para3.push({ name: "stats", value: stats, typeof: sqlP.NVarChar });
                    //        Para3.push({ name: "Mac", value: QueryMacAddress, typeof: sqlP.NVarChar });
                    //        Para3.push({ name: "FAJ", value: QueryFajNo, typeof: sqlP.NVarChar });
                    //        //Count={"recordsets":[[{"TT":11}]],"recordset":[{"TT":11}],"output":{},"rowsAffected":[1]}
                    //        let dt0 = await querySQLWithParas(strSQL, sqlA, Para3);
                    //        let Count = dt0.recordsets[0][0].TT;
                    //        console.log("Count=" + Count);



                    //        strSQL = "SELECT a.*, (SELECT TOP 1 b.user_name FROM " + EHR + "[EHR].dbo.vw_address_book2 b WHERE a.empno=b.main_user   COLLATE   Chinese_Taiwan_Stroke_CI_AS ) user_name ,c.code_desc stats_desc,d.code_desc own_type_desc ,(SELECT + '{' + '\"name\":' + '\"' +  aa.name + '\"' + ',' + '\"value\":' + '\"' + aa.Mac_Address + '\"' + '},' FROM MacAddress_d aa,Computer_MacAddress bb WHERE aa.id=bb.MacAddress_id AND bb.User_Computer_id =a.ID FOR XML PATH('') )   Mac_Address_d FROM user_Computer a,code_d c,code_d d   WHERE a.empno LIKE '%' + @empno + '%' AND a.own_type LIKE '%' + @own_type + '%' AND a.stats LIKE '%' + @stats + '%' AND a.stats=c.code_val AND c.code_kind='Computer_status' AND a.[Own_Type] =d.[code_val]  AND d.code_kind='own_type' AND a.id IN ( SELECT aa.User_Computer_id FROM  Computer_MacAddress aa ,MacAddress_d bb WHERE  Mac_Address LIKE '%' +@Mac +'%'  AND aa.MacAddress_id =bb.id ) AND a.FAJ_number LIKE '%' + @FAJ + '%' ORDER BY id DESC";
                    //        strSQL = strSQL + " OFFSET (@Skip) ROWS FETCH NEXT (@Take) ROWS ONLY "
                    ////let Para3 = new Array;

                    //        Para3.push({ name: "Skip", value: (Page - 1) * 10, typeof: sqlP.Int});
                    //        Para3.push({ name: "Take", value: 10, typeof: sqlP.Int });
                    //        console.log("strSQL=" + strSQL);
                    //        let dt2 = await querySQLWithParas(strSQL, sqlA, Para3);

                    //        Datas.Data = dt2.recordset;

                    //        //一頁10筆。算出一共幾筆          

                    //        let TotalPages = (Count % 10 == 0 ? Count / 10 : parseInt(Count / 10) + 1);
                    //        console.log(chalk.blue("頁數:" + Pages));

                    //        Datas.TotalPages = TotalPages;





                    break;
                case "Q1":
                    dt = clsData.Q1RegAllComputer(inU.id);
                    o.Data = JH.DataTableToJSONWithStringBuilder(dt);
                    //strSQL = "SELECT a.*, (SELECT TOP 1 b.user_name FROM " + EHR + "[EHR].dbo.vw_address_book2 b WHERE a.empno=b.main_user) user_name ,c.code_desc stats_desc,d.code_desc own_type_desc ,(SELECT + '{' + '\"name\":' + '\"' +  aa.name + '\"' + ',' + '\"value\":' + '\"' + aa.Mac_Address + '\"' + '},' FROM MacAddress_d aa,Computer_MacAddress bb WHERE aa.id=bb.MacAddress_id AND bb.User_Computer_id =a.ID FOR XML PATH('') )   Mac_Address_d FROM user_Computer a,code_d c,code_d d   WHERE  a.stats=c.code_val AND c.code_kind='Computer_status' AND a.[Own_Type] =d.[code_val]  AND d.code_kind='own_type' AND id=@id";
                    //let Para4 = new Array;
                    //id = request.body.id;
                    //Para4.push({ name: "id", value: id });

                    //let dt3 = await querySQLWithParas(strSQL, sqlA, Para4);
                    //Datas = dt3.recordset;



                    break;
                case "DEL":
                    bolResult = clsData.DelRegAllComputer(inU.id, ref strErrMsg);
                    if (!bolResult)
                    {
                        o.Message = strErrMsg;
                    }
                   
                    o.Data = bolResult;
                    //strSQL = " UPDATE  user_Computer SET stats='XX' WHERE id=@id";
                    //let Para5 = new Array;
                    //id = request.body.id;
                    //Para5.push({ name: "id", value: id });

                    //await querySQLWithParas(strSQL, sqlA, Para5);



                    break;

                case "DELMAC":
                    bolResult = clsData.DelMac(inU.id, ref strErrMsg);
                    if (!bolResult)
                    {
                        o.Message = strErrMsg;
                    }
                    o.Data = bolResult;
                    //strSQL = "UPDATE  MacAddress_d SET stats='XX' WHERE id=@id";
                    //let Para6 = new Array;
                    //id = request.body.id;
                    //Para6.push({ name: "id", value: id });

                    //await querySQLWithParas(strSQL, sqlA, Para6);



                    break;
                case "ADDMAC":
                    dt = JH.ParseToDataTable(inU.MAC.ToString());
                    bolResult = clsData.AddMac(inU.own_type, inU.HostName, inU.FajNo, inU.remark, inU.UseEmpno, "", dt, ref strErrMsg);
                    if (!bolResult)
                    {
                        o.Message = strErrMsg;
                    }
                    o.Data = bolResult;
                    //Network = request.body.MAC;
                    //own_type = request.body.own_type;
                    //host_name = request.body.HostName;
                    //FajNo = request.body.FajNo;
                    //remark = request.body.Desc;
                    //UseEmpno = request.body.UseEmpno;

                    //computer_id = 0;
                    //strSQL = "INSERT INTO user_Computer (empno, Own_Type,  host_name, FAJ_number,remark,Add_Empno) VALUES ( @empno, @Own_Type,  @host_name, @FAJ_number,@remark ,'" + empno + "') ";
                    //let Para7 = new Array;
                    //Para7.push({ name: "empno", value: UseEmpno });
                    //Para7.push({ name: "Own_Type", value: own_type });
                    //Para7.push({ name: "host_name", value: host_name });
                    //Para7.push({ name: "FAJ_number", value: FajNo });
                    //Para7.push({ name: "remark", value: remark });
                    //await querySQLWithParas(strSQL, sqlA, Para7);

                    //strSQL = "SELECT * FROM user_Computer WHERE empno=@empno AND Own_Type=@Own_Type AND  host_name=@host_name AND FAJ_number=@FAJ_number  AND Add_Empno='" + empno + "'";
                    //let Para7A = new Array;
                    //Para7A.push({ name: "empno", value: UseEmpno });
                    //Para7A.push({ name: "Own_Type", value: own_type });
                    //Para7A.push({ name: "host_name", value: host_name });
                    //Para7A.push({ name: "FAJ_number", value: FajNo });

                    //let dt = await querySQLWithParas(strSQL, sqlA, Para7A);//.recordsets[0].id;result.recordsets[0].length
                    //                                                       //console.log(chalk.red(JSON.stringify(dt[0])));
                    //computer_id = dt.recordsets[0][0].id;
                    ////console.log(chalk.red(JSON.stringify(dt.recordsets[0][0])));
                    //Network = JSON.parse(Network);
                    ////找到ID
                    //for (Ii = 0; Ii < Network.length; Ii++)
                    //{
                    //    strSQL = " INSERT INTO MacAddress_d (name,Mac_Address) VALUES (@name,@Mac_Address) ";
                    //    let Para7B = new Array;
                    //    Para7B.push({ name: "name", value: Network[Ii].name });
                    //    Para7B.push({ name: "Mac_Address", value: Network[Ii].mac });
                    //    await querySQLWithParas(strSQL, sqlA, Para7B);

                    //    strSQL = "SELECT * FROM MacAddress_d WHERE name=@name AND Mac_Address=@Mac_Address ";

                    //    let dt2 = await querySQLWithParas(strSQL, sqlA, Para7B);//.result.recordsets[0][0].id;
                    //                                                        //console.log(chalk.red(JSON.stringify(dt2)));
                    //    mac_id = dt2.recordsets[0][0].id;

                    //    //建立關聯
                    //    strSQL = " INSERT INTO Computer_MacAddress ( MacAddress_id, User_Computer_id) VALUES ( @MacAddress_id, @User_Computer_id) ";

                    //    let Para7C = new Array;

                    //    Para7C.push({ name: "MacAddress_id", value: mac_id });
                    //    Para7C.push({ name: "User_Computer_id", value: computer_id });

                    //    await querySQLWithParas(strSQL, sqlA, Para7C);
                    //}


                    break;
                case "CheckMAC":
                    ds = clsData.CheckMac(inU.HostName, inU.FajNo);
                    o.Data = JH.DataSetToJSONWithStringBuilder(ds);
                    //同樣電腦、同樣mac Address 禁止新增
                    //Network = request.body.MAC;
                    //own_type = request.body.own_type;
                    //host_name = request.body.HostName;
                    //FajNo = request.body.FajNo;
                    //remark = request.body.Desc;
                    //UseEmpno = request.body.UseEmpno;

                    //let Data = { };
                    //strSQL = "SELECT * FROM user_Computer WHERE host_name =@Computer_Name AND stats='00' ";
                    //let Para10 = new Array;
                    //Para10.push({ name: "Computer_Name", value: host_name });
                    //let dt6 = await querySQLWithParas(strSQL, sqlA, Para10);//.result.recordsets[0][0].id;

                    //if (dt6.rowsAffected > 0)
                    //{
                    //    Data.Checkmessage = "有相同的電腦名稱";
                    //    Data.Count = dt6.rowsAffected;
                    //    Data.ChangeColorControl = "txtHostName";
                    //    Data.ChangeColor = "yellow";
                    //    Datas.checkdata = Data;

                    //    return Datas;
                    //}
                    //strSQL = "SELECT * FROM user_Computer WHERE FAJ_number =@FAJ AND stats='00' AND FAJ_number NOT IN ('NA','無資產')";
                    //let Para11 = new Array;
                    //Para11.push({ name: "FAJ", value: FajNo });
                    //dt6 = await querySQLWithParas(strSQL, sqlA, Para11);
                    //if (dt6.rowsAffected > 0)
                    //{
                    //    Data.Checkmessage = "有相同的資產編號";
                    //    Data.Count = dt6.rowsAffected;
                    //    Data.ChangeColorControl = "txtFajNo";
                    //    Data.ChangeColor = "yellow";
                    //    Datas.checkdata = Data;

                    //    return Datas;
                    //}
                    ////從GDTCNT撈
                    //strSQL = "SELECT * FROM [GDTCNT].[SRC].dbo.[DSC_FAJ_FILE] WHERE FAJ02=@FAJ AND faj43 IN ('5','6')";
                    //dt6 = await querySQLWithParas(strSQL, sqlB, Para11);
                    //if (dt6.rowsAffected > 0)
                    //{
                    //    Data.Checkmessage = "資產已報廢";
                    //    Data.Count = dt6.rowsAffected;
                    //    Data.ChangeColorControl = "txtFajNo";
                    //    Data.ChangeColor = "red";
                    //    Datas.checkdata = Data;

                    //    return Datas;
                    //}


                    //strSQL = "SELECT a.* FROM user_Computer a,Computer_MacAddress b , MacAddress_d c WHERE a.id=b.User_Computer_id AND b.MacAddress_id=c.id  AND a.stats='00' ";
                    //Network = JSON.parse(Network);
                    //for (Ii = 0; Ii < Network.length; Ii++)
                    //{
                    //    strSQL = strSQL + " AND c.Mac_Address='" + Network[Ii].mac + "'";

                    //}
                    //dt6 = await querySQL(strSQL, sqlA);
                    ////console.log(chalk.green(dt6.rowsAffected));
                    //if (dt6.rowsAffected > 0)
                    //{
                    //    Data.Checkmessage = "有相同的MAC Address";
                    //    Data.Count = dt6.rowsAffected;
                    //    Data.ChangeColorControl = "txtName_";
                    //    Data.ChangeColor = "yellow";

                    //    Datas.checkdata = Data;
                    //    return Datas;
                    //}
                    //Data.Checkmessage = "OK";
                    //Datas.checkdata = Data;

                    break;
                default:
                    //UPDMAC
                    dt = JH.ParseToDataTable(inU.MAC.ToString());
                    bolResult = clsData.UpdMac(inU.id, inU.own_type, inU.HostName, inU.FajNo, inU.remark, dt, ref strErrMsg);
                    if (!bolResult)
                    {
                        o.Message = strErrMsg;
                    }
                    o.Data = bolResult;
                    //Network = request.body.MAC;
                    //own_type = request.body.own_type;
                    //host_name = request.body.HostName;
                    //FajNo = request.body.FajNo;
                    //remark = request.body.Desc;
                    //UseEmpno = request.body.UseEmpno;

                    //computer_id = request.body.computer_id;
                    //strSQL = "UPDATE  user_Computer SET  Own_Type=@Own_Type,  host_name=@host_name, FAJ_number=@FAJ_number,remark=@remark where id=@id ";
                    //let Para8 = new Array;
                    //Para8.push({ name: "id", value: computer_id });
                    //Para8.push({ name: "Own_Type", value: own_type });
                    //Para8.push({ name: "host_name", value: host_name });
                    //Para8.push({ name: "FAJ_number", value: FajNo });
                    //Para8.push({ name: "remark", value: remark });
                    //await querySQLWithParas(strSQL, sqlA, Para8);


                    //Network = JSON.parse(Network);
                    ////先全砍 MacAddress_d、Computer_MacAddress 
                    //strSQL = "DELETE   MacAddress_d WHERE id IN ( SELECT MacAddress_id FROM  Computer_MacAddress WHERE User_Computer_id=@id )";
                    //let Para9 = new Array;
                    //Para9.push({ name: "id", value: computer_id });
                    //await querySQLWithParas(strSQL, sqlA, Para9);

                    //strSQL = "DELETE   Computer_MacAddress WHERE User_Computer_id =@id ";

                    //await querySQLWithParas(strSQL, sqlA, Para9);


                    ////再欣增
                    //for (Ii = 0; Ii < Network.length; Ii++)
                    //{
                    //    strSQL = " INSERT INTO MacAddress_d (name,Mac_Address) VALUES (@name,@Mac_Address) ";
                    //    let Para8B = new Array;
                    //    Para8B.push({ name: "name", value: Network[Ii].name });
                    //Para8B.push({ name: "Mac_Address", value: Network[Ii].mac });
                    //await querySQLWithParas(strSQL, sqlA, Para8B);

                    //strSQL = "SELECT * FROM MacAddress_d WHERE name=@name AND Mac_Address=@Mac_Address ";

                    //let dtU2 = await querySQLWithParas(strSQL, sqlA, Para8B);//.result.recordsets[0][0].id;
                    //                                                         //console.log(chalk.red(JSON.stringify(dt2)));
                    //mac_id = dtU2.recordsets[0][0].id;

                    ////建立關聯
                    //strSQL = " INSERT INTO Computer_MacAddress ( MacAddress_id, User_Computer_id) VALUES ( @MacAddress_id, @User_Computer_id) ";

                    //let Para8C = new Array;

                    //Para8C.push({ name: "MacAddress_id", value: mac_id });
                    //Para8C.push({ name: "User_Computer_id", value: computer_id });

                    //await querySQLWithParas(strSQL, sqlA, Para8C);
                    break;
            }


            return o;
        }
    }
}
