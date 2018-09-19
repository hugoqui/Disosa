using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DisosaWebSite.Models
{
    public class Catalogo
    {
        public List<ProductoCatalogo> Productos { get; set; }
    }

    public class ProductoCatalogo
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
    }

    public class Empleado
    {
        public int Id{ get; set; }
        
        public string Nombre { get; set; }
        public string Puesto { get; set; }
        public string Imagen { get; set; }        
    }
}