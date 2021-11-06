using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coffeeShop.DTOs
{
    public class FiltroProductosDTO
    {
        public int Pagina { get; set; } = 1;
        public int CantidadRegistrosPorPagina { get; set; } = 10;
        public PaginacionDTO Paginacion
        {
            get { return new PaginacionDTO() { Pagina = Pagina, CantidadRegistrosPorPagina = CantidadRegistrosPorPagina }; }
        }

        public string Nombre { get; set; }
        public int CategoriaId { get; set; }
        public bool Activo { get; set; }
        public string CampoOrdenar { get; set; }
        public bool OrdenAscendente { get; set; } = true;
    }
}
