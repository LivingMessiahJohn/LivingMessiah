namespace Api.Models;

/// <summary>
/// Special event row returned to the PWA when "now" is within ShowBeginDate..ShowEndDate.
/// Property-based class so Dapper can materialize from dbo.vwSpecialEvent.
/// </summary>
public class SpecialEventDto
{
	public int Id { get; set; }
	public DateTime EventDate { get; set; }
	public DateTime ShowBeginDate { get; set; }
	public DateTime ShowEndDate { get; set; }
	public int EventTypeId { get; set; }
	public string? EventTypeDescr { get; set; }
	public string? Title { get; set; }
	public string? SubTitle { get; set; }
	public string? ImageUrl { get; set; }
	public string? YouTubeId { get; set; }
	public string? WebsiteUrl { get; set; }
	public string? WebsiteDescr { get; set; }
	public string? Description { get; set; }
}

public class SpecialEventsResponse
{
	public List<SpecialEventDto> Events { get; set; } = new();
	public string Message { get; set; } = string.Empty;
	public string? ErrorDetails { get; set; }
	public bool IsTransient { get; set; }
}
