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
    }
}
