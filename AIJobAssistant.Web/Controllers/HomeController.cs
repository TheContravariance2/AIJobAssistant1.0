using Microsoft.AspNetCore.Mvc;
using AIJobAssistant.Web.Models;
using AIJobAssistant.Web.Services;

namespace AIJobAssistant.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

        [HttpPost]
        public IActionResult UploadResume(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return View("Index");

            var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsDir))
                Directory.CreateDirectory(uploadsDir);

            var filePath = Path.Combine(uploadsDir, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
                file.CopyTo(stream);

            var text = FileTextExtractor.ExtractText(filePath);

            var model = new ResumeModel
            {
                UploadedFilePath = filePath,
                ExtractedText = text
            };

            TempData["ResumePath"] = model.UploadedFilePath;
            TempData["ResumeText"] = model.ExtractedText;

            return RedirectToAction("Search", "Job");
        }

    }
}
