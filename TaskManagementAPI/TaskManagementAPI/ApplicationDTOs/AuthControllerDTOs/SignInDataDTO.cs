namespace TaskManagementAPI.ApplicationDTOs.AuthControllerDTOs
{
    public class SignInDataDTO
    {
        public string Email { set; get; }
        public string Password { set; get; }
        public SignInDataDTO(string Email,string Password)
        {
            this.Email = Email;
            this.Password = Password;
        }
    }
}
