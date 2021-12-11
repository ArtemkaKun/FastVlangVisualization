namespace FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem;

public interface IPerformanceTestData
{
	string Name { get; }
	DateTime Timestamp { get; }
	string CommitID { get; }
	string CommitMessage { get; }
	int PerformanceResultNumericValue { get; }
}