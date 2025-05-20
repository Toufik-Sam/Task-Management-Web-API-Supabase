namespace TaskManagementDataAccessLayer.TeamData
{
    public class TeamDTO
    {
        public int TeamID { set; get; }
        public int CreatedByProfileID{set;get;}
        public string Name { set; get; }
        public TeamDTO(int TeamID,int CreatedByProfileID,string Name)
        {
            this.TeamID = TeamID;
            this.CreatedByProfileID = CreatedByProfileID;
            this.Name = Name;
        }
    }
}
