namespace FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;

public abstract class BasePerformanceMeasureUnit : IPerformanceMeasureUnit
{
	protected string RawValue { get; }

	protected BasePerformanceMeasureUnit (string rawValue)
	{
		RawValue = rawValue;
	}

	protected abstract int NormalizeNumericalValue (int numericalValue);
	
	public int GetNumericalValue ()
	{
		if (NumberRegexHelper.TryGetNumberFromString(RawValue, out int numericalValue) == true)
		{
			return NormalizeNumericalValue(numericalValue);
		}
		
		return 0;
	}
}