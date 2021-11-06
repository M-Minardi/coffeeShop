using AutoMapper;
using Microsoft.AspNetCore.Identity;
using coffeeShop.DTOs;
using coffeeShop.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace coffeeShop.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<IdentityUser, UsuarioDTO>();
            CreateMap<Categoria, CategoriaDTO>()
                .ForMember(x => x.Producto, options => options.MapFrom(MapProductosCategorias));
            CreateMap<CategoriaDTO, Categoria>()
                .ForMember(x => x.ProductosCategorias, options => options.MapFrom(MapProductosCategorias));
            CreateMap<Producto, ProductoDTO>()
                .ForMember(x => x.Categoria, options => options.MapFrom(MapProductosCategorias));
            CreateMap<ProductoDTO, Producto>()
                .ForMember(x => x.ProductosCategorias, options => options.MapFrom(MapProductosCategorias));
            CreateMap<Pedido, PedidoDTO>()
                .ForMember(x => x.PedidoItem, options => options.MapFrom(MapPedidoItems));
            CreateMap<PedidoDTO, Pedido>()
                .ForMember(x => x.PedidoItems, options => options.MapFrom(MapPedidoItems));
            CreateMap<PedidoItem, PedidoItemDTO>().ReverseMap();			
        }


        private List<CategoriaDTO> MapProductosCategorias(Producto producto, ProductoDTO productoDTO)
        {
            var resultado = new List<CategoriaDTO>();
            if (producto.ProductosCategorias == null) { return resultado; }
            foreach (var categoriaProducto in producto.ProductosCategorias)
            {
                resultado.Add(new CategoriaDTO() { Id = categoriaProducto.CategoriaId});
            }

            return resultado;
        }

        private List<ProductosCategorias> MapProductosCategorias(ProductoDTO ProductoDTO, Producto Producto)
        {
            var resultado = new List<ProductosCategorias>();
            if (ProductoDTO.CategoriasIDs == null) { return resultado; }
            foreach (var id in ProductoDTO.CategoriasIDs)
            {
                resultado.Add(new ProductosCategorias() { CategoriaId = id });
            }

            return resultado;
        }
        private List<ProductoDTO> MapProductosCategorias(Categoria categoria, CategoriaDTO categoriaDTO)
        {
            var resultado = new List<ProductoDTO>();
            if (categoria.ProductosCategorias == null) { return resultado; }
            foreach (var productoCategoria in categoria.ProductosCategorias)
            {
                resultado.Add(new ProductoDTO()
                {
                    Id = productoCategoria.ProductoId,
                    Nombre = productoCategoria.Producto.Nombre,
                    Descripcion = productoCategoria.Producto.Descripcion,
                    Activo = productoCategoria.Producto.Activo,
                    Valor = productoCategoria.Producto.Valor
                });
            }

            return resultado;
        }
        private List<ProductosCategorias> MapProductosCategorias(CategoriaDTO CategoriaDTO, Categoria Categoria)
        {
            var resultado = new List<ProductosCategorias>();
            if (CategoriaDTO.ProductoIDs == null) { return resultado; }
            foreach (var id in CategoriaDTO.ProductoIDs)
            {
                resultado.Add(new ProductosCategorias() { ProductoId = id });
            }

            return resultado;
        }
        private List<PedidoItemDTO> MapPedidoItems(Pedido ent, PedidoDTO eDTO)
        {
            var resultado = new List<PedidoItemDTO>();
            if (ent.PedidoItems == null) { return resultado; }
            foreach (var pedidoItem in ent.PedidoItems)
            {
                resultado.Add(new PedidoItemDTO()
                {
                    PedidoID = pedidoItem.PedidoId,
                    ProductoID = pedidoItem.ProductoId,
                    Producto = new ProductoDTO()
                    {
                        Id = pedidoItem.Producto.Id,
                        Nombre = pedidoItem.Producto.Nombre,
                        Descripcion = pedidoItem.Producto.Descripcion,
                        Activo = pedidoItem.Producto.Activo,
                        Valor = pedidoItem.Producto.Valor
                    },
                    Cantidad = pedidoItem.Cantidad,
                    Valor = pedidoItem.Valor
                });
            }

            return resultado;
        }
        private List<PedidoItem> MapPedidoItems(PedidoDTO eDTO, Pedido ent)
        {
            var resultado = new List<PedidoItem>();
            if (eDTO.PedidoItemIDs == null) { return resultado; }
            foreach (var id in eDTO.PedidoItemIDs)
            {
                resultado.Add(new PedidoItem() { PedidoId = id });
            }

            return resultado;
        }


    }
}
