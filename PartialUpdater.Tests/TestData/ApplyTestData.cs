namespace PartialUpdater.Tests.TestData
{
	class ApplyTestData
    {
		internal static string PatchJson
		{
			get
			{
				return @"{
					""secondProperty"": {
						""secondInnerProperty"": {
							""firstMoreInnerProperty"": ""abc more inner""
						}
					},
					""thirdProperty"": true
					}";
			}
		}
	}
}
