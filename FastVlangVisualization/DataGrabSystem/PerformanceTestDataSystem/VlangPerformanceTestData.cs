using FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;

namespace FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem;

public class VlangPerformanceTestData : IVlangPerformanceTestData
{
	public int PerformanceResultNumericValue => PerformanceResult.NumericalValue;

	public string Name { get; private set; }
	public DateTime Timestamp { get; private set; }
	public string CommitID { get; private set; }
	public string CommitMessage { get; private set; }

	private IPerformanceMeasureUnit PerformanceResult { get; set; }

	public void SetTestName (string name)
	{
		Name = name;
	}

	public void SetTimestamp (DateTime timestamp)
	{
		Timestamp = timestamp;
	}

	public void SetCommitID (string commitID)
	{
		CommitID = commitID;
	}

	public void SetCommitMessage (string message)
	{
		CommitMessage = message;
	}

	public void SetPerformanceResult (IPerformanceMeasureUnit performanceResult)
	{
		PerformanceResult = performanceResult;
	}
}