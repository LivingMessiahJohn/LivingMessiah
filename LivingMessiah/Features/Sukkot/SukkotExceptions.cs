namespace  LivingMessiah.Features.Sukkot;

// public class SukkotExceptions{}
// ToDo: I think I what to delete these


public class UserNotAuthoirizedException : Exception
{
	public UserNotAuthoirizedException()
	{
	}
	public UserNotAuthoirizedException(string message)
			: base(message)
	{
	}

	public UserNotAuthoirizedException(string message, Exception inner)
			: base(message, inner)
	{
	}
}


