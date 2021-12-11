namespace FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;

public class LinesPerformanceMeasureUnit : BasePerformanceMeasureUnit
{
	public LinesPerformanceMeasureUnit (string rawValue) : base(rawValue) { }
	
	protected override int NormalizeNumericalValue (int numericalValue)
	{
		return numericalValue;
	}
}