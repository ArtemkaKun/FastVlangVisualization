namespace FastVlangVisualization.DataGrabSystem;

public class TimePerformanceMeasureUnit : IPerformanceMeasureUnit
{
	private string Value { get; }

	public TimePerformanceMeasureUnit (string value)
	{
		Value = value;
	}

	public int GetNumericalValue ()
	{
		return 0;
	}
}