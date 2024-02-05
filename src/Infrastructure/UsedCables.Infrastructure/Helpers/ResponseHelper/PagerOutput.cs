namespace UsedCables.Infrastructure.Helpers.ResponseHelper
{
    public class PagerOutput<T>
    {
        private readonly IEnumerable<T> Entities;
        private readonly int Total;

        public PagerOutput(IEnumerable<T> entities, int total)
        {
            Entities = entities;
            Total = total;
        }
    }
}