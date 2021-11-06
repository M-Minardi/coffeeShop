using coffeeShop.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coffeeShop.Entidades
{
    public class ProductosCategorias
    {
        public int ProductoId { get; set; }
        public int CategoriaId { get; set; }
        public Producto Producto { get; set; }
        public Categoria Categoria { get; set; }
    }
}
