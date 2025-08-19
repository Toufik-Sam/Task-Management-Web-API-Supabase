using System.Text.Json;
using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.CustomSupabaseClient;

namespace TaskManagementDataAccessLayer.GoalData
{
    public class GoalData : IGoalData
    {
        private readonly ISupabaseClient _supabase;

        public GoalData(ISupabaseClient supabase)
        {
            this._supabase = supabase;
        }
        public async Task<int> AddNewGoal(GoalDTO goalDTO)
        {
            return JsonSerializer.Deserialize<int>(await _supabase.Rpc("sp_add_project_goal",
                           new
                           {
                               p_belong_to_project_id = goalDTO.BelongToProjectID,
                               p_title = goalDTO.Title,
                               p_description = goalDTO.Description,
                               p_planned_start_date = goalDTO.PlannedStartDate,
                               p_planned_end_date = goalDTO.PlannedEndDate,
                               p_actual_start_date = goalDTO.ActualStartDate,
                               p_actual_end_date = goalDTO.ActualEndDate,
                               p_status_id = (int)goalDTO.Status,
                               p_priority_id = (int)goalDTO.Priority,
                               p_created_by_project_member_id = goalDTO.CreatedByProjectMemberID,
                               p_created_at = goalDTO.CreatedAt
                           }));
        }
        public async Task<IEnumerable<GoalBaseModel>> GetGoalByID(int goalID)
        {
            return JsonSerializer.Deserialize<IEnumerable<GoalBaseModel>>
                (await _supabase.Rpc("sp_get_project_goal_by_id", new { p_goal_id = goalID }))!;
        }
        public async Task<bool> DeleteGoal(int goalID)
        {
            return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_delete_project_goal",new { p_goal_id = goalID }));
        }

        public async Task<IEnumerable<GoalBaseModel>> GetAllGoalsByProject(Guid projectID)
        {
            return JsonSerializer.Deserialize<IEnumerable<GoalBaseModel>>
                (await _supabase.Rpc("sp_get_project_goals", new { p_project_id=projectID }))!;
        }

        public async Task<bool> UpdateGoal(GoalDTO goalDTO)
        {
            return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_update_project_goal",
                                new
                                {
                                    p_goal_id=goalDTO.GoalID,
                                    p_belong_to_project_id=goalDTO.BelongToProjectID,
                                    p_title = goalDTO.Title,
                                    p_description = goalDTO.Description,
                                    p_planned_start_date = goalDTO.PlannedStartDate,
                                    p_planned_end_date = goalDTO.PlannedEndDate,
                                    p_actual_start_date = goalDTO.ActualStartDate,
                                    p_actual_end_date = goalDTO.ActualEndDate,
                                    p_status_id = (int)goalDTO.Status,
                                    p_priority_id = (int)goalDTO.Priority
                                }));
        }
    }
}
