using Microsoft.AspNetCore.Mvc;
using AIJobAssistant.Web.Models;

namespace AIJobAssistant.Web.Controllers
{
	public class JobController : Controller
	{
		[HttpGet]
		public IActionResult Search()
		{
            if (TempData["ResumeText"] == null)
                return RedirectToAction("Index", "Home");

            var model = new ResumeModel
            {
                UploadedFilePath = TempData["ResumePath"]?.ToString(),
                ExtractedText = TempData["ResumeText"]?.ToString()
            };

            return View(model); // You can now show the extracted text safely
		}

		[HttpPost]
		public IActionResult Results(string skills, string location)
		{
			// Mock jobs for now — will be replaced with API/scraper data
			var jobs = new List<JobViewModel>
			{
				new() { Title=".NET Developer", Description="C#, ASP.NET, SQL", Company="TechCorp", Location="Bangalore", PostedDate=DateTime.Now.AddDays(-1), Score=0.95 },
				new() { Title="Software Engineer", Description="Full stack .NET", Company="Innova", Location="Remote", PostedDate=DateTime.Now.AddDays(-2), Score=0.88 }
			};

			ViewBag.Skills = skills;
			return View(jobs);
		}
	}
}
