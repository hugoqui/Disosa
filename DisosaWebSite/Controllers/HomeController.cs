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
using System.Security.Cryptography;
using System.IO;
using System.Text;

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
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://admin.disosagt.com/api/Catalogo");//
            request.Method = "Get";
            request.KeepAlive = true;
            request.Accept = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string myResponse;
            using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
            {
                myResponse = sr.ReadToEnd();
            }

            if (search != null) { page = 1; }
            List<ProductoCatalogo> productos = JsonConvert.DeserializeObject<List<ProductoCatalogo>>(myResponse);
            if (!String.IsNullOrEmpty(search))
            {
                productos = productos.Where(p => p.Nombre.Contains(search)).ToList();
            }
            productos = productos.OrderBy(p => p.Codigo).ToList();
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(productos.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Team(string token)
        {
            var empleados = new List<Empleado>();
            var empleado = new Empleado();

            int id = int.Parse(Decrypt(token));
            if (id == 0)
            {
                empleado = new Empleado() { Id = 0,
                    Imagen = "https://cdn3.vectorstock.com/i/1000x1000/24/87/danger-icon-danger-sign-vector-13912487.jpg",
                    Nombre = "No es un empleado de Disosa", Puesto = "No labora" };
                return View(empleado);
            }

            empleado = new Empleado() { Id = 18, Nombre = "Bagner Sánchez", Puesto = "Gerente General" };
            empleado.Imagen = "../../Content/images/team/" + empleado.Id + ".jpeg";
            empleados.Add(empleado);

            empleado = new Empleado() { Id = 17, Nombre = "Jairo Sánchez", Puesto = "Gerente de Ventas" };
            empleado.Imagen = "../../Content/images/team/" + empleado.Id + ".jpeg";
            empleados.Add(empleado);

            var result = empleados.Find(i => i.Id == id);
            
            return View(result);
        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                try
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                catch (Exception)
                {
                    return "0";                    
                }
            }
            return cipherText;
        }
    }
}