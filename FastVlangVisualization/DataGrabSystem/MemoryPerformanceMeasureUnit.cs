namespace FastVlangVisualization.DataGrabSystem;

public class MemoryPerformanceMeasureUnit : IPerformanceMeasureUnit
{
	private string Value { get; }

	public MemoryPerformanceMeasureUnit (string value)
	{
		Value = value;
	}

	public int GetNumericalValue ()
	{
		return 0;
	}
}