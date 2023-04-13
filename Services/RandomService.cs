namespace ProvaPub.Services
{
    public class RandomService
    {
        int seed;
        public RandomService()
        {
            seed = Guid.NewGuid().GetHashCode() * 1;
        }
        public int GetRandom()
        {
            return new Random().Next(100, seed);
        }

    }
}
