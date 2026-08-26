namespace Admin.Features.Users.Data;

public interface IAuth0UserDirectory
{
	Task<IReadOnlyList<UserListItem>> GetUsersAsync(CancellationToken cancellationToken = default);
}
