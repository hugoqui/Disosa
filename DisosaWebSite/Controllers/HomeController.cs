using System;
using System.Data;
using System.Linq;
using DisosaWebSite.Models;
using System.Web.Mvc;
using PagedList;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DisosaWebSite.Controllers
{
    public class HomeController : Controller
    {
        // private disosadbEntities db = new disosadbEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Products(string search, int? page)
        {
            if (search != null) { page = 1; }
            // cuando haya algo, que muestre la pagina 1
            //var productos = db.Productos.ToList();
            //if (!String.IsNullOrEmpty(search))
            //{
            //    productos = productos.Where(p => p.Nombre.Contains(search)).ToList();
            //}
            //productos = productos.OrderBy(p => p.Codigo).ToList();
            //int pageSize = 12;
            //int pageNumber = (page ?? 1);
            //return View(productos.ToPagedList(pageNumber, pageSize));

            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create("http://admin.disosagt.com/api/Catalogo");//
            request.Method = "Get";
            request.KeepAlive = true;
            request.Accept = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string myResponse;
            using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
            {
                myResponse = sr.ReadToEnd();
            }

            List<ProductoCatalogo> productos = JsonConvert.DeserializeObject<List<ProductoCatalogo>>(myResponse);
            
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(productos.ToPagedList(pageNumber, pageSize));
        }
    }
}