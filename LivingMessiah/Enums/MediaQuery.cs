﻿using Ardalis.SmartEnum;

namespace LivingMessiah.Enums;

public abstract class MediaQuery : SmartEnum<MediaQuery>
{
	#region Id's
	private static class Id
	{
		internal const int Xs = 1;
		internal const int Sm = 2;
		internal const int Md = 3;
		internal const int Lg = 5;
		internal const int Xl = 6;
		internal const int XsOrSm = 7;
		internal const int SmOrMdOrLgOrXl = 8;
		internal const int MdOrLgOrXl = 9;
		internal const int LgOrXl = 10;
		//internal const int XsOrXl = 11;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly MediaQuery Xs = new XsSE();
	public static readonly MediaQuery Sm = new SmSE();
	public static readonly MediaQuery Md = new MdSE();
	public static readonly MediaQuery Lg = new LgSE();
	public static readonly MediaQuery Xl = new XlSE();

	// Combination
	public static readonly MediaQuery XsOrSm = new XsOrSmSE();
	public static readonly MediaQuery SmOrMdOrLgOrXl = new SmOrMdOrLgOrXlSE();
	public static readonly MediaQuery MdOrLgOrXl = new MdOrLgOrXlSE();
	public static readonly MediaQuery LgOrXl = new LgOrXlSE();
	//public static readonly MediaQuery XsOrXl = new XsOrXlSE(); 

	// SE=SmartEnum
	#endregion

	private MediaQuery(string name, int value) : base(name, value) { }  // Constructor

	#region Extra Fields
	public abstract string DivClass { get; }
	/*
	public abstract string Breakpoint { get; }
	public abstract string ClassInfix { get; }
	public abstract int Dimensions { get; }
	
	Breakpoint	ClassInfix	Dimensions
		X-Small			None				<576px
		Small				sm					≥576px
		Medium			md					≥768px
		Large				lg					≥992px
		Extra	large	xl					≥1200px
		2xExtra lg	xxl					≥1400px  v5
	*/
	#endregion


	#region Private Instantiation

	private sealed class XsSE : MediaQuery
	{
		public XsSE() : base($"{nameof(Id.Xs)}", Id.Xs) { }
		public override string DivClass => "d-sm-none";
	}

	private sealed class SmSE : MediaQuery
	{
		public SmSE() : base($"{nameof(Id.Sm)}", Id.Sm) { }
		public override string DivClass => "d-none d-sm-block d-md-none d-lg-none d-xl-none";
	}

	private sealed class MdSE : MediaQuery
	{
		public MdSE() : base($"{nameof(Id.Md)}", Id.Md) { }
		public override string DivClass => "d-none d-md-block d-lg-none d-xl-none";
	}

	private sealed class LgSE : MediaQuery
	{
		public LgSE() : base($"{nameof(Id.Lg)}", Id.Lg) { }
		public override string DivClass => "d-none d-lg-block d-xl-none";
	}

	private sealed class XlSE : MediaQuery
	{
		public XlSE() : base($"{nameof(Id.Xl)}", Id.Xl) { }
		public override string DivClass => "d-none d-xl-block";
	}

	// Combination
	private sealed class XsOrSmSE : MediaQuery
	{
		public XsOrSmSE() : base($"{nameof(Id.XsOrSm)}", Id.XsOrSm) { }
		public override string DivClass => "d-md-none";
	}

	private sealed class SmOrMdOrLgOrXlSE : MediaQuery
	{
		public SmOrMdOrLgOrXlSE() : base($"{nameof(Id.SmOrMdOrLgOrXl)}", Id.SmOrMdOrLgOrXl) { }
		public override string DivClass => "d-none d-sm-block";
	}

	private sealed class MdOrLgOrXlSE : MediaQuery
	{
		public MdOrLgOrXlSE() : base($"{nameof(Id.MdOrLgOrXl)}", Id.MdOrLgOrXl) { }
		public override string DivClass => "d-none d-md-block";
	}

	private sealed class LgOrXlSE : MediaQuery
	{
		public LgOrXlSE() : base($"{nameof(Id.LgOrXl)}", Id.LgOrXl) { }
		public override string DivClass => "d-none d-lg-block";
	}

	/*	NOT WOWRKING
		private sealed class XsOrXlSE : MediaQuery
	{
		public XsOrXlSE() : base($"{nameof(Id.XsOrXl)}", Id.XsOrXl) { } // ToDo: what combo goes here?
		public override string DivClass => "d-sm-block d-md-block d-lg-block";
	}
	*/

	#endregion

}
