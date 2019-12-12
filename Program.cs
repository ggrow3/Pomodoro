using PomodoroTest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pomodoro
{
    class Program
    {

        static DateTime endTime;
       
        const string FilePath = @"C:\Users\kneel.LJ\Desktop\Pomodoro.csv";
        //static string FilePath = @"C:\Users\kevja\OneDrive\Pomodoro\Pomodoro.csv";
     

        public static int stopTime = 10000;
      


        //  https://github.com/ziyasal/FireSharp


        public static void Main()
        {

           

            Console.WriteLine("Pomodoro Tasks:");

            Console.WriteLine("What do you want to use?");
            Console.WriteLine("Option 1: Google");
            //Console.WriteLine("Option 2: Microsoft");
            var option = Console.ReadLine();
            var tasks = new List<PomodoroTask>();
            if (option == "1")
            {
                var itemNumber = 1;
                var googleTasks = new GoogleTasks();
                tasks = googleTasks.GetTasks().Where(x => !String.IsNullOrWhiteSpace(x.Title)).ToList().Select((x, i) => new PomodoroTask()
                {
                    ItemNumber = i + 1,
                    Title = x.Title

                }).ToList();
                foreach (var task in tasks)
                {
                    Console.WriteLine("{0} ({1})", itemNumber, task.Title);
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

            int minutes = 5;

            int timerSeconds = 0;

            Console.Write("How many minutes:seconds? ");
            
            var pomodoroTimeInput = Console.ReadLine();

            if(pomodoroTimeInput.Contains(":"))
            {
                var pti = pomodoroTimeInput.Split(":");
                minutes = Convert.ToInt32(pti[0]);
                timerSeconds = Convert.ToInt32(pti[1]);
            }
            else
            {
                minutes = Convert.ToInt32(pomodoroTimeInput);
            }

            
            int pomodoroSeconds = (minutes * 60) + timerSeconds;
            for (int a = pomodoroSeconds; a >= 0; a--)
            {
                Console.CursorLeft = 22;
                minutes = a / 60;
                var secondsInMinute = a - (60 * minutes);
                Console.Write($"{minutes}:{secondsInMinute} ");    // Add space to make sure to override previous contents
                System.Threading.Thread.Sleep(1000);
            }
            endTime = DateTime.Now;

            for (int i = 37; i <= 2000; i += 100)
            {
                Console.Beep(i, 100);
            }
            Console.WriteLine("\nWas the pomodoro a success?");

            var taskTitle = tasks.Where(x => x.ItemNumber == taskNumber).SingleOrDefault().Title;
            var success = Console.ReadLine();
            var csvLine = $"{endTime.AddSeconds(-pomodoroSeconds)},{endTime},{pomodoroSeconds},{success},{taskTitle}\n";
            File.AppendAllText(FilePath, csvLine.ToString());

            Console.WriteLine("Terminating the application...");

        }








    }
}
