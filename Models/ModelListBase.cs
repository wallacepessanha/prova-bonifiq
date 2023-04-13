namespace ProvaPub.Models
{
    public abstract class ModelListBase
    {
        public ModelListBase()
        {
            TotalCount = 10;
            HasNext = false;
        }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
    }
}
