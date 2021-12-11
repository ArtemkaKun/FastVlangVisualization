namespace FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem;

public interface IVlangPerformanceTestData
{
	string Name { get; }
	DateTime Timestamp { get; }
	string CommitID { get; }
	string CommitMessage { get; }
	int NumValue { get; }
}