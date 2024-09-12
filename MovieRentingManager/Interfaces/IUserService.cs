using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentingManager.Interfaces
{
    public interface IUserService
    {
        bool AddUser(Models.User user);
        bool RemoveUser(int userId);

        int GetNextId();
      
        bool UpdateUser(Models.User user);

        IEnumerable<Models.User> GetUsers();
    }
}
