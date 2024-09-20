using Microsoft.EntityFrameworkCore;
using UnitTesting.Models;

namespace UnitTesting.Data
{
	public class UnitTestingContext : DbContext
	{
		public UnitTestingContext(DbContextOptions<UnitTestingContext> options)
			: base(options)
		{ 			
		}

		public DbSet<BrainstormSession> Sessions { get; set; }
		public DbSet<Idea> Ideas { get; set; }
	}
}
