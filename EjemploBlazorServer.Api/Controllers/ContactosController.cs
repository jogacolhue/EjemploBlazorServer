using EjemploBlazorServer.Shared.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EjemploBlazorServer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactosController : ControllerBase
    {
        private static readonly List<Contacto> contactos = GenerarContactos(5);

        private static List<Contacto> GenerarContactos(int number)
        {
            return Enumerable.Range(1, number).Select(index => new Contacto
            {
                Id = index,
                Name = $"First{index} Last{index}",
                PhoneNumber = $"+1 555 987{index}",
                City = "Tacna"
            }).ToList();
        }

        // GET: api/<ContactosController>
        [HttpGet]
        public ActionResult<List<Contacto>> Get()
        {
            return contactos;
        }

        // GET api/<ContactosController>/5
        [HttpGet("{id}")]
        public ActionResult<Contacto> Get(int id)
        {
            var contacto = contactos.FirstOrDefault((p) => p.Id == id);
            if (contacto == null)
                return NotFound();
            return contacto;
        }

        // POST api/<ContactosController>
        [HttpPost]
        public void Post([FromBody] Contacto contacto)
        {
            contacto.Id = contactos.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            contactos.Add(contacto);
        }

        // PUT api/<ContactosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Contacto contacto)
        {
            int index = contactos.FindIndex((p) => p.Id == id);
            if (index != -1)
                contactos[index] = contacto;
        }

        // DELETE api/<ContactosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            int index = contactos.FindIndex((p) => p.Id == id);
            if (index != -1)
                contactos.RemoveAt(index);
        }
    }
}