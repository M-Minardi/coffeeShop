using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using coffeeShop.Entidades;
using coffeeShop.Helpers;

namespace coffeeShop.DTOs
{
    public class CategoriaDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> ProductoIDs { get; set; }
        public List<ProductoDTO> Producto { get; set; }
    }
}
