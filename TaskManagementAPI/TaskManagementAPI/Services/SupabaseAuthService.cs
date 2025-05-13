using Supabase;
using TaskManagementAPI.Services.AuthModels;
namespace TaskManagementAPI.Services
{
    public class SupabaseAuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly Supabase.Client _supabase;
        public SupabaseAuthService(Supabase.Client supabase,IConfiguration config)
        {
            this._config = config;
            this._supabase = supabase;
        }

        public async Task<object> RefreshToken(string accessToken,string refreshToken)
        {
            if (await _supabase.Auth.SetSession(accessToken, refreshToken) != null && await _supabase.InitializeAsync() != null)
            {
                await _supabase.Auth.RefreshToken();
                return new
                {
                    newAccessToken = _supabase.Auth.CurrentSession!.AccessToken,
                    newRefreshToken = _supabase.Auth.CurrentSession.RefreshToken
                };
            }
            return new { };
        }

        public async Task<AuthSignInDTO>SignInAsync(string Email, string Password)
        {
            var result = await _supabase.Auth.SignIn(Email, Password);
            if (result == null)
                return null;
            DateTime ExpiresAt=result.ExpiresAt();
            return new AuthSignInDTO(result.AccessToken,result.TokenType,result.ExpiresIn,ExpiresAt, result.RefreshToken,result.User);
        }

        public async Task<bool> Signout()
        {
            try
            {
                await _supabase.Auth.SignOut();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<AuthSignUpDTO>SignUpAsync(string Email, string Password, Dictionary<string, object> UserData)
        {
            var options = new Supabase.Gotrue.SignUpOptions { Data = UserData };
            var result = await _supabase.Auth.SignUp(Email, Password, options);
            if (result.User == null)
                return null;
            return new AuthSignUpDTO(result.User.Id,result.User.Email,result.User.Aud,result.User.CreatedAt, 
                result.User.ConfirmationSentAt, result.User.UserMetadata);
        }
    }

}
