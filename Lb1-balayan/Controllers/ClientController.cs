using Lb1_balayan.Data;
using Lb1_balayan.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Lb1_balayan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly BeautySalonContext _context;

        // Конструктор для получения контекста базы данных
        public ClientsController(BeautySalonContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Client>> GetClients()
        {
            return _context.Clients.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Client> GetClient(int id)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == id);
            if (client == null)
                return NotFound();
            return client;
        }

        [HttpPost]
        public ActionResult<Client> CreateClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, Client client)
        {
            var existingClient = _context.Clients.FirstOrDefault(c => c.Id == id);
            if (existingClient == null)
                return NotFound();
            existingClient.Name = client.Name;
            existingClient.PreferredStyle = client.PreferredStyle;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == id);
            if (client == null)
                return NotFound();
            _context.Clients.Remove(client);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
