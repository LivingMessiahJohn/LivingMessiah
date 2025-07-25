﻿
namespace LivingMessiah.Features.Calendar;

public class CalendarQuery
{
	public DateTime Date { get; set; }
	public int Detail { get; set; }
	public int DateTypeId { get; set; }
	public int EnumId { get; set; }
	public string? Description { get; set; }

	public override string ToString()
	{
		return $" Date: {Date:yyyy-MM-dd}, Detail: {Detail}, DateTypeId: {DateTypeId}, EnumId: {EnumId}, Description: {Description ?? "null"}";
	}
}
