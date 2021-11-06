using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coffeeShop.Entidades
{
    public class Producto : IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Nombre { get; set; }        
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public double Valor { get; set; }
        public List<ProductosCategorias> ProductosCategorias { get; set; }        
    }
}
