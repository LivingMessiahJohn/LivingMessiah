namespace RCL.Features.Calendar.Constants;

public static class Blobs
{
	public static string Link(string blob)
	{
    return "https://livingmessiahstorage.blob.core.windows.net/images/calendar/" + blob;
	}

  // calendar-2023-[01]-[12].jpg
  // calendar-2024-[01]-[12].jpg
  // calendar-2025-[01]-[13].jpg, calendar-2025-13-appointed-times.jpg
  public static string ScreenShotsLink(string blob)
  {
    return "https://livingmessiahstorage.blob.core.windows.net/images/calendar/" + blob;
  }

}
