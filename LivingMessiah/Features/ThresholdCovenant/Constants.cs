﻿using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Features.ThresholdCovenant;

public static class MyHebrewBible
{
	//private const string articleBaseUrl = "https://myhebrewbible.com/Article/";
	private const string articleBaseUrl = "https://myhebrewbible.azurewebsites.net/Article/";
	public static string ArticleUrl(int id, string slug)
	{
		return $"{articleBaseUrl}/{id}/{slug}/";
	}
}

public static class DynamicComponentPaths
{
	public static string BookSectionCards = "LivingMessiah.Features.ThresholdCovenant.BookSections.";
	public static string SubSectionCards = "LivingMessiah.Features.ThresholdCovenant.BookSections.SubSections.";
}


public static class Strongs
{
	public static MarkupString H(string number)
	{
		return (MarkupString)$"<sup><a href='https://www.blueletterbible.org/lexicon/{number}/kjv/wlc/0-1/' target='blank' title='Blue Letter Bible WLC Lexicon {number}'>{number}</a></sup>";
	}

	public static MarkupString G(string number)
	{
		return (MarkupString)$"<sup><a href='https://www.blueletterbible.org/lexicon/{number}/kjv/tr/0-1/>' target='blank' title='Blue Letter Bible TR Lexicon {number}'{number}</a></sup>";
	}

	public static MarkupString HandTransLit(string transliteration, string number)
	{
		return (MarkupString)$"<sup><i>{transliteration}</i> <a href='https://www.blueletterbible.org/lexicon/{number}/kjv/wlc/0-1/' target='blank' title='Blue Letter Bible WLC Lexicon {number}'>{number}</a></sup>";
	}

}

public static class Blobs
{
	private const string baseUrl = "https://livingmessiahstorage.blob.core.windows.net/images/threshold-covenant/";

	public static string Image(string blob)
	{
		return baseUrl + blob;
	}
}

// Ignore Spelling: Strongs, kjv, wlc