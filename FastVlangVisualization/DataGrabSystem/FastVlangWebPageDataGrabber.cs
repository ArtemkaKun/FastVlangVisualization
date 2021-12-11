using FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem;
using FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;
using HtmlAgilityPack;

namespace FastVlangVisualization.DataGrabSystem;

public class FastVlangWebPageDataGrabber : IDataGrabber
{
	private string FastVlangWebPageAddress { get; }

	public FastVlangWebPageDataGrabber (string fastVlangWebPageAddress)
	{
		FastVlangWebPageAddress = fastVlangWebPageAddress;
	}

	public async Task<List<IVlangPerformanceTestData>> GetVlangSpeedDataAsync ()
	{
		string webPageContent = await GetWebPageContent();
		return ParseWebPageContent(webPageContent);
	}

	private async Task<string> GetWebPageContent ()
	{
		HttpClient webClient = new();

		return await webClient.GetStringAsync(FastVlangWebPageAddress);
	}

	private List<IVlangPerformanceTestData> ParseWebPageContent (string webPageContent)
	{
		HtmlDocument htmlDocument = new();
		htmlDocument.LoadHtml(webPageContent);

		using IEnumerator<HtmlNode> testResultsTableCells = htmlDocument.DocumentNode.Descendants("tr").GetEnumerator();

		testResultsTableCells.MoveNext();
		HtmlNode[] tableHeaderNodes = testResultsTableCells.Current.ChildNodes.Where(node => node.Name == "td").ToArray();
		string[] tableHeaders = new string[tableHeaderNodes.Length];

		for (int headerIndex = 0; headerIndex < tableHeaderNodes.Length; headerIndex++)
		{
			tableHeaders[headerIndex] = tableHeaderNodes[headerIndex].InnerText;
		}

		List<IVlangPerformanceTestData> data = new();
		const int expectedTestsResultsCellsCount = 11;
		List<VlangPerformanceTestData> testsDataBuffer = new(expectedTestsResultsCellsCount);

		while (testResultsTableCells.MoveNext() == true)
		{
			testsDataBuffer.Clear();

			for (int testIndex = 0; testIndex < expectedTestsResultsCellsCount; testIndex++)
			{
				VlangPerformanceTestData newData = new();
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