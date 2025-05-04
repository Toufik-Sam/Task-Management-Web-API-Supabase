using Supabase;
using TaskManagementAPI.Services.AuthModels;
namespace TaskManagementAPI.Services
{
    public class SupabaseAuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly Client _supabase;
        public SupabaseAuthService(IConfiguration config)
        {
            this._config = config;
            this._supabase= new Client(_config["Supabase:URL"]!, _config["Supabase:AnonKey"],
           new SupabaseOptions { AutoConnectRealtime = true });
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
