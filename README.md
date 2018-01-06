# Partial Updater

[![Build Status](https://travis-ci.org/MarkoPapic/PartialUpdater.svg?branch=develop)](https://travis-ci.org/MarkoPapic/PartialUpdater)

Partial Updater is a library for .NET developers that enables patching entities by providing the minimal JSON payload, containing only the fields that need to be updated, thus reducing the network traffic.

## Installation
[![NuGet Version](https://img.shields.io/nuget/vpre/PartialUpdater.svg)](https://www.nuget.org/packages/PartialUpdater)
```
Install-Package PartialUpdater -Version 1.0.0-alpha
```

## Usage
Say you have some blog post entity
```cs
class Post
{
	public int Id { get; set; }
	public string Title { get; set; }
	public DateTime Published { get; set; }
	public DateTime LastUpdated { get; set; }
	public string Content { get; set; }
	public Author Author { get; set; }
}
```
that needs to be updated over network. `Post`'s `Author` property is of type `Author`
```cs
class Author
{
	public string Username { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public ContactInfo ContactInfo { get; set; }
}
```
which, among others, has a `ContactInfo` property of type `ContactInfo`
```cs
class ContactInfo
{
	public string Email { get; set; }
	public string Website { get; set; }
}
```
Now imagine a user edits the post title and author's email in the browser (or some other remote app) and wants to save it. They could send the JSON representation of the updated post over network and then you would replace the original post with the updated one. That way, they could end up sending a large amount of unnecessary data over network (including post content which can be large) along with the post title and author's email that need to be updated. With Partial Updater, a client can send JSON that only contains the fields that need to be updated:
```json
{
	"author": {
		"contactInfo": {
			"email": "newemail@example.com"
		}
	},
	"title": "New title"
}
```
and you can update the original entity the following way:
```cs
PartialUpdate partialUpdate = JsonConvert.DeserializeObject<PartialUpdate<Post>>(jsonString);
partialUpdate.Apply(originalPost);
```
where `jsonString` is a variable of type `string` that contains the above JSON content and `originalPost` is an object of type `Post` that represents the original blog post entity that will be updated.
`Apply` method will only update the fields specified in the JSON payload (even if they are nested) and all the other fields in the original post will remain unchanged.

### Using with ASP.NET Core
Imagine your blog from the previous example is an ASP.NET Core MVC application. In that case, all you need to do is to accept `PartialUpdate<T>` type from the request body and pass your entity type as a generic parameter.
```cs
public class BlogController : Controller
{
	[HttpPatch]
	public void UpdatePost([FromBody] PartialUpdate<Post> partialUpdate)
	{
		Post originalPost = ... //retrieve the original post
		partialUpdate.Apply(originalPost);
		//save the post
	}
}
```

## Building from source
```
git clone https://github.com/MarkoPapic/PartialUpdater.git
cd PartialUpdater
dotnet restore
dotnet build ./PartialUpdater.sln
dotnet test ./PartialUpdater.Tests/
```

## License
[MIT License](https://github.com/MarkoPapic/PartialUpdater/blob/develop/LICENSE.txt)