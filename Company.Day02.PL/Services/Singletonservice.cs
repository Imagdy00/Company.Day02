namespace Company.Day02.PL.Services
{
    public class Singletonservice :ISingletonService
    {
        public Singletonservice()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set; }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
