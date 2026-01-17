using Microsoft.AspNetCore.Components;

namespace RCL.Features.Calendar.Data;

public record DayCellRecord(DateOnly Date
  , Enums.DateType DateType
  , bool IsErev
  , MarkupString? Description
  , string CssClass); // ("erev" or "daytime" or "center")

// ToDo: consider changing bool IsErev to string cssClass ("erev" or "daytime")

// Ignore Spelling: Dto erev