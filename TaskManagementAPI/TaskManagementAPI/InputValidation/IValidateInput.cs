using TaskManagementAPI.ApplicationDTOs.AuthControllerDTOs;
using TaskManagementDataAccessLayer.BaseModels;

namespace TaskManagementAPI.InputValidation;

public interface IValidateInput
{
    public bool SignInDataValidator(SignInDataDTO signInDataDTO);
    public bool SignUpDataValidator(SignUpDataDTO signUpDataDTO);
    public bool ProfileDataValidator(Profile profile);
    public bool TeamDataValidator(TeamBaseModel teamDataDTO);
    public bool TeamMemberDataValidator(TeamMemberBaseModel teamMemberBaseModel);
    public bool InvitationDataValidator(InvitationBaseModel invitationBaseModel);
    public bool ProjectDataValidator(ProjectBaseModel projectBaseModel);
    public bool ProjectMemberDataValidator(ProjectMemberBaseModel projectMemberBaseModel);
    public bool GoalDataValidator(GoalBaseModel goalBaseModel);
    public bool TaskDataValidor(TaskBaseModel taskBaseModel);
    public bool TaskCategoryDataValidor(TaskCategoryBaseModel taskCategoryBaseModel);
    public bool EmailValidator(string Email);
    public bool PhoneValidator(string Phone);
    public bool StringWithOnlyLettersValidator(string text);
    public bool PasswordValidator(string password);
}
