namespace FastVlangVisualization.DataGrabSystem;

public interface IVlangSpeedData
{
	string GetTestName ();
	DateTime GetTimestamp ();
	string GetCommitID ();
	string GetCommitMessage ();
	IPerformanceMeasureUnit GetPerformanceResult ();
}