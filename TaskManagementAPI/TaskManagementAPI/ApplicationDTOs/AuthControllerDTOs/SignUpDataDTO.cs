namespace TaskManagementAPI.ApplicationDTOs.AuthControllerDTOs;

public class SignUpDataDTO
{
    public string FirstName { set; get; }
    public string LastName { set; get; }
    public string Email { set; get; }
    public string Phone { set; get; }
    public string Password { set; get; }
    public SignUpDataDTO(string FirstName, string LastName, string Email, string Phone, string Password)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Email = Email;
        this.Phone = Phone;
        this.Password = Password;
    }
}
