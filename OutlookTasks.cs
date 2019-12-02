using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro
{
    public class OutlookTasks
    {
      

        public async Task GetTasks(string username,string password)
        {
            var handler = new HttpClientHandler();
            handler.Credentials = new NetworkCredential()
            {
                UserName = username ,
                Password = password
            };

            using (var client = new HttpClient(handler))
            {
                var url = "https://outlook.office365.com/api/v1.0/me/tasks";
                var result = await client.GetStringAsync(url);

                var data = JObject.Parse(result);

                foreach (var item in data["value"])
                {
                    Console.WriteLine(item["DisplayName"]);
                }
            }
        }
    }
}
