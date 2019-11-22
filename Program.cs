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
        static string FilePath = @"C:\Users\kevja\OneDrive\Pomodoro\Pomodoro.csv";
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

            Console.Write("Pomodoro Starting:");
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
            // Console.WriteLine(aTimer.Interval);
            Console.Write("\rElapsed time {0:HH:mm:ss.fff}", e.SignalTime);
         

        }


        //static void Main(string[] args)
        //{
        //    startTime = DateTime.Now;

        //    addTime = startTime.AddSeconds(10);

        //    Console.WriteLine($"Start Time : {startTime}");
        //    // Display the date/time when this method got called.
        //    Console.WriteLine("In TimerCallback: " + startTime);
        //    // Force a garbage collection to occur for this demo.

        //    Timer t = new Timer(TimerCallback, null, 0, 1000);
          
            
        //    // Wait for the user to hit <Enter>
        //    Console.ReadLine();

        //}

        //private static void TimerCallback(Object o)
        //{

        //    Console.WriteLine($"Current Time : {DateTime.Now}");
        //    if ( DateTime.Now > addTime)
        //    {
        //        Console.WriteLine("Pomodoro Over");
        //            Console.WriteLine("Was your Pomodoro succesful? y/n");
        //        successPomodoro = Console.ReadLine();

        //        var line = $"{startTime},{DateTime.Now.ToString()},{successPomodoro}";
        //        File.AppendAllText(FilePath, csv.ToString());
        //        string successPomodoro;

                  

        //        return;
                
        //    }
            
        //    GC.Collect();
        //}


    }
}
