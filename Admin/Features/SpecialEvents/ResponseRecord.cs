namespace Admin.Features.SpecialEvents;

public record ResponseRecord(Enums.ResponseMessage MessageType, string Message, bool RefreshList);


