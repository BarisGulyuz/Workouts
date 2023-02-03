namespace ObserverPatternLikeMediatR.Models
{
    public class EventCollection
    {
        public EventCollection(Type @event, List<Type> eventHandlers)
        {
            Event = @event;
            EventHandlers = eventHandlers;
        }

        public Type Event { get; set; }
        public List<Type> EventHandlers { get; set; }

    }

}
