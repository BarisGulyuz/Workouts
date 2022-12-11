using Workouts.API.Results.Response;

namespace Workouts.API.Results.Exceptions
{
    public class BusinessException : Exception
    {

        public BusinessException(string message) : base(message)
        {
        }
        public BusinessException(params string[] messages)
        {
            foreach (string result in messages)
            {
                base.Data.Add(Guid.NewGuid(), result);
            }
        }
    }
}
