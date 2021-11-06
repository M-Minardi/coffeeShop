using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using coffeeShop.DTOs;
using coffeeShop.Entidades;
using coffeeShop.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Logging;

namespace coffeeShop.Controllers.V1
{
    [ApiController]
    [Route("api/Productos")]
    public class ProductoController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<ProductoController> logger;        

        public ProductoController(ApplicationDbContext context,
            IMapper mapper,
            ILogger<ProductoController> logger)
            :base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet("filtro")]
        public async Task<ActionResult<List<ProductoDTO>>> Filtrar([FromQuery] FiltroProductosDTO filtroProductosDTO)
        {
            var ProductosQueryable = context.Productos.AsQueryable();

            if (!string.IsNullOrEmpty(filtroProductosDTO.Nombre))
            {
                ProductosQueryable = ProductosQueryable.Where(x => x.Nombre.Contains(filtroProductosDTO.Nombre));
            }

            if (filtroProductosDTO.Activo)
            {
                ProductosQueryable = ProductosQueryable.Where(x => x.Activo);
            }


            if (filtroProductosDTO.CategoriaId != 0)
            {
                ProductosQueryable = ProductosQueryable
                    .Where(x => x.ProductosCategorias.Select(y => y.CategoriaId)
                    .Contains(filtroProductosDTO.CategoriaId));
            }

            if (!string.IsNullOrEmpty(filtroProductosDTO.CampoOrdenar))
            {
                var tipoOrden = filtroProductosDTO.OrdenAscendente ? "ascending" : "descending";

                try
                {
                    ProductosQueryable = ProductosQueryable.OrderBy($"{filtroProductosDTO.CampoOrdenar} {tipoOrden}");

                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message, ex);
                }
            }

            await HttpContext.InsertarParametrosPaginacion(ProductosQueryable,
                filtroProductosDTO.CantidadRegistrosPorPagina);

            var Productos = await ProductosQueryable.Paginar(filtroProductosDTO.Paginacion).ToListAsync();

            return mapper.Map<List<ProductoDTO>>(Productos);
        }

        [HttpGet("{id}", Name = "obtenerProducto")]
        public async Task<ActionResult<ProductoDTO>> Get(int id)
        {
            var Producto = await context.Productos                
                .Include(x => x.ProductosCategorias).ThenInclude(x => x.Categoria)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (Producto == null)
            {
                return NotFound();
            }            

            return mapper.Map<ProductoDTO>(Producto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ProductoDTO ProductoCreacionDTO)
        {
            var Producto = mapper.Map<Producto>(ProductoCreacionDTO);
            context.Add(Producto);
            await context.SaveChangesAsync();
            var ProductoDTO = mapper.Map<ProductoDTO>(Producto);
            return new CreatedAtRouteResult("obtenerProducto", new { id = Producto.Id }, ProductoDTO);            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ProductoDTO ProductoCreacionDTO)
        {
            var ProductoDB = await context.Productos                
                .Include(x => x.ProductosCategorias)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (ProductoDB == null) { return NotFound(); }

            ProductoDB = mapper.Map(ProductoCreacionDTO, ProductoDB);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Producto>(id);
        }
    }
}
