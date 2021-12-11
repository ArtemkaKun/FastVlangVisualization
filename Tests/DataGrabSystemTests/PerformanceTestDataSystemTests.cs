using FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem.PerformanceMeasureUnitSystem;
using NUnit.Framework;

namespace Tests.DataGrabSystemTests;

public class PerformanceTestDataSystemTests
{
	[Test]
	public void CreateTimePerformanceUnit_GetNumericalValueReturnsExpectedValue_True ()
	{
		int testNumber = 256;
		string testRawValue = $"{testNumber} ms"; //TODO to const
		IPerformanceMeasureUnit testPerformanceUnit = new TimePerformanceMeasureUnit(testRawValue);
		
		Assert.IsTrue(testNumber == testPerformanceUnit.NumericalValue);
	}
	
	[Test]
	public void CreateMemoryPerformanceUnit_GetNumericalValueReturnsExpectedValue_True ()
	{
		int testNumber = 256;
		string testRawValue = $"{testNumber} KB"; //TODO to const
		IPerformanceMeasureUnit testPerformanceUnit = new MemoryPerformanceMeasureUnit(testRawValue);
		
		Assert.IsTrue(testNumber == testPerformanceUnit.NumericalValue);
	}
	
	[Test]
	public void CreateLinesPerformanceUnit_GetNumericalValueReturnsExpectedValue_True ()
	{
		int testNumber = 256;
		string testRawValue = $"{testNumber}"; //TODO to const
		IPerformanceMeasureUnit testPerformanceUnit = new LinesPerformanceMeasureUnit(testRawValue);
		
		Assert.IsTrue(testNumber == testPerformanceUnit.NumericalValue);
	}
}