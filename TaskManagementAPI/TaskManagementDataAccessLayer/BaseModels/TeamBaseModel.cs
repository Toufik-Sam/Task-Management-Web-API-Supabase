namespace TaskManagementDataAccessLayer.BaseModels
{
    public class TeamBaseModel
    {
        public int team_id { set; get; }
        public int created_by_profile_id { set; get; }
        public string name { set; get; }
        public TeamBaseModel(int team_id,int created_by_profile_id,string name)
        {
            this.team_id = team_id;
            this.created_by_profile_id = created_by_profile_id;
            this.name = name;
        }

    }
}
