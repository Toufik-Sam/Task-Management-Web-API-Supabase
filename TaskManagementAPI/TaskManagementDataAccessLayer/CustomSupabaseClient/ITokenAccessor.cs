namespace TaskManagementDataAccessLayer.CustomSupabaseClient;

public interface ITokenAccessor
{
    public string? Token { get; set; }
}
