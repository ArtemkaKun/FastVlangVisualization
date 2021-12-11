namespace FastVlangVisualization.DataGrabSystem;

public class TimePerformanceMeasureUnit : IPerformanceMeasureUnit
{
	public int NumValue => GetNumericalValue();
	private string Value { get; }

	public TimePerformanceMeasureUnit (string value)
	{
		Value = value;
	}

	public int GetNumericalValue ()
	{
		return int.Parse(Value.Replace("ms", ""));
	}
}