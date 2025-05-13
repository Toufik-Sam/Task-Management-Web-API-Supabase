using TaskManagementDataAccessLayer.UserData;

namespace TaskManagementBusinessLayer.Users;

public class User : IUser
{
    private readonly IUserData _userData;

    public User(IUserData userData)
    {
        this._userData = userData;
    }
    public async Task<bool> DeactivateUserProfile()
    {
        return await _userData.DeactivateUserProfile();
    }

    public async Task<bool> DeleteUserAndProfile()
    {
        return await _userData.DeleteUserAndProfile();
    }

    public async Task<bool> DoesUserProfileExist()
    {
        return await _userData.DoesUserProfileExist();
    }
    public async Task<UserDTO> Find()
    {
        return await _userData.GetMyProfileInfo();
    }
    public async Task<UserDTO> Find(string Email)
    {
        return await _userData.GetMyProfileInfo(Email);
    }
    public async Task<UserDTO> UpdateUserProfileInfo(UserDTO user)
    {
        return await _userData.UpdateUserInfo(user) ? new UserDTO(user) : null!;
    }
}
