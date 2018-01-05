using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartialUpdater.AspCoreClient.Model
{
	public class SomeEntity
	{
		public string FirstProperty { get; set; }
		public SomeInnerEntity SecondProperty { get; set; }
		public bool ThirdProperty { get; set; }
	}

	public class SomeInnerEntity
	{
		public string FirstInnerProperty { get; set; }
		public SomeMoreInnerEntity SecondInnerProperty { get; set; }
	}

	public class SomeMoreInnerEntity
	{
		public string FirstMoreInnerProperty { get; set; }
	}
}
