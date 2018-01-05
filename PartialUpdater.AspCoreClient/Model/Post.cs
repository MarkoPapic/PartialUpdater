using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartialUpdater.AspCoreClient.Model
{
	public class Post
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime Published { get; set; }
		public DateTime LastUpdated { get; set; }
		public string Content { get; set; }
		public Author Author { get; set; }
	}

	public class Author
	{
		public string Username { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public ContactInfo ContactInfo { get; set; }
	}

	public class ContactInfo
	{
		public string Email { get; set; }
		public string Website { get; set; }
	}
}
