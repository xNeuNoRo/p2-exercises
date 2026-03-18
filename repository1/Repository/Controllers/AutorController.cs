using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository.Models;
using Repository.Services;

namespace Repository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutor service;
        public AutorController(IAutor service)
        {
            this.service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Autor>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Autor>> GetAll()
        { 
            return Ok(service.GetAutores());
        }

        [HttpPost]
        public void Create(Autor modelo)
        {
            service.SetAutor(modelo);
        }
    }
}
