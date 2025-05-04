namespace TaskManagementAPI.Services.AuthModels;

public class AuthSignUpDTO
{
    public string Id { set; get; }
    public string Email { set; get; }
    public string Aud { set; get; }
    public DateTime CreatedAt { set; get; }
    public DateTime?ConfirmationSentAt { set; get; }
    public Dictionary<string, object> UserMetaData { set; get; }
    public AuthSignUpDTO(string Id,string Email,string Aud,DateTime CreatedAt,DateTime?ConfirmationSentAt,Dictionary<string,object>UserMetaData)
    {
        this.Id = Id;
        this.Email = Email;
        this.Aud = Aud;
        this.CreatedAt = CreatedAt;
        this.ConfirmationSentAt = ConfirmationSentAt;
        this.UserMetaData = UserMetaData;
    }
}
