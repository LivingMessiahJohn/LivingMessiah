namespace LivingMessiah.Settings;

public class AppSettings
{
	/*
	LivingMessiah\Features\Calendar\Index.razor
	
	  Development: "AppSettings": { "Domain": "https://localhost:7142", ...
		Production: "AppSettings": {    "Domain": "https://livingmessiah.com",
		
		YearId = AppSettings!.Value.YearId;
	*/
	public int YearId { get; set; }  
}
