namespace RCL.Features.Parasha.Enums;

public record VerseRange(RCL.Enums.BibleBook BibleBook, string ChapterVerse, int	BegId, int EndId);

/*

## Instantiated by `SmartEnum<Enums.Triennial>` inside #region Extra Fields

```csharp
  public abstract VerseRange TorahVerse { get; }
  public abstract List<VerseRange>? HaftorahVerses { get; }
  public abstract List<VerseRange>? BritVerses { get; }
```

### ChapterVerse example:  "1:1-2:3" or "38:11-40:2" or "1:1-5"

## Used by 

### Helpers.GetPdfFile() => 
- replaces ("-", "-to-"), (":", "-"), (" & ", "-and-")
- appends "-teaching.pdf" or ".pdf"; // based on PdfType
- returns something like: 2026-07-25-Lev-19-and-20.pdf

### ArchiveGridAnchor.razor

*/
