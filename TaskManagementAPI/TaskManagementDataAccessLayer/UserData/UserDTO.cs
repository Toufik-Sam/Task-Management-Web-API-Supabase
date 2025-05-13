namespace TaskManagementDataAccessLayer.UserData;

public class UserDTO
{
    public int UserProfileID { set; get; }
    public Guid UserID { set; get; }
    public string FirstName { set; get; }
    public string LastName { set; get; }
    public string Email { set; get; }
 
    public bool IsActive { set; get; }
    public UserDTO(int UserProfileID,Guid UserID,string FirstName, string LastName, string Email,  bool IsActive)
    {
        this.UserProfileID = UserProfileID;
        this.UserID = UserID;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Email = Email;
        this.IsActive = IsActive;
    }
    public UserDTO(UserDTO user)
    {
        this.UserProfileID = user.UserProfileID;
        this.UserID = UserID;
        this.FirstName = user.FirstName;
        this.LastName = user.LastName;
        this.Email = user.Email;
        this.IsActive = user.IsActive;
    }
}
