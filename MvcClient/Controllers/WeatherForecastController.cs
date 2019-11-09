

using MvcClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace MvcClient.Controllers
{
    public class WeatherForecastController : Controller
    {
        // GET: WeatherForcast
        public ActionResult Index()
        {
            IEnumerable<WeatherForecast> weather = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55781/");
                //HTTP GET
                var responseTask = client.GetAsync("weatherforecast");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    weather = JsonConvert.DeserializeObject<IList<WeatherForecast>>(readTask.Result);
                    
                }
                else
                {
                    weather = Enumerable.Empty<WeatherForecast>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(weather);
        }
    }
}

/*
 
 
     */
