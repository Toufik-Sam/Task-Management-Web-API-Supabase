namespace TaskManagementDataAccessLayer.UserData;

public interface IUserData
{
    Task<bool> AddNewProfile(UserDTO newUserProfile,string AccessToken);
    Task<UserDTO> GetMyProfileInfo();
    Task<UserDTO> GetMyProfileInfo(string Email);
    Task<bool> UpdateUserInfo(UserDTO user);
    Task<bool> DeactivateUserProfile();
    Task<bool> DeleteUserAndProfile();
    Task<bool> DoesUserProfileExist(string AccessToken);
}
