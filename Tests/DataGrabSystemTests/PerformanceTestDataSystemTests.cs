using FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;
using NUnit.Framework;

namespace Tests.DataGrabSystemTests;

public class PerformanceTestDataSystemTests
{
	[Test]
	public void CreateTimePerformanceUnit_GetNumericalValueReturnsExpectedValue_True ()
	{
		int testNumber = 256;
		string testRawValue = $"{testNumber} ms";
		IPerformanceMeasureUnit testPerformanceUnit = new TimePerformanceMeasureUnit(testRawValue);
		
		Assert.IsTrue(testNumber == testPerformanceUnit.NumericalValue);
	}
}