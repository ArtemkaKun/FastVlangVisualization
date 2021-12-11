namespace FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;

public abstract class BasePerformanceMeasureUnit : IPerformanceMeasureUnit
{
	private string RawValue { get; }

	protected BasePerformanceMeasureUnit (string rawValue)
	{
		RawValue = rawValue;
	}

	protected abstract int ConvertRawValueToNumerical ();
	
	public int GetNumericalValue ()
	{
		return ConvertRawValueToNumerical();
	}
}