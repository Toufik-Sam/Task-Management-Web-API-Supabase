using Supabase.Gotrue;

namespace TaskManagementAPI.Services.AuthModels;

public class AuthSignInDTO
{
    public string AccessToken { set; get; }
    public string Token_Type { set; get; }
    public long Expires_In { set; get; }
    public DateTime Expires_At { set; get; }
    public string Refresh_Token { set; get; }
    public User User { set; get; }
    public AuthSignInDTO(string AccessToken, string Token_Type,long Expires_In,DateTime Expires_At,string Refresh_Token,User User)
    {
        this.AccessToken = AccessToken;
        this.Token_Type = Token_Type;
        this.Expires_In = Expires_In;
        this.Expires_At = Expires_At;
        this.Refresh_Token = Refresh_Token;
        this.User = User;
    }
}
