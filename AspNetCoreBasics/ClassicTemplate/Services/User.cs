using System.Collections.Generic;

namespace ClassicTemplate.Services
{
    public record User(string FirstName, string LastName);

    public interface IUserService
    {
        public List<User> GetAll();

        public void Add(User person);
    }

    public class UserService : IUserService
    {
        private static List<User> _peoples = new();

        public void Add(User person) => _peoples.Add(person);

        public List<User> GetAll() => new(_peoples);
    }
}
