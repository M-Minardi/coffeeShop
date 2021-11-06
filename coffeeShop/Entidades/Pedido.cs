using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using coffeeShop.DTOs;
using coffeeShop.Helpers;

namespace coffeeShop.Entidades
{
    public class Pedido : IId
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        [Required]        
        public DateTime FechaAlta { get; set; }
        public DateTime FechaEntregado { get; set; }
        public bool RetiraSucursal { get; set; }
        public string DireccionEnvio { get; set; }        
        public bool Entregado { get; set; }
        public bool Preparado { get; set; }        
        public string UsuarioID { get; set; }
        public IdentityUser Usuario { get; set; }
        public List<PedidoItem> PedidoItems { get; set; }
    }
}
