using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            int minutes = 5;

            int timerSeconds = 10;
            int pomodoroSeconds =  (minutes * 60) + timerSeconds;
            startTime = DateTime.Now;

            Console.Write("Pomodoro Starting :");

            Console.WriteLine("Pomodoro Tasks:");

            Console.WriteLine("What do you want to use?");
            Console.WriteLine("Option 1: Google");
            Console.WriteLine("Option 2: Microsoft");
            var option = Console.ReadLine();
            if(option == "1")
            {
                var googleTasks = new GoogleTasks();
                googleTasks.Run();
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

            var success = Console.ReadLine();
            var csvLine = $"{startTime},{endTime},{pomodoroSeconds},{success}\n";
            File.AppendAllText(FilePath, csvLine.ToString());

            Console.WriteLine("Terminating the application...");
        }

        private static void SetTimer()
        {
            startTime = DateTime.Now;
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(1000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
          
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            currentInterval += aTimer.Interval;
            if(currentInterval >= stopTime)
            {
                aTimer.Stop();
                endTime = e.SignalTime;
                return;

            }
            Console.Write("\rElapsed time {0:HH:mm:ss.fff}", e.SignalTime);
        }


    

    }
}
