using ObserverPatternLikeMediatR.Interfaces;
using ObserverPatternLikeMediatR.Models;
using System.Reflection;

namespace ObserverPatternLikeMediatR.Concrete
{
    /// <summary>
    /// I tried to create a notification system like mediatr
    /// usage in Workouts.Program.cs
    /// </summary>
    public class MyEventHandler
    {
        private const string HANDLE_METHOD_NAME = "Handle";
        private static Assembly _assembly;

        private static List<EventCollection> _eventCollection;
        private static List<EventCollection> EventCollection => _eventCollection ?? (_eventCollection = FindEvents());

        private Dictionary<Type, object> eventHandlerIstanceDictionary = new Dictionary<Type, object>();

        public MyEventHandler(Assembly assembly)
        {
            _assembly = assembly;
        }

        public void Notify(IEvent @event) => NotifyEvent(@event);
        public void Notify(IEvent @event, bool continueOnError) => NotifyEvent(@event, continueOnError);
        public void Notify(IEvent @event, bool continueOnError, Action<Exception> exceptionHandler) => NotifyEvent(@event, continueOnError, exceptionHandler);

        private void NotifyEvent(IEvent @event, bool continueOnError = true, Action<Exception> exceptionHandler = null)
        {
            ArgumentNullException.ThrowIfNull(@event);

            EventCollection currentEventInfo = EventCollection.FirstOrDefault(e => e.Event == @event.GetType());
            foreach (Type handlerType in currentEventInfo.EventHandlers)
            {
                try
                {
                    var handlerInstance = GetHandlerIstance(handlerType);
                    _ = handlerInstance.GetType()
                                       .GetMethod(HANDLE_METHOD_NAME)
                                       .Invoke(handlerInstance, new object[] { @event });
                }
                catch (Exception ex)
                {
                    if (exceptionHandler != null)
                        exceptionHandler(ex);
                    else
                    {
                        //log exception here
                    }

                    if (!continueOnError)
                        throw;
                }
            }
        }

        private static List<EventCollection> FindEvents()
        {
            List<EventCollection> eventCollections = new List<EventCollection>();
            Type eventType = typeof(IEvent);
            IEnumerable<Type> eventTypes = _assembly.GetTypes()
                                                     .Where(t => eventType.IsAssignableFrom(t) && !t.IsInterface);

            foreach (Type type in eventTypes)
            {
                Type eventHandlerType = typeof(IEventHandler<>);
                Type genericEventHandlerType = eventHandlerType.MakeGenericType(type);

                List<Type> handlers = _assembly.GetTypes()
                                               .Where(t => genericEventHandlerType.IsAssignableFrom(t))
                                               .ToList();

                eventCollections.Add(new EventCollection(type, handlers));
            }
            return eventCollections;
        }
        private object GetHandlerIstance(Type handlerType)
        {
            object handlerInstance;
            eventHandlerIstanceDictionary.TryGetValue(handlerType, out handlerInstance);

            if (handlerInstance == null)
            {
                handlerInstance = Activator.CreateInstance(handlerType);
                eventHandlerIstanceDictionary.Add(handlerType, handlerInstance);
            }

            return handlerInstance;
        }
    }
}
