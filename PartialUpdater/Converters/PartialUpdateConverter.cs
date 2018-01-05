using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using PartialUpdater.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PartialUpdater.Converters
{
	public class PartialUpdateConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var root = JObject.Load(reader);
			var leafs = GetLeafs(root);
			var genericType = objectType.GenericTypeArguments[0];
			var lambdas = leafs.Select(x => GetLambda(x, genericType, serializer)).ToList();

			var uptaderType = typeof(PartialUpdate<>);
			var makeme = uptaderType.MakeGenericType(genericType);
			object o = Activator.CreateInstance(makeme);
			PartialUpdate partialUpdate = o as PartialUpdate;
			if (partialUpdate == null)
				return null;
			partialUpdate.Updates = lambdas;
			return partialUpdate;
		}

		private IEnumerable<LeafValuePair> GetLeafs(JToken token)
		{
			if (!token.HasValues)
				return new List<LeafValuePair> { new LeafValuePair { LeafPath = token.Path, LeafValue = token.Value<IConvertible>() } };
			var children = token.Children();
			var retVal = new List<LeafValuePair>();

			foreach (var child in children)
			{
				retVal.AddRange(GetLeafs(child));
			}

			return retVal;
		}


		private LambdaExpression GetLambda(LeafValuePair leafValue, Type type, JsonSerializer serializer)
		{
			var param = Expression.Parameter(type, "x");
			Expression left = param;

			string[] pathTokens = leafValue.LeafPath.Split('.');
			var currentType = type;

			foreach (var pathToken in pathTokens)
			{
				var contract = serializer.ContractResolver.ResolveContract(currentType) as JsonObjectContract;
				if (contract == null)
					throw new InvalidOperationException("Only object types can be partially updated");
				var matchingProperty = contract.Properties.GetClosestMatchProperty(pathToken);

				left = Expression.Property(left, matchingProperty.UnderlyingName);

				currentType = matchingProperty.PropertyType;
			}

			Expression right = Expression.Constant(Convert.ChangeType(leafValue.LeafValue, currentType));

			var myExpression = Expression.Assign(left, right);
			return Expression.Lambda(myExpression, param);
		}

		public override bool CanConvert(Type objectType)
		{
			throw new NotImplementedException();
		}

		internal class LeafValuePair
		{
			internal string LeafPath { get; set; }
			internal IConvertible LeafValue { get; set; }
		}
	}
}
