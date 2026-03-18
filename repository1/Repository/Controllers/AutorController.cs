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
        private readonly IRepository<Autor> service;
        public AutorController(IRepository<Autor> service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult GetAll()
        { 
            return Ok(service.GetAll());
        }

        [HttpPost]
        public void Create(Autor modelo)
        {
            service.Set(modelo);
        }
    }
}
