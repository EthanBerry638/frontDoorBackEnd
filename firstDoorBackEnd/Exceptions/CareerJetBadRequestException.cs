namespace firstDoorBackEnd.Exceptions
{
    public class CareerJetBadRequestException : Exception
    {
        public CareerJetBadRequestException()
        {
        }

        public CareerJetBadRequestException(string message)
            : base(message)
        {
        }
    }
}
