namespace Workouts.API.Results.Response
{
    public class Result
    {
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
