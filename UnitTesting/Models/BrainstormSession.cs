namespace UnitTesting.Models
{
	public class BrainstormSession
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime CreatedDate { get; set; }
		public List<Idea> Ideas { get; } = new List<Idea>();
		public void AddIdea(Idea idea)
		{ 
			Ideas.Add(idea);
		}
	}

	public class Idea
	{ 
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
