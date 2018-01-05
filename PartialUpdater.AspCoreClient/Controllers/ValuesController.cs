using Microsoft.AspNetCore.Mvc;
using PartialUpdater.AspCoreClient.Model;
using PartialUpdater.Exceptions;
using PartialUpdater.Model;
using System.Collections.Generic;

namespace PartialUpdater.AspCoreClient.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
    {
		[HttpPatch("{id}")]
		public IActionResult Patch([FromBody] PartialUpdate<SomeEntity> value)
		{
			/*var someEntity = new SomeEntity
			{
				SecondProperty = new SomeInnerEntity
				{
					SecondInnerProperty = new SomeMoreInnerEntity()
				}
			};*/
			var someEntity = new SomeEntity
			{
				SecondProperty = new SomeInnerEntity()
			};

			try
			{
				value.Apply(someEntity);
				return Ok();
			}
			catch(NullParentException npe)
			{
				return BadRequest(npe.Message);
			}
		}
	}
}
