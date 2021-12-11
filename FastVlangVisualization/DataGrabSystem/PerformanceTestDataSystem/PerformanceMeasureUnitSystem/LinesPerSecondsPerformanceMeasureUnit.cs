namespace FastVlangVisualization.DataGrabSystem;

public class LinesPerSecondsPerformanceMeasureUnit : IPerformanceMeasureUnit
{
	private string Value { get; }

	public LinesPerSecondsPerformanceMeasureUnit (string value)
	{
		Value = value;
	}

	public int GetNumericalValue ()
	{
		return 0;
	}
}