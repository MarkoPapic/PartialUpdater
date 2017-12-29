using Microsoft.AspNetCore.Mvc;
using PartialUpdater.AspCoreClient.Model;
using PartialUpdater.Model;
using System.Collections.Generic;

namespace PartialUpdater.AspCoreClient.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
    {
		[HttpPatch("{id}")]
		public void Patch([FromBody] PartialUpdate<SomeEntity> value)
		{
			var someEntity = new SomeEntity
			{
				DrugiProperti = new SomeInnerEntity
				{
					DrugiInner = new SomeMoreInnerEntity()
				}
			};

			value.Apply(someEntity);
		}
	}
}
