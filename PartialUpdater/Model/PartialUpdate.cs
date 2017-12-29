using Newtonsoft.Json;
using PartialUpdater.Converters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

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
			foreach (var update in Updates)
			{
				update.Compile().DynamicInvoke(entity);
			}
		}
	}
}
