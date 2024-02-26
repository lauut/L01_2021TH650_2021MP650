using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_2021TH650_2021MP650.Models;

namespace L01_2021TH650_2021MP650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        public readonly restauranteContext _restauranteContext;

        public platosController(restauranteContext restaurantecontext) {
            _restauranteContext = restaurantecontext;
        }

        [HttpGet]
        [Route("GetAllPlatos")]
        public IActionResult Get()
        {
            List<platos> listadoplatos = (from p in _restauranteContext.platos
                                                  select p).ToList();
            if (listadoplatos.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoplatos);

        }
        [HttpPost]
        [Route("AddPlatos")]
        public IActionResult GuardarPlatos([FromBody] platos plato)
        {
            try
            {
                _restauranteContext.platos.Add(plato);
                _restauranteContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("ActualizarPlatos/{id}")]

        public IActionResult ActualizarMotoristas(int id, [FromBody] platos platosModificar)
        {
            try
            {

                platos? platoActual = (from m in _restauranteContext.platos
                                               where m.platoId == id
                                               select m).FirstOrDefault();


                if (platoActual == null)
                {
                    return NotFound();
                }



                platoActual.nombrePlato = platosModificar.nombrePlato;
                platoActual.precio = platosModificar.precio;

                _restauranteContext.Entry(platoActual).State = EntityState.Modified;
                _restauranteContext.SaveChanges();

                return Ok(platosModificar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
