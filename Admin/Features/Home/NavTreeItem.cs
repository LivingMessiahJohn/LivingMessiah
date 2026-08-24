namespace Admin.Features.Home;

public class NavTreeItem
{
	public string Text { get; set; } = string.Empty;
	public string Title { get; set; } = string.Empty;
	public string Index { get; set; } = string.Empty;
	public string IconClass { get; set; } = "far fa-folder";   // closed folder by default
	public string OpenIconClass { get; set; } = "far fa-folder-open";
	public bool IsExpanded { get; set; } = false;
	public int RequiredRoles { get; set; } // 0 = no specific role required
	public bool IsFolder => Children.Any();
	public List<NavTreeItem> Children { get; set; } = new();
}