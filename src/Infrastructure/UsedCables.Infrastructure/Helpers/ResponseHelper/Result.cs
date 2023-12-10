namespace UsedCables.Infrastructure.Helpers.ResponseHelper
{
    public class Result<T>
    {
        internal Result(bool succeeded, IEnumerable<string> errors, object data, PagerInput? pagerInput, int total)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            Data = (T)data;
            Total = total;
            NextPage = pagerInput?.NextPage();
        }

        public bool Succeeded { get; set; }

        public T Data { get; set; }

        public string[] Errors { get; set; }

        public PagerInput? NextPage { get; set; }

        public int Total { get; set; }

        public static async Task<Result<T>> SuccessAsync(object data = null, PagerInput pagerInput = null, int total = 0)
        {
            return await Task.Run(() =>
            {
                return new Result<T>(true, Array.Empty<string>(), (T)data, pagerInput, total);
            });
        }

        public static async Task<Result<T>> FailureAsync(IEnumerable<string> errors)
        {
            return await Task.Run(() =>
            {
                return new Result<T>(false, errors, null, null);
            });
        }

        public static async Task<Result<T>> FailureAsync(string error)
        {
            return await Task.Run(() =>
            {
                return new Result<T>(false, new List<string>() { error }, null, null);
            });
        }
    }
}