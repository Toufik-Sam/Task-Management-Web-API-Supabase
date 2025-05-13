namespace TaskManagementDataAccessLayer.UserData;

public interface IUserData
{
    Task<UserDTO> GetMyProfileInfo();
    Task<UserDTO> GetMyProfileInfo(string Email);
    Task<bool> UpdateUserInfo(UserDTO user);
    Task<bool> DeactivateUserProfile();
    Task<bool> DeleteUserAndProfile();
    Task<bool> DoesUserProfileExist();
}
