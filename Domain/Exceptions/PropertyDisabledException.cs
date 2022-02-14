namespace Domain.Exceptions
{
    public class PropertyDisabledException : Exception
    {
        public PropertyDisabledException()
            : base("Property disabled by date or by status")
        {

        }
    }
}
