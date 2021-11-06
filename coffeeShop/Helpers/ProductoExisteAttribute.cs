using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coffeeShop.Helpers
{
    public class ProductoExisteAttribute : Attribute, IAsyncResourceFilter
    {
        private readonly ApplicationDbContext Dbcontext;

        public ProductoExisteAttribute(ApplicationDbContext context)
        {
            this.Dbcontext = context;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, 
            ResourceExecutionDelegate next)
        {
            var productoIdObject = context.HttpContext.Request.RouteValues["productoId"];

            if (productoIdObject == null)
            {
                return;
            }

            var productoId = int.Parse(productoIdObject.ToString());

            var existeProducto = await Dbcontext.Productos.AnyAsync(x => x.Id == productoId);

            if (!existeProducto)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                await next();
            }
        }
    }
}
