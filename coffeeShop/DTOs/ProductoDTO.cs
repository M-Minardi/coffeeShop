using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using coffeeShop.Helpers;

namespace coffeeShop.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Nombre { get; set; }        
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public double Valor { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> CategoriasIDs { get; set; }
        public List<CategoriaDTO> Categoria { get; set; }
    }
}
