using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Tasks.v1;
using Google.Apis.Tasks.v1.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Pomodoro
{
   public class GoogleTasks
    {
         string[] Scopes = { TasksService.Scope.TasksReadonly };
         string ApplicationName = "Google Tasks API .NET Quickstart";

        public void Run()
        {
            UserCredential credential;

            using (var stream =
                new FileStream(@"C:\Users\kneel.LJ\Desktop\credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Tasks API service.
            var service = new TasksService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            TasklistsResource.ListRequest listRequest = service.Tasklists.List();
            listRequest.MaxResults = 10;

            // List task lists.
            IList<TaskList> taskLists = listRequest.Execute().Items;
            Console.WriteLine("Task Lists:");

            if (taskLists != null && taskLists.Count > 0)
            {
                foreach (var taskList in taskLists)
                {
                    Console.WriteLine("{0} ({1})", taskList.Title, taskList.Id);
                    // Define parameters of request.
                    TasksResource.ListRequest taskListRequest = service.Tasks.List(taskList.Id);
                    taskListRequest.MaxResults = 40;
                      var tasks = taskListRequest.Execute().Items;
                    foreach (var task in tasks)
                    {
                        Console.WriteLine("{0} ({1})", task.Title, task.Notes);
                    }
                }
            }
            else
            {
                Console.WriteLine("No task lists found.");
            }
        

        }

        public List<string> GetTasks()
        {
            return null;

        }
    }
}
