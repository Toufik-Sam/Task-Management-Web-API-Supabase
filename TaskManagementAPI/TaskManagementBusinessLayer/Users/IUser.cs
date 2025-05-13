using TaskManagementDataAccessLayer.UserData;

namespace TaskManagementBusinessLayer.Users;

public interface IUser
{
    Task<UserDTO> Find();
    Task<UserDTO> Find(string Email);
    Task<UserDTO> UpdateUserProfileInfo(UserDTO user);
    Task<bool> DeactivateUserProfile();
    Task<bool> DeleteUserAndProfile();
    Task<bool> DoesUserProfileExist();
}
