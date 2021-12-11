using FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;

namespace FastVlangVisualization.DataGrabSystem;

public interface IVlangSpeedData
{
	string TestName { get; }
	DateTime Timestamp { get; }
	string CommitID { get; }
	string CommitMessage { get; }
	int NumValue { get; }
	
	string GetTestName ();
	DateTime GetTimestamp ();
	string GetCommitID ();
	string GetCommitMessage ();
	IPerformanceMeasureUnit GetPerformanceResult ();
}