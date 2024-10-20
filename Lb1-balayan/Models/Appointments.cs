using Lb1_balayan.Models;
using System;

namespace Lb1_balayan.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int StylistId { get; set; }
        public int ClientId { get; set; }
        public DateTime AppointmentTime { get; set; }

        // Метод бизнес-логики: Переназначение времени записи
        public void Reschedule(DateTime newTime, Stylist stylist)
        {
            if (stylist.IsAvailable(newTime))
            {
                AppointmentTime = newTime;
            }
            else
            {
                throw new Exception("Стилист недоступен в новое время.");
            }
        }
    }
}
