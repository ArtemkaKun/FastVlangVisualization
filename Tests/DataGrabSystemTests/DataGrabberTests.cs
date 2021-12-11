using System.Threading.Tasks;
using FastVlangVisualization.DataGrabSystem;
using NUnit.Framework;

namespace Tests.DataGrabSystemTests;

public class DataGrabberTests
{
	[Test]
	public async Task TestGetVlangSpeedDataAsyncExecutionTime ()
	{
		IDataGrabber testGrabber = new FastVlangWebPageDataGrabber("https://fast.vlang.io/");
		await testGrabber.GetVlangSpeedDataAsync();
	}
}