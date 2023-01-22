namespace Workouts.API.Results.Response
{
    public class Result
    {
        public Result()
        {

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
        BussinessException,
        ValidationException,
        Info,
        Warning,
        InternalServerError
        //.......
    }
}
