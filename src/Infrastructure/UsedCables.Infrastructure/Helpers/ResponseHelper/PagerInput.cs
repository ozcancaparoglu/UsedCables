namespace UsedCables.Infrastructure.Helpers.ResponseHelper
{
    public class PagerInput
    {
        public int Take { get; private set; }
        public int Skip { get; private set; }

        public PagerInput(int take = 100, int skip = 0)
        {
            Take = take;
            Skip = skip;
        }
    }
}