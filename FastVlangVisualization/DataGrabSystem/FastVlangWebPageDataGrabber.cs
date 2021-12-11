using FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem;
using FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;
using HtmlAgilityPack;

namespace FastVlangVisualization.DataGrabSystem;

public class FastVlangWebPageDataGrabber : IDataGrabber
{
	private string FastVlangWebPageAddress { get; }

	private const string TABLE_CELL_HTML_ATTRIBUTE = "td";
	private const string TABLE_ROW_HTML_ATTRIBUTE = "tr";
	private const int EXPECTED_SERVICE_DATA_CELLS_COUNT = 3;
	
	private static readonly string[] TIME_RESULTS_MARKERS = {"ms"};
	private static readonly string[] MEMORY_RESULTS_MARKERS = {"KB"};

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
		HtmlNode[] testResultsTableRows = GetPerformanceResultsTableRows(webPageContent);
		string[] tableHeaders = GetTableHeaders(testResultsTableRows[0]);

		return GetPerformanceDataCollection(testResultsTableRows, tableHeaders);
	}

	private List<IVlangPerformanceTestData> GetPerformanceDataCollection (IReadOnlyList<HtmlNode> testResultsTableRows, IReadOnlyList<string> tableHeaders)
	{
		List<IVlangPerformanceTestData> vlangPerformanceTestDataCollection = new();
		int expectedTestsCount = tableHeaders.Count - EXPECTED_SERVICE_DATA_CELLS_COUNT;
		List<VlangPerformanceTestData> vlangPerformanceTestDataBuffer = new(expectedTestsCount);

		for (int rowIndex = 1; rowIndex < testResultsTableRows.Count; rowIndex++)
		{
			HtmlNode[] tableCellsNodes = GetNextTableRowCells(testResultsTableRows[rowIndex]);
			PreparePerformanceTestDataBuffer(tableCellsNodes);
			InsertPerformanceResults(tableCellsNodes, vlangPerformanceTestDataBuffer);
			vlangPerformanceTestDataCollection.AddRange(vlangPerformanceTestDataBuffer);
		}

		return vlangPerformanceTestDataCollection;

		void PreparePerformanceTestDataBuffer (IReadOnlyList<HtmlNode> tableCellsNodes)
		{
			vlangPerformanceTestDataBuffer.Clear();

			for (int dataIndex = 0; dataIndex < expectedTestsCount; dataIndex++)
			{
				VlangPerformanceTestData newData = new();
				newData.SetTestName(tableHeaders[dataIndex + EXPECTED_SERVICE_DATA_CELLS_COUNT]);
				InsertServiceData(newData, tableCellsNodes);
				vlangPerformanceTestDataBuffer.Add(newData);
			}
		}
	}

	private HtmlNode[] GetPerformanceResultsTableRows (string webPageContent)
	{
		HtmlDocument htmlDocument = new();
		htmlDocument.LoadHtml(webPageContent);

		return htmlDocument.DocumentNode.Descendants(TABLE_ROW_HTML_ATTRIBUTE).ToArray();
	}

	private string[] GetTableHeaders (HtmlNode testResultsTableRow)
	{
		HtmlNode[] tableHeaderNodes = GetNextTableRowCells(testResultsTableRow);
		string[] tableHeaders = new string[tableHeaderNodes.Length];

		for (int headerIndex = 0; headerIndex < tableHeaderNodes.Length; headerIndex++)
		{
			tableHeaders[headerIndex] = tableHeaderNodes[headerIndex].InnerText;
		}

		return tableHeaders;
	}

	private HtmlNode[] GetNextTableRowCells (HtmlNode testResultsTableRow)
	{
		return testResultsTableRow.ChildNodes.Where(node => node.Name == TABLE_CELL_HTML_ATTRIBUTE).ToArray();
	}

	private void InsertServiceData (VlangPerformanceTestData newData, IReadOnlyList<HtmlNode> tableCellsNodes)
	{
		newData.SetTimestamp(DateTime.Parse(tableCellsNodes[0].InnerText));
		newData.SetCommitID(tableCellsNodes[1].InnerText);
		newData.SetCommitMessage(tableCellsNodes[2].InnerText);
	}

	private void InsertPerformanceResults (IReadOnlyList<HtmlNode> tableCellsNodes, IReadOnlyList<VlangPerformanceTestData> vlangPerformanceTestDataBuffer)
	{
		for (int cellIndex = EXPECTED_SERVICE_DATA_CELLS_COUNT; cellIndex < tableCellsNodes.Count; cellIndex++)
		{
			string cellText = tableCellsNodes[cellIndex].InnerText;
			IPerformanceMeasureUnit resultsUnit = CreateSuitablePerformanceResultsUnit(cellText);
			vlangPerformanceTestDataBuffer[cellIndex - EXPECTED_SERVICE_DATA_CELLS_COUNT].SetPerformanceResult(resultsUnit);
		}
	}

	private IPerformanceMeasureUnit CreateSuitablePerformanceResultsUnit (string cellText)
	{
		IPerformanceMeasureUnit resultsUnit;

		if (CheckIfResultContainsAnyMarker(TIME_RESULTS_MARKERS, cellText) == true)
		{
			resultsUnit = new TimePerformanceMeasureUnit(cellText);
		}
		else if (CheckIfResultContainsAnyMarker(MEMORY_RESULTS_MARKERS, cellText) == true)
		{
			resultsUnit = new MemoryPerformanceMeasureUnit(cellText);
		}
		else
		{
			resultsUnit = new LinesPerformanceMeasureUnit(cellText);
		}

		return resultsUnit;
	}

	private bool CheckIfResultContainsAnyMarker (IReadOnlyList<string> markers, string result)
	{
		for (int markerIndex = 0; markerIndex < markers.Count; markerIndex++)
		{
			if (result.Contains(markers[markerIndex], StringComparison.OrdinalIgnoreCase) == true)
			{
				return true;
			}
		}

		return false;
	}
}