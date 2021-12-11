using System.Text.RegularExpressions;

namespace FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;

public class TimePerformanceMeasureUnit : BasePerformanceMeasureUnit
{
	public TimePerformanceMeasureUnit (string rawValue) : base(rawValue) { }
	
	protected override int ConvertRawValueToNumerical ()
	{
		if (RawValue.Contains("ms", StringComparison.OrdinalIgnoreCase) == true)
		{
			return int.Parse(new Regex("[0-9]*").Match(RawValue).Value);
		}
		
		return 0;
	}
}