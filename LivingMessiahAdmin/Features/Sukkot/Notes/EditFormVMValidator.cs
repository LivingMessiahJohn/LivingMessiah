using FluentValidation;

namespace LivingMessiahAdmin.Features.Sukkot.Notes;

public class EditFormVMValidator : AbstractValidator<EditFormVM>
{
	public EditFormVMValidator()
	{
		RuleFor(p => p.Notes)
			.MaximumLength(800).WithMessage("Notes cannot be longer than 800 characters");
	}
}

