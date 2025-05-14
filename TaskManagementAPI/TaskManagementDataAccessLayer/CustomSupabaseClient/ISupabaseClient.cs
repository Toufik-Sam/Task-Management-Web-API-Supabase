
namespace TaskManagementDataAccessLayer.CustomSupabaseClient
{
    public interface ISupabaseClient
    {
         Task<string> Rpc(string FunctionName, object parameters,string OverrideToken="");
    }
}
