using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Interfaces.Services
{
    public interface IUsersService
    {
        Task AddUsersFromJson();
    }
}
