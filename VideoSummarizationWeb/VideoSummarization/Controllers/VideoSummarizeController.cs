using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using VideoSummarization.Models;

namespace VideoSummarization.Controllers
{
    [Authorize]
    public class VideoSummarizeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["url"] = "";
            ViewData["script"] = "";
            ViewData["summ"] = "";
            return View();
        }

        [HttpPost]
        public IActionResult Index(string url)
        {
            API_DT dT_Api = new API_DT
            {
                youtubeURL = url,
                transcript = null,
                transcriptSummary = null
            };

            HttpClient client = new HttpClient();

            var Rdata = JsonSerializer.Serialize(dT_Api);
            var requestContent = new StringContent(Rdata, Encoding.UTF8, "application/json");

            var response = client.PostAsync("http://127.0.0.1:8000/transcriptYoutubeVideo/", requestContent);
            response.Wait();
            var result = response.Result;

            if (result.IsSuccessStatusCode)
            {
                var dT = JsonSerializer.Deserialize<API_DT>(result.Content.ReadAsStringAsync().Result);
                if (dT == null) return NotFound();

                ViewData["url"] = dT.youtubeURL;
                ViewData["script"] = dT.transcript;
                ViewData["summ"] = dT.transcriptSummary;

            }
            return View();

            //API_DT dT = new API_DT
            //{
            //    youtubeURL = url,
            //    Questions = "[{\"Question\": \"1. question test1 ?\", \"A1\": \"A1\", \"A2\": \"A2\", \"A3\": \"A3\", \"A4\": \"A4\", \"answer\": \"AN\"}, {\"Question\": \"2. question test2 ?\", \"A1\": \"A1\", \"A2\": \"A2\", \"A3\": \"A3\", \"A4\": \"A4\", \"answer\": \"AN\"}, {\"Question\": \"3. question test3 ?\", \"A1\": \"A1\", \"A2\": \"A2\", \"A3\": \"A3\", \"A4\": \"A4\", \"answer\": \"AN\"}, {\"Question\": \"4. question test 4?\", \"A1\": \"A1\", \"A2\": \"A2\", \"A3\": \"A3\", \"A4\": \"A4\", \"answer\": \"AN\"}]",
            //    transcript = "I used 3 servos for this model. The model can take off and land from short distances. That's why the nose wheel is fixed. A rudder is also not required on this model. Since there is no rudder, servo cost is reduced. And the flight weight is also slightly reduced. The model is structured as simply as possible. But it has a stable flight. It can also fly at low speeds. It is also suitable for beginners.\r\n I used 3 servos for this model. The model can take off and land from short distances. That's why the nose wheel is fixed. A rudder is also not required on this model. Since there is no rudder, servo cost is reduced. And the flight weight is also slightly reduced. The model is structured as simply as possible. But it has a stable flight. It can also fly at low speeds. It is also suitable for beginners.\r\n\r\n I used 3 servos for this model. The model can take off and land from short distances. That's why the nose wheel is fixed. A rudder is also not required on this model. Since there is no rudder, servo cost is reduced. And the flight weight is also slightly reduced. The model is structured as simply as possible. But it has a stable flight. It can also fly at low speeds. It is also suitable for beginners.\r\n\r\n",
            //    transcriptSummary = "I used 3 servos for this model. The model can take off and land from short distances. That's why the nose wheel is fixed. A rudder is also not required on this model. Since there is no rudder, servo cost is reduced. And the flight weight is also slightly reduced. The model is structured as simply as possible. But it has a stable flight. It can also fly at low speeds. It is also suitable for beginners."
            //};


        }
        
       
        public IActionResult ShowResult(double result)
        {
            ViewData["Result"] = result.ToString();
            //return RedirectToAction("Index","Home");
            return View();
        }
    }
}
