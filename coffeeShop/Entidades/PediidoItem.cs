using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coffeeShop.Entidades
{
    public class PedidoItem : IId
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public int ProductoId { get; set; }        
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public double Valor { get; set; }
    }
}
