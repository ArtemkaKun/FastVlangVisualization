using FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem;

namespace FastVlangVisualization.DataGrabSystem;

public class FastVlangWebPageDataGrabber : IDataGrabber
{
	private string FastVlangWebPageAddress { get; }

	public FastVlangWebPageDataGrabber (string fastVlangWebPageAddress)
	{
		FastVlangWebPageAddress = fastVlangWebPageAddress;
	}

	public async Task<List<IPerformanceTestData>> GetVlangSpeedDataAsync ()
	{
		string webPageContent = await GetWebPageContent();

		return new VlangTestsTableParser().ParseWebPageContent(webPageContent);
	}

	private async Task<string> GetWebPageContent ()
	{
		HttpClient webClient = new();

		return await webClient.GetStringAsync(FastVlangWebPageAddress);
	}
}