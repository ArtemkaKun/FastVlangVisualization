using FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;
using HtmlAgilityPack;

namespace FastVlangVisualization.DataGrabSystem;

public class FastVlangWebPageDataGrabber : IDataGrabber
{
	private string FastVlangWebPageAddress { get; }
	private int ExpectedTableColumnsCount { get; }

	public FastVlangWebPageDataGrabber (string fastVlangWebPageAddress, int expectedTableColumnsCount)
	{
		FastVlangWebPageAddress = fastVlangWebPageAddress;
		ExpectedTableColumnsCount = expectedTableColumnsCount;
	}

	public async Task<List<IVlangSpeedData>> GetVlangSpeedDataAsync ()
	{
		string webPageContent = await GetWebPageContent();
		return ParseWebPageContent(webPageContent);
	}

	private async Task<string> GetWebPageContent ()
	{
		HttpClient webClient = new();

		return await webClient.GetStringAsync(FastVlangWebPageAddress);
	}

	private List<IVlangSpeedData> ParseWebPageContent (string webPageContent)
	{
		HtmlDocument htmlDocument = new();
		htmlDocument.LoadHtml(webPageContent);

		using IEnumerator<HtmlNode> testResultsTableCells = htmlDocument.DocumentNode.Descendants("tr").GetEnumerator();

		testResultsTableCells.MoveNext();

		string[] tableHeaders = new string[ExpectedTableColumnsCount];

		for (int headerIndex = 0; headerIndex < ExpectedTableColumnsCount; headerIndex++)
		{
			tableHeaders[headerIndex] = testResultsTableCells.Current.ChildNodes.Where(node => node.Name == "td").ToList()[headerIndex].InnerText;
		}

		List<IVlangSpeedData> data = new();
		const int expectedTestsResultsCellsCount = 11;
		List<VlangSpeedData> testsDataBuffer = new(expectedTestsResultsCellsCount);

		while (testResultsTableCells.MoveNext() == true)
		{
			testsDataBuffer.Clear();

			for (int testIndex = 0; testIndex < expectedTestsResultsCellsCount; testIndex++)
			{
				VlangSpeedData newData = new();
				newData.SetTestName(tableHeaders[testIndex + 3]);
				testsDataBuffer.Add(newData);
			}

			List<HtmlNode> test = testResultsTableCells.Current.ChildNodes.Where(node => node.Name == "td").ToList();

			for (int cellIndex = 0; cellIndex < test.Count; cellIndex++)
			{
				string cellText = test[cellIndex].InnerText;

				if (cellIndex == 0)
				{
					for (int testIndex = 0; testIndex < expectedTestsResultsCellsCount; testIndex++)
					{
						testsDataBuffer[testIndex].SetTimestamp(DateTime.Parse(cellText));
					}
				}
				else if (cellIndex == 1)
				{
					for (int testIndex = 0; testIndex < expectedTestsResultsCellsCount; testIndex++)
					{
						testsDataBuffer[testIndex].SetCommitID(cellText);
					}
				}
				else if (cellIndex == 2)
				{
					for (int testIndex = 0; testIndex < expectedTestsResultsCellsCount; testIndex++)
					{
						testsDataBuffer[testIndex].SetCommitMessage(cellText);
					}
				}
				else if (cellIndex < expectedTestsResultsCellsCount)
				{
					IPerformanceMeasureUnit resultsUnit;

					if (cellText.Contains("ms"))
					{
						resultsUnit = new TimePerformanceMeasureUnit(cellText);
					}
					else if (cellText.Contains("KB"))
					{
						resultsUnit = new MemoryPerformanceMeasureUnit(cellText);
					}
					else
					{
						resultsUnit = new LinesPerformanceMeasureUnit(cellText);
					}

					testsDataBuffer[cellIndex - 3].SetPerformanceResult(resultsUnit);
				}
			}

			data.AddRange(testsDataBuffer);
		}

		return data;
	}
}