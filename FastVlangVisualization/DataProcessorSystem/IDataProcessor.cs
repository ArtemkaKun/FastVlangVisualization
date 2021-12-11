using FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem;

namespace FastVlangVisualization.DataProcessorSystem;

public interface IDataProcessor
{
	Dictionary<string, IPerformanceTestData> GroupedTestResultsMap { get; }
}