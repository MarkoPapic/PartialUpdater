using Microsoft.AspNetCore.Mvc;
using PartialUpdater.AspCoreClient.Model;
using PartialUpdater.Exceptions;
using PartialUpdater.Model;
using System.Collections.Generic;

namespace PartialUpdater.AspCoreClient.Controllers
{
	[Route("api/[controller]")]
	public class BlogController : Controller
    {
		[HttpPatch]
		public IActionResult Patch([FromBody] PartialUpdate<Post> partialUpdate)
		{
			var post = new Post
			{
				Author = new Author
				{
					ContactInfo = new ContactInfo()
				}
			};

			try
			{
				partialUpdate.Apply(post);
				return Ok();
			}
			catch(NullEntityPatchException npe)
			{
				return BadRequest(npe.Message);
			}
		}
	}
}
