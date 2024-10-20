using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lb1_balayan.Data;
using Lb1_balayan.Models;
using System.Collections.Generic;
using System.Linq;

namespace Lb1_balayan.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly BeautySalonContext _context;

        // Конструктор для получения контекста базы данных
        public AppointmentsController(BeautySalonContext context)
        {
            _context = context;
        }

        // Получить все записи
        [HttpGet]
        public ActionResult<IEnumerable<Appointment>> GetAppointments()
        {
            return _context.Appointments.ToList();
        }

        // Получить запись по ID
        [HttpGet("{id}")]
        public ActionResult<Appointment> GetAppointment(int id)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
                return NotFound();
            return appointment;
        }

        // Создать новую запись
        [HttpPost]
        public ActionResult<Appointment> CreateAppointment(Appointment appointment)
        {
            // Проверяем, существует ли стилист с указанным ID
            var stylist = _context.Stylists.FirstOrDefault(b => b.Id == appointment.StylistId);
            if (stylist == null)
                return NotFound("Стилист не найден.");

            // Проверяем, существует ли клиент с указанным ID
            var client = _context.Clients.FirstOrDefault(c => c.Id == appointment.ClientId);
            if (client == null)
                return NotFound("Клиент не найден.");

            // Проверяем, доступен ли стилист в указанное время
            if (!stylist.IsAvailable(appointment.AppointmentTime))
                return BadRequest("Стилист недоступен в выбранное время.");

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        // Обновить существующую запись
        [HttpPut("{id}")]
        public IActionResult UpdateAppointment(int id, Appointment appointment)
        {
            var existingAppointment = _context.Appointments.FirstOrDefault(a => a.Id == id);
            if (existingAppointment == null)
                return NotFound();

            var stylist = _context.Stylists.FirstOrDefault(b => b.Id == appointment.StylistId);
            if (stylist == null)
                return NotFound("Стилист не найден.");

            if (!stylist.IsAvailable(appointment.AppointmentTime))
                return BadRequest("Стилист недоступен в выбранное время.");

            // Обновляем данные существующей записи
            existingAppointment.AppointmentTime = appointment.AppointmentTime;
            existingAppointment.StylistId = appointment.StylistId;
            existingAppointment.ClientId = appointment.ClientId;

            _context.SaveChanges();
            return Ok(existingAppointment);
        }

        // Удалить запись по ID
        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
                return NotFound();

            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
