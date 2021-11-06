using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using coffeeShop.DTOs;
using coffeeShop.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace coffeeShop.Controllers.V1
{
    [ApiController]
    [Route("api/Categorias")]
    public class CategoriasController: CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;        

        public CategoriasController(ApplicationDbContext context,
            IMapper mapper)
            :base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;            
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriaDTO>>> Get()
        {
            return await Get<Categoria, CategoriaDTO>();
        }

        [HttpGet("{id:int}", Name = "obtenerCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            return await Get<Categoria, CategoriaDTO>(id);
        }

        [HttpGet("detalles/{id:int}", Name = "obtenerCategoriaDetalles")]
        public async Task<ActionResult<CategoriaDTO>> GetConDetalles(int id)
        {            
            var Categoria = await context.Categorias
               .Include(x => x.ProductosCategorias).ThenInclude(x => x.Producto)
               .FirstOrDefaultAsync(x => x.Id == id);

            if (Categoria == null)
            {
                return NotFound();
            }

            return mapper.Map<CategoriaDTO>(Categoria);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoriaDTO CategoriaDTO)
        {
            return await Post<CategoriaDTO, Categoria, CategoriaDTO>(CategoriaDTO, "obtenerCategoria");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoriaDTO CategoriaDTO)
        {
            return await Put<CategoriaDTO, Categoria>(id, CategoriaDTO);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Categoria>(id);
        }
    }
}
