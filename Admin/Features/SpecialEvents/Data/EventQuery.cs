


using Microsoft.AspNetCore.Components;
using Admin.Features.SpecialEvents.Enums;

namespace Admin.Features.SpecialEvents.Data;
 
public class EventQuery 
{
	public int Id { get; set; } 
	public DateTime EventDate { get; set; }

	public int EventTypeId { get; set; }
	public string EventTypeDescr
	{
		get { return EventType.FromValue(EventTypeId).Descr; }
	}

	public int DaysDiff { get; set; }
	public string? DaysDiffDescr { get; set; }
	public string? Title { get; set; }
	public string? SubTitle { get; set; }
	public DateTime ShowBeginDate { get; set; }
	public DateTime ShowEndDate { get; set; }
	public string? ImageUrl { get; set; }
	public string? YouTubeId { get; set; }
	public string? WebsiteUrl { get; set; }
	public string? WebsiteDescr { get; set; }
	public string? Description { get; set; } 

	public MarkupString DaysAheadMU
	{
		get
		{
			if (DaysDiffDescr != null)
			{
				if (DaysDiffDescr == "Days Ahead")
				{
					return (MarkupString)$"<span class='text-success'>{DaysDiff}</span> <i class='fas fa-angle-right'></i>";
				}
				else
				{
					return (MarkupString)$"<span class='text-danger'>{DaysDiff}</span> <i class='fas fa-angle-left'></i>"; // 
				}
			}
			else
			{
				return (MarkupString)"?";
			}
		}
	}

	public MarkupString DaysAheadXmSmMU
	{
		get
		{
			if (DaysDiffDescr != null)
			{
				if (DaysDiffDescr == "Days Ahead")
				{
					return (MarkupString)$"<span class='text-success float-end'><i class='fas fa-angle-right'></i> <b>{DaysDiff}</b></span>";
				}
				else
				{
					return (MarkupString)$"<span class='text-danger float-end'><b>{DaysDiff}</b> <i class='fas fa-angle-left'></i></span>"; // 
				}
			}
			else
			{
				return (MarkupString)"?";
			}
		}
	}

	public override string ToString()
	{
		return $"EventDate: {EventDate}, EventTypeId: {EventTypeId}";
	}

	public string EventDay()
	{
		return EventDate.Day.ToString();
	}

	public string EventYear()
	{
		return EventDate.Year.ToString();
	}

	public string EventMonth()
	{
		return EventDate.ToString("MMMM");
	}

}
// Ignore Spelling: Diff Descr
// Ignore Spelling: vw