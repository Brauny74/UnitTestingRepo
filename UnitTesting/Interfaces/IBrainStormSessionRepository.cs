using UnitTesting.Models;

namespace UnitTesting.Interfaces
{
	public interface IBrainStormSessionRepository
	{
		Task<BrainstormSession> GetByIdAsync(int id);
		Task<List<BrainstormSession>> ListAsync();
		Task AddAsync(BrainstormSession session);
		Task UpdateAsync(BrainstormSession session);
	}
}
