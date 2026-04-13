using RCL.Features.Calendar.Enums;

namespace PWA.Components.Omer.Data;

public interface IService
{
	int GetOmerCount(DateTime date);
}

public class Service : IService
{
	public int GetOmerCount(DateTime date)
	{
		int OmerCount = 0;
		EventFeast? eventFeast = EventFeast.List.Where(ef => ef.Value == EventFeast.Omer.Value).FirstOrDefault();
		if (eventFeast is not null)
		{
			var omerRange = eventFeast.FeastDateRange;
			DateOnly today = DateOnly.FromDateTime(date); 
			if (today >= omerRange.Min && today <= omerRange.Max)
			{
				OmerCount = today.DayNumber - omerRange.Min.DayNumber + 1;
			}
		}
		return OmerCount;
	}
}
