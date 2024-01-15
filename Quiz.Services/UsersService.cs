using Newtonsoft.Json;
using Quiz.Core.Interfaces.Database;
using Quiz.Core.Interfaces.Services;
using Quiz.Core.Models.Database.Entities;

namespace Quiz.Services
{
    public class UsersService : IUsersService
    {
        private readonly IDatabaseTable<User> _usersTable;

        public UsersService(IDatabaseTable<User> usersTable)
        {
            _usersTable = usersTable;
        }

        public async Task AddUsersFromJson()
        {
            using StreamReader reader = new("../Quiz.API/users.json");
            var json = reader.ReadToEnd();

            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(json);
            if (users is null)
            {
                return;
            }
            var dbUsers = await _usersTable.GetAllAsync();

            users = users.Except(dbUsers);

            _usersTable.BulkAdd(users);
            await _usersTable.CommitAsync();
        }
    }
}
