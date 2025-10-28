using LivingMessiahSukkot.Features.Constants;

namespace LivingMessiahSukkot.Features.Print.Constants;

public static class ModalMessage
{
	public const string PayWithCheck = @$"
By clicking the <i class='fa fa-print'></i> <span class='text-muted'>Print</span> button,
you can choose where to print your registration form.
If for some reason you can not print the form that is OK, it just makes it convenient to process and it shows clearly the intent of your check.
<br /><br />The address of Living Messiah Ministries will be on the bottom of the form.
Make the check payable to <b>Living Messiah</b> and attach it to the printed out form before you mail it.
<br /><br />Please put the <b>registration id</b> on the check and write <b>Sukkot {Year.String} Payment</b>.  
<br /><br />Thanks!
";
}
