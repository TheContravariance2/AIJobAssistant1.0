namespace AIJobAssistant.Web.Models
{
	public class ResumeModel
	{
		public string UploadedFilePath { get; set; }
		public string ExtractedText { get; set; }
		public List<string> Skills { get; set; } = new();
	}
}
