using System.Text.Json;

namespace Workouts.API.Results.Response
{
    public class Response<T>
    {
        public Response()
        {
            Results = new List<Result>();
        }

        public T Value { get; set; }
        public bool IsSuccess
        {
            get
            {
                return !Results.Any(r => r.ResultType == ResultType.BussinessException
                                      || r.ResultType == ResultType.ValidationException);
            }
        }
        public List<Result> Results { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public void AddError(ResultType resultType, List<string> messages)
        {
            foreach (string message in messages)
                Results.Add(new Result { ResultType = resultType, Message = message });
        }
    }
}
