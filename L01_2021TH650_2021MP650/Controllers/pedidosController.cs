using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2021TH650_2021MP650.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2021TH650_2021MP650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidosController : ControllerBase
    {
        public readonly restauranteContext  _restauranteContext;
        
        public pedidosController(restauranteContext restaurantecontext) {
            _restauranteContext = restaurantecontext;
        
        
        }
        [HttpGet]
        [Route("GetAllPedidos")]
        public IActionResult Get()
        {
            List<pedidos> listadopedidos = (from p in _restauranteContext.pedidos
                                            select p).ToList();
            if(listadopedidos.Count() ==0)
            {
                return NotFound();
            }
            return Ok(listadopedidos);

        }
        [HttpGet]
        [Route("FindPedidoCliente/{filtro}")]
        public IActionResult FindByClienteId(int filtro)
        {
            pedidos? pedido = (from p in _restauranteContext.pedidos
                               where p.clienteId == filtro
                               select p).FirstOrDefault();
            if ( pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }
        [HttpGet]
        [Route("FindPedidoMotorista/{filtro}")]
        public IActionResult FindByMotorista(int filtro)
        {
            pedidos? pedido = (from p in _restauranteContext.pedidos
                               where p.motoristaId == filtro
                               select p).FirstOrDefault();
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }
        [HttpPost]
        [Route("AddPedido")]
        public IActionResult Guardarpedido([FromBody] pedidos pedido)
        {
            try
            {
                _restauranteContext.pedidos.Add(pedido);
              _restauranteContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("actualizarpedido/{id}")]

        public IActionResult Actualizarpedido(int id, [FromBody] pedidos pedidosModificar)
        {
            try
            {

                pedidos? pedidoActual = (from e in _restauranteContext.pedidos
                                         where e.pedidoId == id
                                         select e).FirstOrDefault();

                
                if (pedidoActual == null)
                {
                    return NotFound();
                }

              

                pedidoActual.motoristaId = pedidosModificar.motoristaId;
                pedidoActual.clienteId = pedidosModificar.clienteId;
                pedidoActual.platoId = pedidosModificar.platoId;
                pedidoActual.cantidad = pedidosModificar.cantidad;
                pedidoActual.precio = pedidosModificar.precio;
                

                

                _restauranteContext.Entry(pedidoActual).State = EntityState.Modified;
                _restauranteContext.SaveChanges();

                return Ok(pedidosModificar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult Eliminarpedido(int id)
        {
            try
            {
               
                pedidos? pedido = (from p in _restauranteContext.pedidos
                                   where p.pedidoId == id
                                   select p).FirstOrDefault();

                //Verificamos que exista el registro según su id
                if (pedido == null)
                {
                    return NotFound();
                }

                //Ejecutamos la acción de eliminar el registro 
               _restauranteContext.pedidos.Attach(pedido);
                _restauranteContext.pedidos.Remove(pedido);
                _restauranteContext.SaveChanges();

                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
