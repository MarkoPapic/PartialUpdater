using System;
using System.Collections.Generic;
using System.Text;

namespace PartialUpdater.Tests.TestData
{
	class TestEntity
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
