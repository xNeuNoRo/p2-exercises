using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository.Models;

namespace Repository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditorialController : ControllerBase
    {
        private readonly IEditorial _service;
        public EditorialController(IEditorial service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Editorial>), StatusCodes.Status200OK)]
        public ActionResult GetAll()
        {
            var resultado = _service.GetEditoriales();
            return Ok(resultado);
        }

        [HttpPost]
        public void Create(Editorial modelo)
        {
            _service.SetEditorial(modelo);
        }
    }
}
