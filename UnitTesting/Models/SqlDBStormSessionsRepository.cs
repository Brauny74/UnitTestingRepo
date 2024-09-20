using Microsoft.EntityFrameworkCore;
using UnitTesting.Data;
using UnitTesting.Interfaces;

namespace UnitTesting.Models
{
	public class SqlDBStormSessionsRepository : IBrainStormSessionRepository
	{
		private readonly UnitTestingContext _dbContext;

		public SqlDBStormSessionsRepository(UnitTestingContext dbContext)
		{ 
			_dbContext = dbContext;
		}

		public Task AddAsync(BrainstormSession session)
		{
			_dbContext.Sessions.Add(session);
			return _dbContext.SaveChangesAsync();
		}

		public Task<BrainstormSession> GetByIdAsync(int id)
		{
			return _dbContext.Sessions.Include(s => s.Ideas)
				.FirstOrDefaultAsync(s => s.Id == id);
		}

		public Task<List<BrainstormSession>> ListAsync()
		{
			return _dbContext.Sessions.Include(s => s.Ideas)
			.OrderByDescending(s => s.CreatedDate)
			.ToListAsync();
		}

		public Task UpdateAsync(BrainstormSession session)
		{
			_dbContext.Entry(session).State = EntityState.Modified;
			return _dbContext.SaveChangesAsync();
		}
	}
}
