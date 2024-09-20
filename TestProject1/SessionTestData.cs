using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public static class SessionTestData
    {

        public static List<BrainstormSession> GetTestSessions()
        {
            var sessions = new List<BrainstormSession>();
            sessions.Add(new BrainstormSession()
            {
                Id = 1,
                CreatedDate = new DateTime(2024, 7, 2),
                Name = "Test One"
            });

            sessions.Add(new BrainstormSession()
            {
                Id = 2,
                CreatedDate = new DateTime(2024, 7, 1),
                Name = "Test Two"
            });
            return sessions;
        }
    }
}
