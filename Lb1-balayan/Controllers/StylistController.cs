using Lb1_balayan.Data;
using Lb1_balayan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Lb1_balayan.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class StylistsController : ControllerBase
    {
        private readonly BeautySalonContext _context;

        // Конструктор для получения контекста базы данных
        public StylistsController(BeautySalonContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Stylist>> GetStylists()
        {
            return _context.Stylists.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Stylist> GetStylist(int id)
        {
            var stylist = _context.Stylists.FirstOrDefault(s => s.Id == id);
            if (stylist == null)
                return NotFound();
            return stylist;
        }

        [HttpPost]
        public ActionResult<Stylist> CreateStylist(Stylist stylist)
        {
            _context.Stylists.Add(stylist);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStylist), new { id = stylist.Id }, stylist);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStylist(int id, Stylist stylist)
        {
            var existingStylist = _context.Stylists.FirstOrDefault(s => s.Id == id);
            if (existingStylist == null)
                return NotFound();

            existingStylist.Name = stylist.Name;
            existingStylist.ExperienceLevel = stylist.ExperienceLevel;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStylist(int id)
        {
            var stylist = _context.Stylists.FirstOrDefault(s => s.Id == id);
            if (stylist == null)
                return NotFound();

            _context.Stylists.Remove(stylist);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
