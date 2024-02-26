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
        public IActionResult GuardarMotoristas([FromBody] motoristas motorista)
        {
            try
            {
                _restauranteContext.motoristas.Add(motorista);
                _restauranteContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("Actualizarmotoristas/{id}")]

        public IActionResult ActualizarMotoristas(int id, [FromBody] motoristas motoristasModificar)
        {
            try
            {

                motoristas? motoristaActual = (from m in _restauranteContext.motoristas
                                         where m.motoristaId == id
                                         select m).FirstOrDefault();


                if (motoristaActual == null)
                {
                    return NotFound();
                }



                motoristaActual.nombreMotorista = motoristasModificar.nombreMotorista;

                _restauranteContext.Entry(motoristaActual).State = EntityState.Modified;
                _restauranteContext.SaveChanges();

                return Ok(motoristasModificar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpDelete]
        [Route("EliminarMotorista/{id}")]

        public IActionResult Eliminarmotorista(int id)
        {
            try
            {

                motoristas? motorista = (from p in _restauranteContext.motoristas
                                   where p.motoristaId == id
                                   select p).FirstOrDefault();

                
                if (motorista == null)
                {
                    return NotFound();
                }

                
                _restauranteContext.motoristas.Attach(motorista);
                _restauranteContext.motoristas.Remove(motorista);
                _restauranteContext.SaveChanges();

                return Ok(motorista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
