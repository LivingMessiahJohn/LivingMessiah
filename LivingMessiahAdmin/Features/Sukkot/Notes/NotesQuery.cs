using LivingMessiahAdmin.Helpers;

namespace LivingMessiahAdmin.Features.Sukkot.Notes;

public class NotesQuery
{
	public int Id { get; set; }
	public string? FirstName { get; set; }
	public string? FamilyName { get; set; }
	public string? Phone { get; set; }
	public string? EMail { get; set; }
	public string? AdminNotes { get; set; }
	public string? UserNotes { get; set; }

	public bool HasAdminNotes => !string.IsNullOrEmpty(AdminNotes);
	public bool HasUserNotes => !string.IsNullOrEmpty(UserNotes);
	public string PhoneNumber => (Phone ?? "").PhoneNumber();
}
