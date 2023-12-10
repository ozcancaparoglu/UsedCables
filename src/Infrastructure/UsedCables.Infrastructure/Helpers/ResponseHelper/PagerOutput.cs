namespace UsedCables.Infrastructure.Helpers.ResponseHelper
{
    public class PagerOutput<T>
    {
        private IEnumerable<T> entities;
        private int total;

        public PagerOutput(IEnumerable<T> entities, int total)
        {
            this.entities = entities;
            this.total = total;
        }
    }
}