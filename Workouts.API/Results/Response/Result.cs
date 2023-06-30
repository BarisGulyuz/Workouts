namespace Workouts.API.Results.Response
{
    public class Result
    {
        public Result()
        {
            ResultType = ResultType.Info;
        }
        public Result(ResultType resultType, string message)
        {
            ResultType = resultType;
            Message = message;
        }

        public ResultType ResultType { get; set; }
        public string Message { get; set; }

    }

    public enum ResultType
    {
        Success,
        Info,
        Warning,
        Error,
        ValidationError,
        InternalServerError,
    }
}
