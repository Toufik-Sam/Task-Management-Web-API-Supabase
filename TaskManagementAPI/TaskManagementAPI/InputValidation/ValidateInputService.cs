using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using TaskManagementAPI.ApplicationDTOs.AuthControllerDTOs;
using TaskManagementDataAccessLayer.BaseModels;

namespace TaskManagementAPI.InputValidation;

public class ValidateInputService : IValidateInput
{
    public bool EmailValidator(string Email)
    {
        return new EmailAddressAttribute().IsValid(Email);
    }

    public bool PasswordValidator(string password)
    {
        return (password.Length >= 8 && !StringWithOnlyLettersValidator(password) && !StringWithOnlyDigitsValidator(password));
    }

    public bool PhoneValidator(string Phone)
    {
        return (Regex.IsMatch(Phone, "^(?:\\+1)?\\s?\\(?\\d{3}\\)?[-.\\s]?\\d{3}[-.\\s]?\\d{4}$") && !string.IsNullOrEmpty(Phone));
    }

    public bool StringWithOnlyLettersValidator(string text)
    {
        return (text.All(c => Char.IsLetter(c) || c == ' ') && !string.IsNullOrEmpty(text));
    }

    public bool StringWithOnlyDigitsValidator(string text)
    {
        return (!string.IsNullOrEmpty(text) && text.All(char.IsDigit));
    }

    public bool GoalDataValidator(GoalBaseModel goalBaseModel)
    {
        return goalBaseModel.goal_id>=0 && 
               !string.IsNullOrEmpty(goalBaseModel.title) &&
               !string.IsNullOrEmpty(goalBaseModel.description) &&
               goalBaseModel.created_by_project_member_id>0 &&
               (goalBaseModel.status_id>0 && goalBaseModel.status_id<=8) &&
               (goalBaseModel.priority_id > 0 && goalBaseModel.priority_id <= 5);
    }

    public bool InvitationDataValidator(InvitationBaseModel invitationBaseModel)
    {
        return invitationBaseModel.invitation_id>=0 && 
               invitationBaseModel.sent_by>0 && 
               invitationBaseModel.received_by>0 && 
               invitationBaseModel.team_id>0 && 
               invitationBaseModel.role_id>0 && invitationBaseModel.role_id<=4;
    }

    public bool ProfileDataValidator(Profile profile)
    {
        return profile.profile_id >= 0 && 
               StringWithOnlyLettersValidator(profile.first_name) && 
               StringWithOnlyLettersValidator(profile.last_name) && 
               EmailValidator(profile.email);
    }

    public bool ProjectDataValidator(ProjectBaseModel projectBaseModel)
    {
        return projectBaseModel.owner_id > 0 &&
               !string.IsNullOrEmpty(projectBaseModel.title) &&
               !string.IsNullOrEmpty(projectBaseModel.title) &&
               projectBaseModel.status_id > 0 && projectBaseModel.priority_id > 0;
    }

    public bool ProjectMemberDataValidator(ProjectMemberBaseModel projectMemberBaseModel)
    {
        return projectMemberBaseModel.project_member_id >= 0 && projectMemberBaseModel.team_member_id > 0;
    }

    public bool SignInDataValidator(SignInDataDTO signInDataDTO)
    {
        return (EmailValidator(signInDataDTO.Email) && PasswordValidator(signInDataDTO.Password));
    }

    public bool SignUpDataValidator(SignUpDataDTO signUpDataDTO)
    {
        return ( StringWithOnlyLettersValidator(signUpDataDTO.FirstName)&&
                 StringWithOnlyLettersValidator(signUpDataDTO.LastName) &&
                 EmailValidator(signUpDataDTO.Email) &&
                 PhoneValidator(signUpDataDTO.Phone) &&
                 PasswordValidator(signUpDataDTO.Password));
    }

    public bool TaskCategoryDataValidor(TaskCategoryBaseModel taskCategoryBaseModel)
    {
        return taskCategoryBaseModel.task_category_id >= 0 &&
               !string.IsNullOrEmpty(taskCategoryBaseModel.title) &&
               !string.IsNullOrEmpty(taskCategoryBaseModel.description);
    }

    public bool TaskDataValidor(TaskBaseModel taskBaseModel)
    {
        return taskBaseModel.task_id >= 0 &&
               !string.IsNullOrEmpty(taskBaseModel.title) &&
               !string.IsNullOrEmpty(taskBaseModel.description) &&
               taskBaseModel.status_id > 0 && taskBaseModel.status_id <= 8 &&
               taskBaseModel.priority_id > 0 && taskBaseModel.priority_id <= 4 &&
               taskBaseModel.goal_id > 0 &&
               (taskBaseModel.parent_task_id == -1 || taskBaseModel.parent_task_id > 0) &&
               taskBaseModel.created_by_project_member_id > 0 &&
               taskBaseModel.assigned_to_project_member_id > 0 &&
               (taskBaseModel.task_category_id > 0 || taskBaseModel.task_category_id == null);
    }

    public bool TeamDataValidator(TeamBaseModel teamDataDTO)
    {
        return (teamDataDTO.team_id >= 0 && teamDataDTO.created_by_profile_id > 0 && StringWithOnlyLettersValidator(teamDataDTO.name));
    }

    public bool TeamMemberDataValidator(TeamMemberBaseModel teamMemberBaseModel)
    {
        return teamMemberBaseModel.team_member_id >= 0 &&
               StringWithOnlyLettersValidator(teamMemberBaseModel.first_name) &&
               StringWithOnlyLettersValidator(teamMemberBaseModel.last_name) &&
               EmailValidator(teamMemberBaseModel.email) &&
               teamMemberBaseModel.role_id > 0 && teamMemberBaseModel.role_id<=4;
    }

}
