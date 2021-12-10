namespace FastVlangVisualization.DataGrabSystem;

public class VlangSpeedData : IVlangSpeedData
{
	private string TestName { get; set; }
	private DateTime Timestamp { get; set; }
	private string CommitID { get; set; }
	private string CommitMessage { get; set; }
	private IPerformanceMeasureUnit PerformanceResult { get; set; }

	public void SetTestName (string testName)
	{
		TestName = testName;
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
	
	public string GetTestName ()
	{
		return TestName;
	}

	public DateTime GetTimestamp ()
	{
		return Timestamp;
	}

	public string GetCommitID ()
	{
		return CommitID;
	}

	public string GetCommitMessage ()
	{
		return CommitMessage;
	}

	public IPerformanceMeasureUnit GetPerformanceResult ()
	{
		return PerformanceResult;
	}
}