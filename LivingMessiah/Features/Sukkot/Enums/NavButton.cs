﻿using Ardalis.SmartEnum;
using LivingMessiah.Features.Sukkot.Constants;

namespace LivingMessiah.Features.Sukkot.Enums;

public abstract class NavButton : SmartEnum<NavButton>
{
	#region Id's
	private static class Id
	{
		internal const int Details = 1;
		internal const int DetailsPrint = 2;
		internal const int DeleteConfirmation = 3;
		internal const int Donation = 4;
		internal const int RegistrationSteps = 5;
		internal const int RegistrationStepsBack = 6;
		internal const int Add = 7;

	}
	#endregion

	private NavButton(string name, int value) : base(name, value) { } // Constructor

	#region  Declared Public Instances
	public static readonly NavButton Details = new DetailsSE();
	public static readonly NavButton DetailsPrint = new DetailsPrintSE();
	public static readonly NavButton DeleteConfirmation = new DeleteConfirmationSE();
	public static readonly NavButton Donation = new DonationSE();
	public static readonly NavButton RegistrationSteps = new RegistrationStepsSE();
	public static readonly NavButton RegistrationStepsBack = new RegistrationStepsBackSE();
	// SE=SmartEnum
	#endregion

	#region Extra Fields
	public abstract string Label { get; }
	public abstract string Icon { get; }
	public abstract string Title { get; }
	public abstract string Css { get; }
	public abstract string Route { get; }
	public abstract string RouteSuffix { get; }
	#endregion

	#region Private Instantiation

	private sealed class DetailsSE : NavButton
	{
		public DetailsSE() : base($"{nameof(Id.Details)}", Id.Details) { }
		public override string Label => "View";
		public override string Title => "Registration View";
		public override string Icon => "fas fa-info";
		public override string Css => "btn btn-outline-info";
		public override string Route => Pages.Details.Index; // Link.Details.Index;
		public override string RouteSuffix => "/false";
	}

	private sealed class DetailsPrintSE: NavButton
	{
		public DetailsPrintSE() : base($"{nameof(Id.DetailsPrint)}", Id.DetailsPrint) { }
		public override string Label => "Print";
		public override string Title => "Registration Print";
		public override string Icon => "fas fa-print";
		public override string Css => "btn btn-outline-info";
		public override string Route => Pages.Details.Index; // Link.Details.Index;
		public override string RouteSuffix => "/true";
	}

	private sealed class DeleteConfirmationSE : NavButton
	{
		public DeleteConfirmationSE() : base($"{nameof(Id.DeleteConfirmation)}", Id.DeleteConfirmation) { }
		public override string Label => "Delete";
		public override string Title => "Delete";
		public override string Icon => "fas fa-times";
		public override string Css => "btn btn-outline-danger";
		public override string Route => Pages.DeleteConfirmation.Index; // Link.DeleteConfirmation;
		public override string RouteSuffix => "";
	}

	private sealed class DonationSE : NavButton
	{
		public DonationSE() : base($"{nameof(Id.Donation)}", Id.Donation) { }
		public override string Label => "Donation";
		public override string Title => Pages.Payment.Title; // Link.Stripe.Payment.Title;   
		public override string Icon => Pages.Payment.Icon; // Links.Donate.Icon; 
		public override string Css => "btn btn-warning";  // btn-sm
		public override string Route => Pages.Payment.Index; // Link.Stripe.Payment.Index;  
		public override string RouteSuffix => "";
	}

	//Todo: Incorporate these two or delete them
	private sealed class RegistrationStepsSE : NavButton
	{
		public RegistrationStepsSE() : base($"{nameof(Id.RegistrationSteps)}", Id.RegistrationSteps) { }
		public override string Label => Breadcrumbs.Start.Text; // Link.RegistrationSteps.StartButtonText;
		public override string Title => Breadcrumbs.Start.Text; // Link.RegistrationSteps.StartButtonText;
		public override string Icon => Breadcrumbs.Start.Icon; // Link.RegistrationSteps.StartButtonIcon;
		public override string Css => "btn btn-success btn-lg";  
		public override string Route => Pages.RegistrationSteps.Index; // Link.RegistrationSteps.Index;
		public override string RouteSuffix => "";
	}

	private sealed class RegistrationStepsBackSE : NavButton
	{
		public RegistrationStepsBackSE() : base($"{nameof(Id.RegistrationSteps)}", Id.RegistrationSteps) { }
		public override string Label => Breadcrumbs.BackTo.Text; // Link.RegistrationSteps.BackToButtonText;
		public override string Title => Breadcrumbs.BackTo.Text; // Link.RegistrationSteps.BackToButtonText;
		public override string Icon => Pages.RegistrationSteps.Icon; // Link.RegistrationSteps.BackToButtonIcon;
		public override string Css => "btn btn-success btn-lg";  
		public override string Route => Pages.RegistrationSteps.Index; // Link.RegistrationSteps.Index;
		public override string RouteSuffix => "";
	}

	#endregion

}
