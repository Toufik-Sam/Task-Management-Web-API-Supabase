namespace TaskManagementDataAccessLayer.Exceptions
{
    public class ForbiddenRequestException:Exception
    {
        public ForbiddenRequestException(string message):base(message)
        {
            
        }
    }
}
