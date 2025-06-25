namespace LivingMessiah.Features.Sukkot.Enums;

public record DayRecord(int Number, int Permutation, string Title, int Week);
/*

## Replace 
- Number: 5, 6... 14 AttendanceDateRange.Start to AttendanceDateRange.Finish
- Permutation: 1, 2, 4, 8, 16, 32, 64, 128, 256, 512
- Title: e.g. Tue 10/14
- Week: is always 1 if there isn't a split month, otherwise it's 1 or 2
- ??? DateRangeType: Attendance or Lodging 

 EntryForm.razor	(Features\Sukkot\NormalUser\EntryForm.razor	163)
 <AttendanceDateList SelectedDates="@VM.AttendanceDateList" />	
 

 */
