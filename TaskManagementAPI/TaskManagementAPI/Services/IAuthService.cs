using Supabase.Gotrue;
using TaskManagementAPI.Services.AuthModels;

namespace TaskManagementAPI.Services;

public interface IAuthService
{
    Task<AuthSignInDTO> SignInAsync(string Email, string Password);
    Task<AuthSignUpDTO> SignUpAsync(string Email, string Password, Dictionary<string, object> MetaData);
    Task<bool>Signout();
    Task<object> RefreshToken(string accessToken, string refreshToken);
}
