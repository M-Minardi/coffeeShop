using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coffeeShop.Helpers;

namespace coffeeShop.DTOs
{
    public class PedidoItemDTO
    {        
        public int Id { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<int>))]
        public int PedidoID { get; set; }
        public PedidoDTO Pedido { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<int>))]
        public int ProductoID { get; set; }
        public ProductoDTO Producto { get; set; }
        public int Cantidad { get; set; }
        public double Valor { get; set; }
    }
}
