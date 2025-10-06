using SukkotEnums = LivingMessiahAdmin.Features.Sukkot.Enums;

namespace LivingMessiahAdmin.Features.Sukkot.Dashboard;

public record DetailCardRecord(
int Id, 
string FullName, 
string AdminNotes, 
string UserNotes,
SukkotEnums.RegistrationFee RegistrationFee);  // int FeeEnumValue RegistrationFee
