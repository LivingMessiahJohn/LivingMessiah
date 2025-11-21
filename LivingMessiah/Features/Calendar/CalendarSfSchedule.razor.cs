//using Blazored.Toast.Services;

using System;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Schedule;

using static LivingMessiah.Features.Calendar.ScheduleData;

namespace LivingMessiah.Features.Calendar;


public partial class CalendarSfSchedule
{
	[Inject] public ILogger<CalendarSfSchedule>? Logger { get; set; }
	[Inject] public IService? Service { get; set; } // Can this be gotten in RCL?
	[Inject] public IToastService? Toast { get; set; }

	[Parameter] public bool IsXsOrSm { get; set; }
	[Parameter] public int YearId { get; set; }

	protected PrintedCalendarEnum printedCalendarEnum = PrintedCalendarEnum.NotAvailable;
	protected SfSchedule<ReadonlyEventsData>? ScheduleRef;
	public DateTime CurrentDate = DateTime.Now;
	protected List<CalendarQuery>? CalendarQueries;
	public View ViewNow = View.Month;
	public List<ReadonlyEventsData>? AppointmentDataList { get; set; }


	List<DropDownData> ViewData = new List<DropDownData>() {
				new DropDownData { Name = "Week", Value = View.Week },
				new DropDownData { Name = "Month", Value = View.Month },
				new DropDownData { Name = "Year", Value = View.Year }
		};

	public class DropDownData
	{
		public string? Name { get; set; }
		public View? Value { get; set; }
	}

	protected override void OnInitialized()
	{
		Logger!.LogDebug("{Method}, YearId: {YearId}", nameof(OnInitialized), YearId);

		try
		{
			AppointmentDataList = new List<ReadonlyEventsData>();
			AppointmentDataList = Service!.GetData();
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "{Method}", nameof(OnInitialized));
			Toast!.ShowError("An invalid operation occurred, contact your administrator");
		}

	}

	public void OnEventRendered(EventRenderedArgs<ReadonlyEventsData> args)
	{
		args.Attributes = ApplyCategoryColor(
		args.Data.CategoryColor!, args.Attributes, ViewNow);
	}

	#region Header
	private DateTime SystemTime { get; set; } = DateTime.UtcNow.AddHours(+2);

	public async void OnPrintClick()
	{
		await ScheduleRef!.PrintAsync();
	}

	public async void OnExportClick(Syncfusion.Blazor.SplitButtons.MenuEventArgs args)
	{
		if (args.Item.Text == "Excel")
		{
			List<ReadonlyEventsData> ExportDatas = new List<ReadonlyEventsData>();
			List<ReadonlyEventsData> EventCollection = await ScheduleRef!.GetEventsAsync();
			List<ReadonlyEventsData> datas = EventCollection.ToList();
			foreach (ReadonlyEventsData data in datas)
			{
				ExportDatas.Add(data);
			}
			ExportOptions Options = new ExportOptions()
			{
				ExportType = ExcelFormat.Xlsx,
				CustomData = ExportDatas,
				Fields = new string[] { "Id", "Subject", "StartTime", "EndTime" }
			};
			await ScheduleRef.ExportToExcelAsync(Options);
		}
		else
		{
			await ScheduleRef!.ExportToICalendarAsync();
		}
	}
	#endregion
}

