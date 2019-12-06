using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;

namespace Pomodoro
{
    class Program
    {
        private static System.Timers.Timer aTimer;

        static DateTime startTime;
        static DateTime endTime;
       
        static string FilePath = @"C:\Users\kneel.LJ\Desktop\Pomodoro.csv";
        //static string FilePath = @"C:\Users\kevja\OneDrive\Pomodoro\Pomodoro.csv";
        static string csv = string.Empty;

        public static double currentInterval;
        public static int stopTime = 10000;
        public static string success = "";

 

        public static void Main()
        {
            int minutes = 0;

            int timerSeconds = 10;
            int pomodoroSeconds =  (minutes * 60) + timerSeconds;
            startTime = DateTime.Now;

            Console.Write("Pomodoro Starting :");

            Console.WriteLine("Pomodoro Tasks:");

            Console.WriteLine("What do you want to use?");
            Console.WriteLine("Option 1: Google");
            //Console.WriteLine("Option 2: Microsoft");
            var option = Console.ReadLine();
            var tasks = new List<Task>();
            if(option == "1")
            {
                var itemNumber = 1;
                var googleTasks = new GoogleTasks();
                tasks = googleTasks.GetTasks().Where(x => !String.IsNullOrWhiteSpace(x.Title)).ToList().Select((x, i) => new Task()
                {
                     ItemNumber = i + 1,
                     Title = x.Title

                }).ToList();
                foreach (var task in tasks)
                {
                        Console.WriteLine("{0} ({1})",itemNumber, task.Title);
                        itemNumber++;
                }
            }
            else
            {
                var outlookTasks = new OutlookTasks();
                Console.WriteLine("Username:");
                var username = Console.ReadLine();
                Console.WriteLine("Password:");
                var password = Console.ReadLine();
                var m = outlookTasks.GetTasks(username, password);
            }
            var taskNumber = Convert.ToInt32(Console.ReadLine());

            for (int a = pomodoroSeconds; a >= 0; a--)
            {
                Console.CursorLeft = 22;
                minutes = a / 60;
                var secondsInMinute = a - (60 * minutes);
                Console.Write($"{minutes}:{secondsInMinute} ");    // Add space to make sure to override previous contents
                System.Threading.Thread.Sleep(1000);
            }
            endTime = DateTime.Now;
            Console.WriteLine("\nWas the pomodoro a success?");

            var taskTitle = tasks.Where(x => x.ItemNumber == taskNumber).SingleOrDefault().Title;
            var success = Console.ReadLine();
            var csvLine = $"{startTime},{endTime},{pomodoroSeconds},{success},{taskTitle}\n";
            File.AppendAllText(FilePath, csvLine.ToString());

            Console.WriteLine("Terminating the application...");
        }



        public class Task
        {
            public int ItemNumber { get; set; }
            public string Title { get; set; }
        }


    }
}
