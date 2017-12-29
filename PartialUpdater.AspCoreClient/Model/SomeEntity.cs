using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartialUpdater.AspCoreClient.Model
{
	public class SomeEntity
	{
		public string PrviProperti { get; set; }
		public SomeInnerEntity DrugiProperti { get; set; }
		public bool TreciProperti { get; set; }
	}

	public class SomeInnerEntity
	{
		public string PrviInner { get; set; }
		public SomeMoreInnerEntity DrugiInner { get; set; }
	}

	public class SomeMoreInnerEntity
	{
		public string PrviMoreInner { get; set; }
	}
}
