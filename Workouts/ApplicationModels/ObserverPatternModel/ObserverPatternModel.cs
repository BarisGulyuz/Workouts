using ObserverPatternLikeMediatR.Interfaces;

namespace Workouts.ApplicationModels.ObserverPatternModel
{
    internal class ObserverPatternModel
    {

        #region Event
        public class UserCreated : IEvent
        {
            public int Id { get; set; }
        }
        #endregion

        #region EventHandlers
        public class UserCreatedMailSenHandler : IEventHandler<UserCreated>
        {
            public UserCreatedMailSenHandler()
            {

            }
            public void Handle(UserCreated value)
            {
                Console.WriteLine("Mail Gönderildi {0}", value.Id);
            }
        }


        public class UserCreatedLogHandler : IEventHandler<UserCreated>
        {
            public UserCreatedLogHandler()
            {

            }
            public void Handle(UserCreated value)
            {
                Console.WriteLine("Log Oluşturuldu {0}", value.Id);
            }
        }

        public class UserCreatedSecurityCheckHandler : IEventHandler<UserCreated>
        {
            public UserCreatedSecurityCheckHandler()
            {

            }
            public void Handle(UserCreated value)
            {
                Console.WriteLine("Güvenlik Testleri Yapıldı{0}", value.Id);
            }
        }

        #endregion

    }
}
