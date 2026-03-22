using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository.Models;

namespace Repository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoria _service;
        public CategoriaController(ICategoria service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Categoria>), StatusCodes.Status200OK)]
        public ActionResult GetAll()
        {
            var resultado = _service.GetCategorias();//esta linea me da error: System.NullReferenceException: 'Object reference not set to an instance of an object.'
            return Ok(resultado);
        }

        [HttpPost]
        public void Create(Categoria modelo)
        {
            _service.SetCategoria(modelo);
        }
    }
}
