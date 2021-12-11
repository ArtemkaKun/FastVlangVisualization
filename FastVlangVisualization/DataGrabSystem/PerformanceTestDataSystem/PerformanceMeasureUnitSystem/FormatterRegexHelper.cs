using System.Text.RegularExpressions;

namespace FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;

public static class FormatterRegexHelper
{
	private static Regex RegexInstance { get; } = new("[^0-9].*");

	public static string GetFormatString (string stringWithFormat)
	{
		return RegexInstance.Match(stringWithFormat).Value;
	}
}