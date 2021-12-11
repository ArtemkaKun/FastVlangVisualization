using FastVlangVisualization.DataGrabSystem.PerformanceTestDataSystem;

namespace FastVlangVisualization.DataGrabSystem;

public interface IDataGrabber
{
	Task<List<IVlangPerformanceTestData>> GetVlangSpeedDataAsync ();
}