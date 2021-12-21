using System.Net.Http;
using System.Threading.Tasks;
using FastVlangVisualization.DataGrabSystem;
using NUnit.Framework;

namespace Tests.DataGrabSystemTests;

public class DataGrabberTests
{
	[Test]
	public async Task TestVlangTestsTableParserExecutionTime ()
	{
		HttpClient webClient = new();
		string vlangWebPageHTML = await webClient.GetStringAsync("https://fast.vlang.io/");
		
		new VlangTestsTableParser().ParseWebPageContent(vlangWebPageHTML);
	}
}