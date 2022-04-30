using DataLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerReg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class getMenuController : Controller
    {
        IConfiguration configuration;
        public getMenuController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost][HttpGet]
        public outMenu GetMenu()
        {
            outMenu o = new outMenu();
            BigMenu[] o1;
            SubMenu[] o2;
            int Ii;
            int Jj;
            DataView Dv;
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            clsBusiness clsData = new clsBusiness(configuration.GetValue<string>("ConnectionStrings:sqlConnect"));

            dt1 = clsData.GetMenu("TA150005");

            Dv = dt1.DefaultView;
            dt2 = Dv.ToTable(true, "BigSeq");

            o1 = new BigMenu[dt2.Rows.Count];
            for(Ii=0;Ii<dt2.Rows.Count;Ii++)
            {
                
                Dv.RowFilter = " BigSeq='" + dt2.Rows[Ii]["BigSeq"].ToString() + "' AND MasterNo=-1 ";
                if (Dv.ToTable().Rows.Count == 0)
                {
                    continue;

                }
                o1[Ii] = new BigMenu();
                o1[Ii].bigname = Dv.ToTable().Rows[0]["PrgName"].ToString();

                Dv.RowFilter = " BigSeq='"+ dt2.Rows[Ii]["BigSeq"].ToString() +"' AND MasterNo<>-1";
                dt3 = Dv.ToTable();
                o2 = new SubMenu[dt3.Rows.Count];
                for (Jj = 0; Jj < dt3.Rows.Count; Jj++)
                {
                    o2[Jj] = new SubMenu();
                    o2[Jj].name = dt3.Rows[Jj]["PrgName"].ToString();
                    o2[Jj].path = dt3.Rows[Jj]["PrgPath"].ToString();
                }
                o1[Ii].item = o2;
            }
            o.menu = o1;
            return o;
        }
    }
}
