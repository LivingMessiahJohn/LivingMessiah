namespace LivingMessiahSukkot.Data;

public static class Helper
{

	public static string? Scrub(string? notes)
	{
		if (!string.IsNullOrEmpty(notes))
		{
			return notes.Replace("\"", string.Empty).Replace("'", string.Empty);
		}
		else
		{
			return string.Empty;
		}
	}
}
