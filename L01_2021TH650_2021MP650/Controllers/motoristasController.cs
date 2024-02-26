using L01_2021TH650_2021MP650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2021TH650_2021MP650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class motoristasController : ControllerBase
    {
        public readonly restauranteContext _restauranteContext;

        public motoristasController(restauranteContext restaurantecontext) {
            _restauranteContext = restaurantecontext;

        }


        [HttpGet]
        [Route("GetAllmotoristas")]
        public IActionResult Get()
        {
            List<motoristas> listadomotoristas = (from m in _restauranteContext.motoristas
                                            select m).ToList();
            if (listadomotoristas.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadomotoristas);

        }
        [HttpGet]
        [Route("Findmotorista/{filtro}")]
        public IActionResult FindByClienteId(int filtro)
        {
            pedidos? pedido = (from p in _restauranteContext.pedidos
                               where p.clienteId == filtro
                               select p).FirstOrDefault();
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }
        [HttpPost]
        [Route("AddMotoristas")]
        public IActionResult GuardarMotoristas([FromBody] pedidos pedido)
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


    }
}
