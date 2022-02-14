namespace Application.Exceptions
{
    public class OtherActivityAtTheSameTimeException : Exception
    {
        public OtherActivityAtTheSameTimeException()
            : base("There is another activity at the same time for the property")
        {

        }
    }
}
