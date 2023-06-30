namespace ObserverPatternLikeMediatR.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventHandler<T> where T : class, IEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        void Handle(T value);
    }
}

