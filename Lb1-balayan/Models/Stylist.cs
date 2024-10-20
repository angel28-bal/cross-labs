namespace Lb1_balayan.Models
{
    public class Stylist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ExperienceLevel { get; set; }

        // Метод бизнес-логики: Проверка доступности стилиста
        public bool IsAvailable(DateTime time)
        {
            // Пример простой логики
            return time.Hour >= 9 && time.Hour <= 18;
        }
    }
}
