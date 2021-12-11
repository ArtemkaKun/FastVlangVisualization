using FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem;

namespace FastVlangVisualization.DataProcessorSystem;

public class VlangDataProcessor : IDataProcessor
{
	public IReadOnlyDictionary<string, List<IPerformanceTestData>> GroupedTestResultsMap => GroupedVlangTestResultsMap;
	
	public Dictionary<string, List<IPerformanceTestData>> GroupedVlangTestResultsMap { get; }

	public VlangDataProcessor (IReadOnlyList<IPerformanceTestData> testDataCollection)
	{
		GroupedVlangTestResultsMap = new Dictionary<string, List<IPerformanceTestData>>();
		FillTestResultsMap(testDataCollection);
	}

	private void FillTestResultsMap (IReadOnlyList<IPerformanceTestData> testDataCollection)
	{
		for (int dataIndex = 0; dataIndex < testDataCollection.Count; dataIndex++)
		{
			IPerformanceTestData cachedTestData = testDataCollection[dataIndex];

			if (cachedTestData.PerformanceResultNumericValue == null)
			{
				continue;
			}

			string cachedTestName = cachedTestData.Name;

			if (GroupedVlangTestResultsMap.ContainsKey(cachedTestName) == true)
			{
				GroupedVlangTestResultsMap[cachedTestName].Add(cachedTestData);
			}
			else
			{
				GroupedVlangTestResultsMap[cachedTestName] = new List<IPerformanceTestData> {cachedTestData};
			}
		}
	}
}