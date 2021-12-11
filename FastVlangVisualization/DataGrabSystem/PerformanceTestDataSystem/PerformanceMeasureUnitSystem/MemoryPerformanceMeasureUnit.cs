namespace FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;

public class MemoryPerformanceMeasureUnit : BasePerformanceMeasureUnit
{
	public MemoryPerformanceMeasureUnit (string rawValue) : base(rawValue) { }
	
	protected override int NormalizeNumericalValue (int numericalValue)
	{
		return 0;
	}
}