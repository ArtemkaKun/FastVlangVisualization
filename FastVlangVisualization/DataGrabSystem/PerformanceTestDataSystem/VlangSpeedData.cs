namespace FastVlangVisualization.DataGrabSystem;

public class VlangSpeedData : IVlangSpeedData
{
	public string TestName { get; set; }
	public DateTime Timestamp { get; set; }
	public string CommitID { get; set; }
	public string CommitMessage { get; set; }
	public IPerformanceMeasureUnit PerformanceResult { get; set; }
	public int NumValue => PerformanceResult.GetNumericalValue();

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