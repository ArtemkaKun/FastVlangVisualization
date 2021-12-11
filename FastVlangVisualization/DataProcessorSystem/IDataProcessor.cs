using FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem;

namespace FastVlangVisualization.DataProcessorSystem;

public interface IDataProcessor
{
	IReadOnlyDictionary<string, List<IPerformanceTestData>> GroupedTestResultsMap { get; }
}