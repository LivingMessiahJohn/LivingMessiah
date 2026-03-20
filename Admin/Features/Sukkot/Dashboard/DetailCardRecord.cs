using RCL.Features.Sukkot.Enums;

namespace Admin.Features.Sukkot.Dashboard;

public record DetailCardRecord(
int Id, 
string FullName, 
string OtherNames,
string AdminNotes, 
string UserNotes,
RegistrationFee RegistrationFee);  // int FeeEnumValue RegistrationFee
