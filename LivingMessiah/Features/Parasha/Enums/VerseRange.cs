using LivingMessiah.Enums;

namespace LivingMessiah.Features.Parasha.Enums;

// See 057-Strongs-Frequency-Analysis\Notes.md re.  `VerseRange.cs` ! `GetSatVerseList()`
public record VerseRange(BibleBook BibleBook, string ChapterVerse, int	BegId, int EndId);

