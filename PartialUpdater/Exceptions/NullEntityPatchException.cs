using System;

namespace PartialUpdater.Exceptions
{
	public class NullEntityPatchException : Exception
    {
		public NullEntityPatchException()
		{
		}

		public NullEntityPatchException(string message)
			: base(message)
		{
		}

		public NullEntityPatchException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
