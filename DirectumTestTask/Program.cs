using DirectumTestTask.Models;
using System;
using System.Globalization;

namespace DirectumTestTask
{
    internal class Program
    {
        static void Main()
        {
            var manager = new MeetingManager();
            while (true)
            {
                Console.WriteLine("Меню управления встречами:");
                Console.WriteLine("1. Добавить встречу");
                Console.WriteLine("2. Редактировать встречу");
                Console.WriteLine("3. Удалить встречу");
                Console.WriteLine("4. Просмотреть встречи на день");
                Console.WriteLine("5. Экспортировать встречи на день");
                Console.WriteLine("6. Выход");
                Console.Write("Выберите пункт меню: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddMeeting(manager);
                        break;
                    case "2":
                        EditMeeting(manager);
                        break;
                    case "3":
                        DeleteMeeting(manager);
                        break;
                    case "4":
                        ViewMeetings(manager);
                        break;
                    case "5":
                        ExportMeetings(manager);
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                        break;
                }
            }
        }
        static void AddMeeting(MeetingManager manager)
        {
            Console.Write("Введите описание встречи: ");
            var description = Console.ReadLine();
            Console.Write("Введите дату и время начала встречи (dd.MM.yyyy HH:mm): ");
            var startTime = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            Console.Write("Введите дату и время окончания встречи (dd.MM.yyyy HH:mm): ");
            var endTime = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            Console.Write("Введите время напоминания (в минутах): ");
            var reminderMinutes = int.Parse(Console.ReadLine());
            manager.AddMeeting(startTime, endTime, description, TimeSpan.FromMinutes(reminderMinutes));
        }
        static void EditMeeting(MeetingManager manager)
        {
            Console.Write("Введите индекс встречи для редактирования: ");
            var index = int.Parse(Console.ReadLine());
            Console.Write("Введите новое описание встречи: ");
            var description = Console.ReadLine();
            Console.Write("Введите новую дату и время начала встречи (dd.MM.yyyy HH:mm): ");
            var startTime = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            Console.Write("Введите новую дату и время окончания встречи (dd.MM.yyyy HH:mm): ");
            var endTime = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            Console.Write("Введите новое время напоминания (в минутах): ");
            var reminderMinutes = int.Parse(Console.ReadLine());
            manager.EditMeeting(index, startTime, endTime, description, TimeSpan.FromMinutes(reminderMinutes));
        }
        static void DeleteMeeting(MeetingManager manager)
        {
            Console.Write("Введите индекс встречи для удаления: ");
            var index = int.Parse(Console.ReadLine());
            manager.DeleteMeeting(index);
        }
        static void ViewMeetings(MeetingManager manager)
        {
            Console.Write("Введите дату для просмотра встреч (dd.MM.yyyy): ");
            var date = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
            manager.ViewMeetings(date);
        }
        static void ExportMeetings(MeetingManager manager)
        {
            Console.Write("Введите дату для экспорта встреч (dd.MM.yyyy): ");
            var date = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
            Console.Write("Введите имя файла для экспорта: ");
            var fileName = Console.ReadLine();
            manager.ExportMeetings(date, fileName);
        }
    }
}
