using Microsoft.AspNetCore.Components;
using LivingMessiah.Features.Sukkot.RegistrationSteps.Enums;

namespace LivingMessiah.Features.Sukkot.RegistrationSteps;

public class MarkupLiterals
{
	public static MarkupString Col_2nd_CheckIcon(Status usersCurrentStatus, Status comparisonStatus, bool isXs)
	{
		if (usersCurrentStatus.DisplayAsCompleted(comparisonStatus))
		{
			return isXs ?
				(MarkupString)$"<p class='text-center'><i class='{comparisonStatus.Icon} fa-2x'></i></p>" :
				(MarkupString)$"<i class='{comparisonStatus.Icon} fa-3x'></i>";
		}
		else
		{
			return (MarkupString)(string.Empty);
		}
	}

	public static MarkupString Col_3rd_Heading(Status comparisonStatus, string appendix)
	{
		return (MarkupString)$"<h4>Task - {comparisonStatus.Heading} {appendix}</h4>";
	}

	public static MarkupString Col_3rd_SubHeading(Status usersCurrentStatus, Status comparisonStatus)
	{
		if (usersCurrentStatus.DisplayAsCompleted(comparisonStatus))
		{
			return (MarkupString)"<p class='lead'><b>Task completed</b></p>";
		}
		else
		{
			return (MarkupString)(string.Empty);
		}
	}

}
