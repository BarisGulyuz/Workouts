using System.Text.Json;

namespace Workouts.API.Results.Response
{
    public class Response<T> : Response
    {
        public T Value { get; set; }

        public static Response<T> CreateReponse(T value, string message, ResultType resultType)
        {
            Response<T> response = new Response<T>();

            response.Value = value;
            response.AddResult(resultType, message);

            return response;
        }
        public static Response<T> CreateReponse(T value, List<Result> results)
        {
            Response<T> response = new Response<T>();

            response.Value = value;
            response.Results.AddRange(results);

            return response;
        }
        public static Response<T> CreateSuccessResponse(T value, string message) => CreateReponse(value, message, ResultType.Success);
        public static Response<T> CreateErrorResponse(T value, string message) => CreateReponse(value, message, ResultType.Error);

    }

    public class Response
    {
        public Response()
        {
            Results = new List<Result>();
        }

        public Response(string message)
        {
            Results = new List<Result>();
            AddResult(ResultType.Success, message);
        }

        public Response(string message, ResultType resultType)
        {
            Results = new List<Result>();
            AddResult(resultType, message);
        }

        public bool IsSuccess
        {
            get
            {
                return !Results.Any(r => r.ResultType == ResultType.Error
                                      || r.ResultType == ResultType.InternalServerError
                                      || r.ResultType == ResultType.ValidationError);
            }
        }
        public List<Result> Results { get; set; }

        public void AddResult(ResultType resultType, string message)
        {
            Results.Add(new Result { ResultType = resultType, Message = message });
        }
        public void AddResult(ResultType resultType, List<string> messages)
        {
            foreach (string message in messages)
                AddResult(resultType, message);
        }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
