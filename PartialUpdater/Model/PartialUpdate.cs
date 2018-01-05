using Newtonsoft.Json;
using PartialUpdater.Constants;
using PartialUpdater.Converters;
using PartialUpdater.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace PartialUpdater.Model
{
	public class PartialUpdate
	{
		internal List<LambdaExpression> Updates { get; set; }
	}

	[JsonConverter(typeof(PartialUpdateConverter))]
	public class PartialUpdate<T> : PartialUpdate
	{
		public void Apply(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			foreach (var update in Updates)
			{
				try
				{
					update.Compile().DynamicInvoke(entity);
				}
				catch (TargetInvocationException tie)
				{
					if (tie.InnerException != null && tie.InnerException is NullReferenceException)
					{
						throw new NullParentException(ErrorMessages.NullParent, tie.InnerException);
					}
					else
					{
						throw;
					}
				}
			}
		}
	}
}
