namespace FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;

public interface IPerformanceMeasureUnit
{
	int NumericalValue { get; }
	
	string FormatValue (object value);
}