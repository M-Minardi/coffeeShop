using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using coffeeShop.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using coffeeShop.Entidades;

namespace coffeeShop.DTOs
{
    public class PedidoDTO
    {                
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string DireccionEnvio { get; set; }
        public bool RetiraSucursal { get; set; }
        public bool Entregado { get; set; }
        public bool Preparado { get; set; }
        [Required]
        public DateTime FechaAlta { get; set; }
        public DateTime FechaEntregado { get; set; }        
        
        [ModelBinder(BinderType = typeof(TypeBinder<string>))]
        public string UsuarioID { get; set; }
        public UsuarioDTO Usuario { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> PedidoItemIDs { get; set; }
        public List<PedidoItemDTO> PedidoItem { get; set; }
    }
}
