/*
 * MediatR ile ObserverPattern (InMemory Event)
 * 
 */
namespace Workouts.DesignPatterns
{
    public class UserObserverWriteToConsole : IUserObserver
    {
        public void UserCreated(User user)
        {
            Console.WriteLine($"{user.Name}({user.Id}) created");
        }
    }
    public class UserObserverCreateDiscount : IUserObserver
    {
        public void UserCreated(User user)
        {
            int rate = new Random().Next(1, 100);
            Console.WriteLine($"{user.Name}'s discount rate : {rate}");
        }
    }
    public class UserObserverSendEmail : IUserObserver
    {
        public void UserCreated(User user)
        {
            Console.WriteLine($"{user.Name} Welcome Our WebSite");
            Console.WriteLine($"Email Sent To {user.Name}");
        }
    }
    public interface IUserObserver
    {
        void UserCreated(User user);
    }
    public class UserObserverSubject
    {
        private static UserObserverSubject _userObserverSubject;
        public static UserObserverSubject Instance = _userObserverSubject ?? (_userObserverSubject = new UserObserverSubject());

        private readonly List<IUserObserver> _userObservers;
        private UserObserverSubject()
        {
            _userObservers = new List<IUserObserver>();
            _userObservers.Add(new UserObserverWriteToConsole());
            _userObservers.Add(new UserObserverCreateDiscount());
            _userObservers.Add(new UserObserverSendEmail());
        }
        public void AddObserver(IUserObserver userObserver) => _userObservers.Add(userObserver);
        public void RemoveObserver(IUserObserver userObserver) => _userObservers.Remove(userObserver);
        public void Notify(User user)
        {
            foreach (IUserObserver userObserver in _userObservers)
            {
                userObserver.UserCreated(user);
            }
        }
    }

    #region Models
    public class User : Base
    {
        public int Name { get; set; }
    }
    public class Discount : Base
    {
        public int UserId { get; set; }
        public int Rate { get; set; }
    }
    public class Base
    {
        public int Id { get; set; }
    }
    public class UserService : IUserService
    {
        public void CreateUser(User user)
        {
            User createdUser = new User { Id = new Random().Next(1, 10000), Name = user.Name };
            UserObserverSubject.Instance.Notify(createdUser);
        }
    }
    public interface IUserService
    {
        void CreateUser(User user);
    }

    #endregion

}
