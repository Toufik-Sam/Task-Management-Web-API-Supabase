namespace TaskManagementAPI.Middlewares.CustomExceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string Message):base(Message){}
    }
}
