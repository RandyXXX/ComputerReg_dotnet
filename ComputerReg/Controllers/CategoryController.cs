using DataLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ComputerReg.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        IConfiguration configuration;
        public CategoryController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public outCategory DataHandler(inCategory inU)
        {
            outCategory o = new outCategory();
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
                    dt = clsData.QryCategory();
                    o.Data = JH.DataTableToJSONWithStringBuilder(dt);
                    //strSQL = "SELECT * FROM vw_menu ORDER BY BigSeq,MasterNo,SeqNo ";
                    //dt = await querySQL(strSQL, sqlA);


                    //Datas = dt.recordset;
                    //console.log(JSON.stringify(Datas));



                    break;
                case "ADD":
                    //新增
                    bolResult = clsData.AddCategory(inU.PrgPath, inU.CategoryDesc, inU.PrgName, inU.MasterNo, inU.Seq, ref strErrMsg);
                    if (!bolResult)
                    {
                        o.Message = strErrMsg;
                    }
                    o.Data = bolResult;
                    //MasterNo = request.body.MasterNo;
                    //PrgName = request.body.PrgName;
                    //CategoryDesc = request.body.CategoryDesc;
                    //PrgPath = request.body.PrgPath;
                    //Seq = request.body.Seq;

                    //let Para1 = new Array;

                    //Para1.push({ name: "MasterNo", value: MasterNo});
                    //Para1.push({ name: "PrgName", value: PrgName });
                    //Para1.push({ name: "CategoryDesc", value: CategoryDesc });

                    //Para1.push({ name: "PrgPath", value: PrgPath});
                    //Para1.push({ name: "SeqNo", value: Seq });

                    //strSQL = "INSERT INTO Category(PrgPath, CategoryDesc, PrgName, MasterNo, SeqNo) VALUES (@PrgPath, @CategoryDesc, @PrgName, @MasterNo, @SeqNo)";

                    //await querySQLWithParas(strSQL, sqlA, Para1);
                    break;
                case "UPD":
                    //修改
                    bolResult = clsData.UpdCategory(inU.PrgSerNo, inU.PrgPath, inU.CategoryDesc, inU.PrgName, inU.MasterNo, inU.Seq, ref strErrMsg);
                    if (!bolResult)
                    {
                        o.Message = strErrMsg;
                    }
                    o.Data = bolResult;
                    //MasterNo = request.body.MasterNo;
                    //PrgName = request.body.PrgName;
                    //CategoryDesc = request.body.CategoryDesc;
                    //PrgPath = request.body.PrgPath;
                    //Seq = request.body.Seq;
                    //PrgSerNo = request.body.PrgSerNo;
                    //let Para2 = new Array;

                    //Para2.push({ name: "MasterNo", value: MasterNo });
                    //Para2.push({ name: "PrgName", value: PrgName });
                    //Para2.push({ name: "CategoryDesc", value: CategoryDesc });
                    //Para2.push({ name: "PrgPath", value: PrgPath });
                    //Para2.push({ name: "SeqNo", value: Seq });

                    //Para2.push({ name: "PrgSerNo", value: PrgSerNo });

                    //strSQL = "UPDATE Category SET PrgPath=@PrgPath , CategoryDesc=@CategoryDesc , PrgName=@PrgName , MasterNo=@MasterNo, SeqNo=@SeqNo WHERE PrgSerNo=@PrgSerNo ";

                    //await querySQLWithParas(strSQL, sqlA, Para2);



                    break;
                case "DEL":
                    //刪除
                    bolResult = clsData.DelCategory(inU.PrgSerNo,ref strErrMsg);
                    if (!bolResult)
                    {
                        o.Message = strErrMsg;
                    }
                    o.Data = bolResult;
                    //PrgSerNo = request.body.PrgSerNo;
                    //let Para3 = new Array;
                    //Para3.push({ name: "PrgSerNo", value: PrgSerNo });
                    //strSQL = "DELETE  Category   WHERE PrgSerNo=@PrgSerNo ";

                    //await querySQLWithParas(strSQL, sqlA, Para3);

                    break;
                case "Q1":
                    dt = clsData.Q1Category(inU.PrgSerNo);
                    o.Data = JH.DataTableToJSONWithStringBuilder(dt);
                    //PrgSerNo = request.body.PrgSerNo;
                    //let Para4 = new Array;
                    //Para4.push({ name: "PrgSerNo", value: PrgSerNo });
                    //strSQL = "SELECT * FROM  Category where PrgSerNo=@PrgSerNo";
                    //dt = await querySQLWithParas(strSQL, sqlA, Para4);
                    //Datas = dt.recordset;



                    break;
                default:
                    //取得目錄
                    dt = clsData.QueryMenuCategory();
                    o.Data = JH.DataTableToJSONWithStringBuilder(dt);
                    //strSQL = "SELECT * FROM  Category where MasterNo=-1 ORDER BY SeqNo ";
                    //dt = await querySQL(strSQL, sqlA);
                    //Datas = dt.recordset;
                    //console.log(JSON.stringify(Datas));
                    break;
            }





            return o;
        }
    }
}
