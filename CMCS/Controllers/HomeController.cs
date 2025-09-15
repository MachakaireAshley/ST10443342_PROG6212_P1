using System.Diagnostics;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //I am adding data to show how the application will look. this data would usually come from the database

            var dashboard = new DashboardViewModel
            {
                PendingClaims = 55,
                RejectedClaims = 42,
                AcceptedClaims = 109,
                RecentClaims = new List<Claim>
                {
                    new Claim { ClaimId = 1, UserId = 1, SubmitDate = DateTime.Now.AddDays(-2),
                               Period = "September 2025", Amount = 4500.00m, Status = ClaimStatus.Pending, Workload = 18 },
                    new Claim { ClaimId = 2, UserId = 1, SubmitDate = DateTime.Now.AddDays(-5),
                               Period = "July 2025", Amount = 5200.00m, Status = ClaimStatus.Approved, Workload = 20 },
                    new Claim { ClaimId = 3, UserId = 1, SubmitDate = DateTime.Now.AddDays(-10),
                               Period = "August 2025", Amount = 3750.00m, Status = ClaimStatus.Rejected, Workload = 15 }

                },
                Notifications = new List<Notification>
                {
                    new Notification { NotificationId = 1, Content = "New claim submitted for review", Date = DateTime.Now.AddHours(-2), IsRead = false },
                    new Notification { NotificationId = 2, Content = "Document approved for claim CL-0042", Date = DateTime.Now.AddDays(-1), IsRead = true },
                    new Notification { NotificationId = 3, Content = "New claim submitted for review", Date = DateTime.Now.AddDays(-2), IsRead = true }
                },
                Messages = new List<Message>
                {
                    new Message { MessageId = 1, Sender = "Academic Manager", Content = "Please review the updated claim guidelines", Date = DateTime.Now.AddHours(-5), IsRead = false },
                    new Message { MessageId = 2, Sender = "System Administrator", Content = "Reminder: Claim deadline approaching", Date = DateTime.Now.AddDays(-1), IsRead = true }
                }
            };
            return View(dashboard);
        }

        // this will all be updated for part 2
        // for now it is to show the frontend for the UI  
        [HttpGet]
        public IActionResult SubmitClaim()
        {
            var model = new ClaimSubmissionViewModel
            {
                HourlyRate = 50
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitClaim(ClaimSubmissionViewModel model)
        {
            if (ModelState.IsValid)
            TempData["SuccessMessage"] = "Claim submitted successfully!";
            return RedirectToAction("Index");
        }
         return View(model);
        }

        [HttpGet]
        public IActionResult UploadDocuments()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadDocuments(List<IFormFile> files)
        {
            if (files != null && files.Count > 0)
            {
                TempData["SuccessMessage"] = $"{files.Count} document(s) uploaded successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Please select at least one document to upload.";
            }

            return RedirectToAction("Index");
        }

        public IActionResult GenerateReport()
        {
            
            return View();
        }

        public IActionResult ViewHistory()
        {

            var claims = new List<Claim>
            {
                new Claim { ClaimId = 1, SubmitDate = DateTime.Now.AddDays(-2), Period = "August 2025",
                           Amount = 4500.00m, Status = ClaimStatus.Pending, Workload = 18 },
                new Claim { ClaimId = 2, SubmitDate = DateTime.Now.AddDays(-5), Period = "July 2025",
                           Amount = 5200.00m, Status = ClaimStatus.Approved, Workload = 20.8m },
                new Claim { ClaimId = 3, SubmitDate = DateTime.Now.AddDays(-10), Period = "June 2025",
                           Amount = 3750.00m, Status = ClaimStatus.Rejected, Workload = 15 },
                new Claim { ClaimId = 4, SubmitDate = DateTime.Now.AddDays(-35), Period = "May 2025",
                           Amount = 6250.00m, Status = ClaimStatus.Approved, Workload = 25 },
                new Claim { ClaimId = 5, SubmitDate = DateTime.Now.AddDays(-65), Period = "May 2025",
                           Amount = 5000.00m, Status = ClaimStatus.Approved, Workload = 20 }
            };

            return View(claims);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
