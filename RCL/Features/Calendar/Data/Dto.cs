using Microsoft.AspNetCore.Components;

namespace RCL.Features.Calendar.Data;

public record Dto(DateOnly Date, Enums.DateType DateType, MarkupString? Description);

// Ignore Spelling: Dto