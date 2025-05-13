using System.Text.Json;
using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.CustomSupabaseClient;
namespace TaskManagementDataAccessLayer.UserData;
public  class UserData:IUserData
{
    private readonly ISupabaseClient _supabase;

    public UserData(ISupabaseClient supabase)
    {
        this._supabase = supabase;
    }
    
    public async Task<bool> DeactivateUserProfile()
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_deactivate_user_profile", new {}));
    }
    public async Task<bool> DeleteUserAndProfile()
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_delete_user_profile", new {}));
    }
    public async Task<bool> DoesUserProfileExist()
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_does_user_profile_exist", new {}));
    }
    public async Task<UserDTO> GetMyProfileInfo(string Email)
    {
        var Profiles = JsonSerializer.Deserialize<List<Profile>>(await _supabase.Rpc("sp_get_user_profile_by_email", new { p_email = Email }));
        if (Profiles.Count != 0)
            return new UserDTO(Profiles.First().profile_id,
                               Guid.Empty,
                               null,
                               null,
                               Profiles.First().email,
                               Profiles.First().is_active);
        return null!;
    }
    public async Task<UserDTO> GetMyProfileInfo()
    {
        var Profiles = JsonSerializer.Deserialize<List<Profile>>(await _supabase.Rpc("sp_get_current_user_profile", new { }));
        if (Profiles.Count != 0)
            return new UserDTO(Profiles.First().profile_id,
                               Profiles.First().user_id,
                               Profiles.First().first_name,
                               Profiles.First().last_name,
                               Profiles.First().email,
                               Profiles.First().is_active);
        return null!;
    }
    public async Task<bool> UpdateUserInfo(UserDTO user)
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_update_user_profile",
            new {  p_first_name = user.FirstName, p_last_name = user.LastName }));
    }
}
