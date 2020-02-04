namespace MusicApp.Models
{
	public class Album
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Artist { get; set; }

		public string Category { get; set; }

		public bool Selected { get; set; } = false;
	}
}
