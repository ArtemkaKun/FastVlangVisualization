namespace FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;

public class TimePerformanceMeasureUnit : BasePerformanceMeasureUnit
{
	public TimePerformanceMeasureUnit (string rawValue) : base(rawValue) { }
	
	protected override int ConvertRawValueToNumerical ()
	{
		return 0;
	}
}