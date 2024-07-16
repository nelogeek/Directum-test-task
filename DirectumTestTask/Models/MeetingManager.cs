using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectumTestTask.Models
{
    public class MeetingManager
    {
        private readonly List<Meeting> meetings = [];
        public void AddMeeting(DateTime start, DateTime end, string description, TimeSpan reminder)
        {
            if (meetings.Any(m => (start < m.EndTime && end > m.StartTime)))
            {
                Console.WriteLine("Ошибка: Время встречи пересекается с существующими встречами.");
                return;
            }
            var meeting = new Meeting { StartTime = start, EndTime = end, Description = description, Reminder = reminder };
            meetings.Add(meeting);
            Console.WriteLine("Встреча добавлена успешно.");
            ScheduleReminder(meeting);
        }
        public void EditMeeting(int index, DateTime start, DateTime end, string description, TimeSpan reminder)
        {
            if (index < 0 || index >= meetings.Count)
            {
                Console.WriteLine("Ошибка: Встреча не найдена.");
                return;
            }
            var meeting = meetings[index];
            meeting.StartTime = start;
            meeting.EndTime = end;
            meeting.Description = description;
            meeting.Reminder = reminder;
            Console.WriteLine("Встреча отредактирована успешно.");
            ScheduleReminder(meeting);
        }
        public void DeleteMeeting(int index)
        {
            if (index < 0 || index >= meetings.Count)
            {
                Console.WriteLine("Ошибка: Встреча не найдена.");
                return;
            }
            meetings.RemoveAt(index);
            Console.WriteLine("Встреча удалена успешно.");
        }
        public void ViewMeetings(DateTime date)
        {
            var dailyMeetings = meetings.Where(m => m.StartTime.Date == date.Date).ToList();
            if (dailyMeetings.Count == 0)
            {
                Console.WriteLine("В этот день встреч нет.");
                return;
            }
            foreach (var meeting in dailyMeetings)
            {
                Console.WriteLine($"Встреча: {meeting.Description}, Начало: {meeting.StartTime}, Конец: {meeting.EndTime}, Напоминание: {meeting.Reminder}");
            }
        }
        public void ExportMeetings(DateTime date, string fileName)
        {
            var dailyMeetings = meetings.Where(m => m.StartTime.Date == date.Date).ToList();
            if (dailyMeetings.Count == 0)
            {
                Console.WriteLine("В этот день встреч нет для экспорта.");
                return;
            }
            using (var writer = new System.IO.StreamWriter(fileName))
            {
                foreach (var meeting in dailyMeetings)
                {
                    writer.WriteLine($"Встреча: {meeting.Description}, Начало: {meeting.StartTime}, Конец: {meeting.EndTime}, Напоминание: {meeting.Reminder}");
                }
            }
            Console.WriteLine("Расписание встреч экспортировано успешно.");
        }
        private void ScheduleReminder(Meeting meeting)
        {
            var reminderTime = meeting.StartTime - meeting.Reminder;
            var delay = (int)(reminderTime - DateTime.Now).TotalMilliseconds;
            if (delay > 0)
            {
                new Timer(ReminderCallback, meeting, delay, Timeout.Infinite);
            }
        }
        private void ReminderCallback(object state)
        {
            var meeting = (Meeting)state;
            Console.WriteLine($"\n******");
            Console.WriteLine($"\nНапоминание: {meeting.Description} начинается в {meeting.StartTime}");
            Console.WriteLine($"\n******\n");
        }
    }
}
