using Newtonsoft.Json;
using PartialUpdater.Constants;
using PartialUpdater.Exceptions;
using PartialUpdater.Model;
using PartialUpdater.Tests.TestData;
using System;
using Xunit;

namespace PartialUpdater.Tests
{
	public class Apply
    {
		[Fact]
		public void EntityNull_ExceptionThrown()
		{
			//Arrange
			var jsonString = ApplyTestData.PatchJson;
			TestEntity entity = null;
			var partialUpdate = JsonConvert.DeserializeObject<PartialUpdate<TestEntity>>(jsonString);

			//Act & assert
			Assert.Throws<ArgumentNullException>(() => partialUpdate.Apply(entity));
		}

		[Fact]
		public void PatchNullObject_ExceptionThrown()
		{
			//Arrange
			var jsonString = ApplyTestData.PatchJson;
			var entity = new TestEntity
			{
				SecondProperty = new SomeInnerEntity()
			};
			var partialUpdate = JsonConvert.DeserializeObject<PartialUpdate<TestEntity>>(jsonString);

			//Act & assert
			NullEntityPatchException nepe = Assert.Throws<NullEntityPatchException>(() => partialUpdate.Apply(entity));
			Assert.Equal(ErrorMessages.NullEntityPatch, nepe.Message);
		}

		[Fact]
		public void EverythingOk_EntityUpdated()
		{
			//Arrange
			var jsonString = ApplyTestData.PatchJson;
			var entity = new TestEntity
			{
				SecondProperty = new SomeInnerEntity
				{
					SecondInnerProperty = new SomeMoreInnerEntity()
				}
			};
			var partialUpdate = JsonConvert.DeserializeObject<PartialUpdate<TestEntity>>(jsonString);

			//Act
			partialUpdate.Apply(entity);

			//Assert
			Assert.Equal("abc more inner", entity.SecondProperty.SecondInnerProperty.FirstMoreInnerProperty);
			Assert.True(entity.ThirdProperty);
		}
	}
}
