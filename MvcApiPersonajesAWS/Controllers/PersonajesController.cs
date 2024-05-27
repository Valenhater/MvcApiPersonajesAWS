using Microsoft.AspNetCore.Mvc;
using MvcApiPersonajesAWS.Models;
using MvcApiPersonajesAWS.Services;

namespace MvcApiPersonajesAWS.Controllers
{
    public class PersonajesController : Controller
    {
        private ServiceApiPersonajes service;

        public PersonajesController(ServiceApiPersonajes service)
        {
            this.service = service;
        }
    
        public async Task<IActionResult> Index()
        {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            return View(personajes);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Personaje personaje)
        {
            await this.service.CreatePersonajeAsync(personaje.Nombre, personaje.Imagen);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update()
        {
            return View();
        }
        [HttpPut]
        public async Task<IActionResult> Update(Personaje personaje, int id)
        {
            id = personaje.IdPersonaje;
            await this.service.UpdatePersonajeAsync(id, personaje.Nombre, personaje.Imagen);
            return RedirectToAction("Index");
        }
    }
}
