﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO;

namespace DirectumTestTask.Models
{
    public class MeetingManager
    {
        private readonly List<Meeting> meetings = new List<Meeting>();

        public void AddMeeting(DateTime start, DateTime end, string description, TimeSpan reminder)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении встречи: {ex.Message}");
            }
        }

        public void EditMeeting(int index, DateTime start, DateTime end, string description, TimeSpan reminder)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при редактировании встречи: {ex.Message}");
            }
        }

        public void DeleteMeeting(int index)
        {
            try
            {
                if (index < 0 || index >= meetings.Count)
                {
                    Console.WriteLine("Ошибка: Встреча не найдена.");
                    return;
                }
                meetings.RemoveAt(index);
                Console.WriteLine("Встреча удалена успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении встречи: {ex.Message}");
            }
        }

        public void ViewMeetings(DateTime date)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при просмотре встреч: {ex.Message}");
            }
        }

        public void ExportMeetings(DateTime date, string fileName)
        {
            try
            {
                var dailyMeetings = meetings.Where(m => m.StartTime.Date == date.Date).ToList();
                if (dailyMeetings.Count == 0)
                {
                    Console.WriteLine("В этот день встреч нет для экспорта.");
                    return;
                }
                using (var writer = new StreamWriter(fileName))
                {
                    foreach (var meeting in dailyMeetings)
                    {
                        writer.WriteLine($"Встреча: {meeting.Description}, Начало: {meeting.StartTime}, Конец: {meeting.EndTime}, Напоминание: {meeting.Reminder}");
                    }
                }
                Console.WriteLine("Расписание встреч экспортировано успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при экспорте встреч: {ex.Message}");
            }
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
            Console.WriteLine($"Напоминание: {meeting.Description} начинается в {meeting.StartTime}");
            Console.WriteLine($"******\n");
        }
    }
}
