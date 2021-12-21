using FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem;
using FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;
using HtmlAgilityPack;

namespace FastVlangVisualization.DataGrabSystem;

public class VlangTestsTableParser
{
	private const string TABLE_CELL_HTML_ATTRIBUTE = "td";
	private const string TABLE_ROW_HTML_ATTRIBUTE = "tr";
	private const int EXPECTED_SERVICE_DATA_CELLS_COUNT = 3;

	private static readonly string[] TIME_RESULTS_MARKERS = {"ms"};
	private static readonly string[] MEMORY_RESULTS_MARKERS = {"KB"};

	public List<IPerformanceTestData> ParseWebPageContent (string webPageContent)
	{
		HtmlNodeCollection testResultsTableRows = GetPerformanceResultsTableRows(webPageContent);
		string[] tableHeaders = GetTableHeaders(testResultsTableRows[0]);

		return GetPerformanceDataCollection(testResultsTableRows, tableHeaders);
	}

	private HtmlNodeCollection GetPerformanceResultsTableRows (string webPageContent)
	{
		HtmlDocument htmlDocument = new();
		htmlDocument.LoadHtml(webPageContent);

		return htmlDocument.DocumentNode.SelectNodes($"//{TABLE_ROW_HTML_ATTRIBUTE}");
	}

	private string[] GetTableHeaders (HtmlNode testResultsTableRow)
	{
		List<HtmlNode> tableHeaderNodes = GetNextTableRowCells(testResultsTableRow);
		string[] tableHeaders = new string[tableHeaderNodes.Count];

		for (int headerIndex = 0; headerIndex < tableHeaderNodes.Count; headerIndex++)
		{
			tableHeaders[headerIndex] = tableHeaderNodes[headerIndex].InnerText;
		}

		return tableHeaders;
	}

	private List<IPerformanceTestData> GetPerformanceDataCollection (HtmlNodeCollection testResultsTableRows, IReadOnlyList<string> tableHeaders)
	{
		List<IPerformanceTestData> vlangPerformanceTestDataCollection = new();
		int expectedTestsCount = tableHeaders.Count - EXPECTED_SERVICE_DATA_CELLS_COUNT;
		List<PerformanceTestData> vlangPerformanceTestDataBuffer = new(expectedTestsCount);

		for (int rowIndex = 1; rowIndex < testResultsTableRows.Count; rowIndex++)
		{
			List<HtmlNode> tableCellsNodes = GetNextTableRowCells(testResultsTableRows[rowIndex]);
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
				PerformanceTestData newData = new();
				newData.SetTestName(tableHeaders[dataIndex + EXPECTED_SERVICE_DATA_CELLS_COUNT]);
				InsertServiceData(newData, tableCellsNodes);
				vlangPerformanceTestDataBuffer.Add(newData);
			}
		}
	}

	private List<HtmlNode> GetNextTableRowCells (HtmlNode testResultsTableRow)
	{
		List<HtmlNode> rowCellsCollection = new();

		for (int childNodeIndex = 0; childNodeIndex < testResultsTableRow.ChildNodes.Count; childNodeIndex++)
		{
			HtmlNode cachedChildNode = testResultsTableRow.ChildNodes[childNodeIndex];

			if (cachedChildNode.Name == TABLE_CELL_HTML_ATTRIBUTE)
			{
				rowCellsCollection.Add(cachedChildNode);
			}
		}

		return rowCellsCollection;
	}

	private void InsertServiceData (PerformanceTestData newData, IReadOnlyList<HtmlNode> tableCellsNodes)
	{
		DateTime testCaseConvertedTimestamp = CustomParseVlangTestCaseTimestamp(tableCellsNodes[0].InnerText);
		newData.SetTimestamp(testCaseConvertedTimestamp);
		newData.SetCommitID(tableCellsNodes[1].InnerText);
		newData.SetCommitMessage(tableCellsNodes[2].InnerText);
	}

	private static DateTime CustomParseVlangTestCaseTimestamp (string tableCellsNodes)
	{
		string[] dateAndTime = tableCellsNodes.Split(' ');
		string[] dateMembers = dateAndTime[0].Split('-');
		string[] timeMembers = dateAndTime[1].Split(':');
		DateTime testCaseConvertedTimestamp = new(int.Parse(dateMembers[0]), int.Parse(dateMembers[1]), int.Parse(dateMembers[2]), int.Parse(timeMembers[0]), int.Parse(timeMembers[1]), 0);

		return testCaseConvertedTimestamp;
	}

	private void InsertPerformanceResults (IReadOnlyList<HtmlNode> tableCellsNodes, IReadOnlyList<PerformanceTestData> vlangPerformanceTestDataBuffer)
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