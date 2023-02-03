namespace ObserverPatternLikeMediatR.Interfaces
{
    public interface IEventHandler<T> where T : class, IEvent
    {
        void Handle(T value);

    }
}

