namespace AIJobAssistant.Web.Models
{
	public class JobViewModel
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Company { get; set; }
		public string Location { get; set; }
		public DateTime PostedDate { get; set; }
		public double Score { get; set; }
	}
}
