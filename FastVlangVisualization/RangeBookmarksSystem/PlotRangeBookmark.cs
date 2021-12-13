namespace FastVlangVisualization.RangeBookmarksSystem;

public record PlotRangeBookmark : IRangeBookmark
{
	public string Name { get; init; }
	public (DateTime rangeStart, DateTime rangeEnd) Range { get; init; }
}