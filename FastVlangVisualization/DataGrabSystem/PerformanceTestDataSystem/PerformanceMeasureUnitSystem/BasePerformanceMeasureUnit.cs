namespace FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;

public abstract class BasePerformanceMeasureUnit : IPerformanceMeasureUnit
{
	public int NumericalValue { get; }
	
	protected string RawValue { get; }

	protected BasePerformanceMeasureUnit (string rawValue)
	{
		RawValue = rawValue;
		NumericalValue = GetNumericalValue();
	}

	protected abstract int NormalizeNumericalValue (int numericalValue);

	private int GetNumericalValue ()
	{
		if (NumberRegexHelper.TryGetNumberFromString(RawValue, out int numericalValue) == true)
		{
			return NormalizeNumericalValue(numericalValue);
		}
		
		return 0;
	}
}