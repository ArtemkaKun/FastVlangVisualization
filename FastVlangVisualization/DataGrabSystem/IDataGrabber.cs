namespace FastVlangVisualization.DataGrabSystem;

public interface IDataGrabber
{
	Task<List<IVlangSpeedData>> GetVlangSpeedDataAsync ();
}