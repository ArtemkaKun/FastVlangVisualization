using System.Text.RegularExpressions;

namespace FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;

public static class NumberRegexHelper
{
	private static Regex RegexInstance { get; } = new("[0-9]*");

	public static bool TryGetNumberFromString (string stringWithNumber, out int number)
	{
		number = 0;
		string regexMatchString = new Regex("[0-9]*").Match(stringWithNumber).Value;

		return int.TryParse(regexMatchString, out number) == true;
	}
}