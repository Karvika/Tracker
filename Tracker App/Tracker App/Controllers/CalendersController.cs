using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System.Threading;
using System.Web.Mvc;

namespace Tracker_App.Controllers
{
    public class CalendersController : Controller
    {
        // GET: Calenders
        public ActionResult Index()
        {
                // Create the credentials object
                UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets { ClientId = "", ClientSecret = "YOUR_CLIENT_SECRET" },
                    new[] { CalendarService.Scope.Calendar },
                    "user",
                    CancellationToken.None).Result;

                // Create the Calendar service
                CalendarService service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "YOUR_APPLICATION_NAME"
                });

                // Example: List the user's first 10 calendar events
                EventsResource.ListRequest request = service.Events.List("primary");
                request.TimeMin = DateTime.Now;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.MaxResults = 10;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
                Events events = request.Execute();

                // Pass the events to the view using a model
                return View(events);
            

        }
        public ActionResult Calender()
        {
            return View();
        }
    }
}