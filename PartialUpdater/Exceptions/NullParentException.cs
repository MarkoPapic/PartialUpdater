using System;
using System.Collections.Generic;
using System.Text;

namespace PartialUpdater.Exceptions
{
    public class NullParentException : Exception
    {
		public NullParentException()
		{
		}

		public NullParentException(string message)
			: base(message)
		{
		}

		public NullParentException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
