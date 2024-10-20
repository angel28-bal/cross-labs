namespace Lb1_balayan.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PreferredStyle { get; set; }

        // Метод бизнес-логики: Бронирование записи
        public Appointment BookAppointment(DateTime time, Stylist stylist)
        {
            if (stylist.IsAvailable(time))
            {
                return new Appointment
                {
                    Id = new Random().Next(1, 1000),
                    StylistId = stylist.Id,
                    ClientId = this.Id,
                    AppointmentTime = time
                };
            }
            else
            {
                throw new Exception("Стилист недоступен в выбранное время.");
            }
        }
    }
}
