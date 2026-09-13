namespace RCL.Features.Sukkot.Data.DailySchedule;

/*
 ToDo: `GetAsync` is deprecated so ScheduleQuery is deprecated
*/
public class ScheduleQuery
{
	public string Markdown { get; set; } = string.Empty;


  /*
   ToDo: The logic of how this is done needs to be figured out
   This is because getting the large single markdown is being deprecated
  */

  public DateTime LastRevised { get; set; }
}
