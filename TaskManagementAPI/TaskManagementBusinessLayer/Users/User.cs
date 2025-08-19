using TaskManagementDataAccessLayer.UserData;

namespace TaskManagementBusinessLayer.Users;

public class User : IUser
{
    private readonly IUserData _userData;

    public User(IUserData userData)
    {
        this._userData = userData;
    }

    public async Task<bool> AddNewProfile(UserDTO newUserProfile,string AccessToken)
    {
        return await _userData.AddNewProfile(newUserProfile,AccessToken);
    }

    public async Task<bool> DeactivateUserProfile()
    {
        return await _userData.DeactivateUserProfile();
    }

    public async Task<bool> DeleteUserAndProfile()
    {
        return await _userData.DeleteUserAndProfile();
    }

    public async Task<bool> DoesUserProfileExist(string AccessToken)
    {
        return await _userData.DoesUserProfileExist(AccessToken);
    }
    public async Task<UserDTO> Find()
    {
        return await _userData.GetMyProfileInfo();
    }
    public async Task<UserDTO> Find(string Email)
    {
        return await _userData.GetMyProfileInfo(Email);
    }
    public async Task<bool> UpdateUserProfileInfo(UserDTO user)
    {
        return await _userData.UpdateUserInfo(user);
    }
}
