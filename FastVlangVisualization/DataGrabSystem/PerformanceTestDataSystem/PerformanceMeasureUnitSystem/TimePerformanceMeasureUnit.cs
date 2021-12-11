namespace FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;

public class TimePerformanceMeasureUnit : BasePerformanceMeasureUnit
{
	public TimePerformanceMeasureUnit (string rawValue) : base(rawValue) { }
	
	protected override int NormalizeNumericalValue (int numericalValue)
	{
		if (RawValue.Contains("ms", StringComparison.OrdinalIgnoreCase) == true)
		{
			return numericalValue;
		}
		
		return 0;
	}
}