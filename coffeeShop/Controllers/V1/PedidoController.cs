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
using System.Security.Claims;

namespace coffeeShop.Controllers.V1
{
    [ApiController]
    [Route("api/Pedidos")]
    public class PedidosController: CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;        

        public PedidosController(ApplicationDbContext context,
            IMapper mapper)
            :base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;            
        }

        [HttpGet]
        public async Task<ActionResult<List<PedidoDTO>>> Get()
        {
            return await Get<Pedido, PedidoDTO>();
        }

        [HttpGet("{id:int}", Name = "obtenerPedido")]
        public async Task<ActionResult<PedidoDTO>> Get(int id)
        {
            return await Get<Pedido, PedidoDTO>(id);
        }

        [HttpGet("detalles/{id:int}", Name = "obtenerPedidoDetalles")]
        public async Task<ActionResult<PedidoDTO>> GetConDetalles(int id)
        {
            var Pedido = await context.Pedidos
               .Include(x => x.PedidoItems).ThenInclude(x => x.Producto)
               .FirstOrDefaultAsync(x => x.Id == id);

            if (Pedido == null)
            {
                return NotFound();
            }

            return mapper.Map<PedidoDTO>(Pedido);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] PedidoDTO PedidoDTO)
        {
            var usuarioId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var pedido = mapper.Map<Pedido>(PedidoDTO);
            pedido.UsuarioID = usuarioId;

            context.Add(pedido);
            context.SaveChanges();
            if (PedidoDTO.PedidoItem != null && PedidoDTO.PedidoItem.Count > 0) {
                foreach (PedidoItemDTO itemDTO in PedidoDTO.PedidoItem)
                {
                    var item = mapper.Map<PedidoItem>(itemDTO);
                    item.PedidoId = pedido.Id;
                    context.PedidoItems.Add(item);                    
                }
            }

            await context.SaveChangesAsync();
            //var eDTO = mapper.Map<PedidoDTO>(pedido);
            return new CreatedAtRouteResult("obtenerPedido", new { id = pedido.Id }, PedidoDTO);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(int id, [FromBody] PedidoDTO PedidoDTO)
        {
            var pedidoDB = await context.Pedidos.FirstOrDefaultAsync(x => x.Id == id);
            if (pedidoDB == null) { return NotFound(); }

            var usuarioId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if (pedidoDB.UsuarioID != usuarioId)
            {
                return BadRequest("No tiene permisos de editar este pedido");
            }
            pedidoDB = mapper.Map(PedidoDTO, pedidoDB);

            await context.SaveChangesAsync();
            return NoContent();            
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Pedido>(id);
        }
    }
}
