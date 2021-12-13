namespace FastVlangVisualization.RangeBookmarksSystem;

public interface ITestRelatedRangeBookmark : IRangeBookmark
{
	string RelatedTestName { get; }
}