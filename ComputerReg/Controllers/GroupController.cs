using DataLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ComputerReg.Controllers
{
    [Route("[controller]")]
    public class GroupController : Controller
    {
        IConfiguration configuration;
        public GroupController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public outGroup DataHandler(inGroup inU)
        {
            outGroup o = new outGroup();
            clsBusiness clsData = new clsBusiness(configuration.GetValue<string>("ConnectionStrings:sqlConnect"));
            AES.AES aes = new AES.AES();
            string strTmp = aes.AES_Decrypt(inU.ServerKey, configuration.GetValue<string>("AppSetting:AES_header") + "_" + inU.fingerprint);
            if (strTmp.Equals(""))
            {
                throw new HttpException(400, "請登入");
            }
            DataTable dt = new DataTable();
            string strErrMsg = "";
            bool bolResult = false;
            JsonHandler JH = new JsonHandler();
            switch (inU.Way)
            {
                case "QRY":
                    //查詢
                    dt = clsData.QueryAllGroup();
                    //dt.TableName = "AA";
                    
                    o.Data = JH.DataTableToJSONWithStringBuilder(dt);
                    break;
                case "ADD":

                    bolResult = clsData.AddGroup(inU.GROUPNO, inU.GroupDesc, inU.GroupName, ref strErrMsg);
                    o.Data = bolResult;
                    if (bolResult)
                    {

                    }
                    else
                    {
                        o.Message = strErrMsg;
                    }

                 
                    break;
                case "UPD":
                    bolResult = clsData.UPDGroup(inU.SeqNo, inU.GROUPNO, inU.GroupDesc, inU.GroupName, ref strErrMsg);
                    o.Data = bolResult;
                    if (bolResult)
                    {

                    }
                    else
                    {
                        o.Message = strErrMsg;
                    }

               

                    break;
                case "DEL":
                    bolResult = clsData.DELGroup(inU.SeqNo, ref strErrMsg);
                    o.Data = bolResult;
                    if (bolResult)
                    {

                    }
                    else
                    {
                        o.Message = strErrMsg;
                    }
               

                    break;

                //GroupToPrg
                case "QRYGP":
                    DataSet ds = new DataSet();
                    ds = clsData.QRYGP(inU.GROUPNO);
                    o.Data = JH.DataSetToJSONWithStringBuilder(ds);
                    //o.Data = ds;
                    //GROUPNO = request.body.GROUPNO;
                    //let Para5 = new Array;
                    //Para5.push({ name: "GROUPNO", value: GROUPNO });
                    //strSQL = " SELECT a.Seqno ID,b.*,CASE WHEN b.MasterNo=-1 THEN '目錄' ELSE '程式' END TypeDesc FROM GroupToPrg a, Category b WHERE a.PrgSerNo=b.PrgSerNo AND a.GROUPNO=@GROUPNO ORDER BY b.SeqNo";

                    //dt = await querySQLWithParas(strSQL, sqlA, Para5);
                    //Datas.AllDatas = dt.recordset;

                    //strSQL = " SELECT b.*,CASE WHEN b.MasterNo=-1 THEN '目錄' ELSE '程式' END TypeDesc,  CASE WHEN b.MasterNo=-1 THEN  SeqNo ELSE (SELECT TOP 1 a.SeqNo FROM Category a WHERE a.PrgSerNo=b.MasterNo) END BigSeq FROM Category b WHERE 1=1 AND PrgSerNo NOT IN ( SELECT PrgSerNo FROM GroupToPrg WHERE GROUPNO=@GROUPNO)  ORDER BY  BigSeq DESC ,MasterNo ASC,SeqNo  DESC";

                    //dt = await querySQLWithParas(strSQL, sqlA, Para5);
                    //Datas.PartDatas = dt.recordset;
                    break;
                case "ADDGP":
                    

                    bolResult = clsData.AddGP(inU.PrgSerNo, inU.GROUPNO, ref strErrMsg);
                    if (!bolResult)
                    {
                        o.Message = strErrMsg;
                    }
                    o.Data = bolResult;

                    //PrgSerNo = request.body.PrgSerNo;
                    //GROUPNO = request.body.GROUPNO;
                    //let Para6 = new Array;
                    //Para6.push({ name: "PrgSerNo", value: PrgSerNo });
                    //Para6.push({ name: "GROUPNO", value: GROUPNO });
                    //strSQL = " INSERT INTO GroupToPrg (PrgSerNo,GROUPNO) VALUES (@PrgSerNo,@GROUPNO)";

                    //await querySQLWithParas(strSQL, sqlA, Para6);

                    break;
                case "DELGP":
                    bolResult = clsData.DelGP(inU.SeqNo, ref strErrMsg);
                    if (!bolResult)
                    {
                        o.Message = strErrMsg;
                    }
                    o.Data = bolResult;

                    //SeqNo = request.body.SeqNo;
                    //let Para7 = new Array;
                    //Para7.push({ name: "SeqNo", value: SeqNo });

                    //strSQL = "DELETE GroupToPrg WHERE Seqno=@SeqNo";

                    //await querySQLWithParas(strSQL, sqlA, Para7);

                    break;
                //GroupToUser
                case "QRYGU":
                    dt = clsData.QRYGU(inU.GROUPNO);
                    o.Data = JH.DataTableToJSONWithStringBuilder(dt);
                    //GROUPNO = request.body.GROUPNO;

                    //let Para8 = new Array;
                    //Para8.push({ name: "GROUPNO", value: GROUPNO });
                    //strSQL = " SELECT * FROM GroupToUser  WHERE GROUPNO=@GROUPNO ORDER BY Serno";

                    ////console.log(chalk.blue(strSQL));

                    //dt = await querySQLWithParas(strSQL, sqlA, Para8);
                    //Datas = dt.recordset;
                    break;
                case "ADDGU":
                    bolResult = clsData.ADDGU(inU.GROUPNO, inU.EMPNO , inU.AD, ref strErrMsg);
                    if (!bolResult)
                    {
                        o.Message = strErrMsg;
                    }
                    o.Data = bolResult;
                    //GROUPNO = request.body.GROUPNO;
                    //EMPNO = request.body.EMPNO;
                    //AD = request.body.AD;
                    //let Para9 = new Array;
                    //strSQL = "INSERT INTO GroupToUser (EMPNO,GROUPNO,AD) VALUES (@EMPNO,@GROUPNO,@AD)";
                    //Para9.push({ name: "GROUPNO", value: GROUPNO });
                    //Para9.push({ name: "EMPNO", value: EMPNO });
                    //Para9.push({ name: "AD", value: AD });
                    //await querySQLWithParas(strSQL, sqlA, Para9);

                    break;
                case "DELGU":
                    bolResult = clsData.DELGU(inU.SeqNo, ref strErrMsg);
                    if (!bolResult)
                    {
                        o.Message = strErrMsg;
                    }
                    o.Data = bolResult;
                    //SeqNo = request.body.SeqNo;
                    //let Para10 = new Array;
                    //Para10.push({ name: "SerNo", value: SeqNo });

                    //strSQL = "DELETE GroupToUser WHERE Serno=@SerNo";

                    //await querySQLWithParas(strSQL, sqlA, Para10);


                    break;
                default:
                    dt=clsData.Q1UserGroup(inU.SeqNo);
                    o.Data = JH.DataTableToJSONWithStringBuilder(dt);

                    break;
                    ////Q1
                    //SeqNo = request.body.SeqNo;
                    //let Para4 = new Array;
                    //Para4.push({ name: "SeqNo", value: SeqNo });
                    //strSQL = "SELECT * FROM UserGroup WHERE SeqNo=@SeqNo";
                    //dt = await querySQLWithParas(strSQL, sqlA, Para4);


                    //Datas = dt.recordset;


            }




            return o;
        }


    }
}
