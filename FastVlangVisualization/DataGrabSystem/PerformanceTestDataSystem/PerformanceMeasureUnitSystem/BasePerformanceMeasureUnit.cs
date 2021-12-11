namespace FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;

public abstract class BasePerformanceMeasureUnit : IPerformanceMeasureUnit
{
	protected string RawValue { get; }

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