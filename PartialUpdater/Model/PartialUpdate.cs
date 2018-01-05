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

	/// <summary>
	/// This class contains updates for an entity. JSON is deserialized into this type.
	/// </summary>
	/// <typeparam name="T">The type of entity to be updated.</typeparam>
	[JsonConverter(typeof(PartialUpdateConverter))]
	public class PartialUpdate<T> : PartialUpdate
	{
		/// <summary>
		/// Applies the updates to the entity.
		/// </summary>
		/// <param name="entity">Entity to be updated.</param>
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
						throw new NullEntityPatchException(ErrorMessages.NullEntityPatch, tie.InnerException);
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
