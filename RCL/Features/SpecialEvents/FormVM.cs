namespace RCL.Features.SpecialEvents;

/// <summary>
/// Display model for a special event card (PWA public view / Admin PWA preview).
/// </summary>
public class FormVM
{
	public int Id { get; set; }
	public DateTime? ShowBeginDate { get; set; }
	public DateTime? ShowEndDate { get; set; }
	public DateTime EventDate { get; set; }
	public int EventTypeId { get; set; }
	/// <summary>From EventType.Descr (API join or Admin SmartEnum map).</summary>
	public string? EventTypeDescr { get; set; }
	public string? Title { get; set; }
	public string? SubTitle { get; set; }
	public string? ImageUrl { get; set; }
	public string? YouTubeId { get; set; }
	public string? WebsiteUrl { get; set; }
	public string? WebsiteDescr { get; set; }
	public string? Description { get; set; }
}
