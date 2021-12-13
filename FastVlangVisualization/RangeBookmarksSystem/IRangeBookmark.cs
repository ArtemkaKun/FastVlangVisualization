namespace FastVlangVisualization.RangeBookmarksSystem;

public interface IRangeBookmark
{
	string Name { get; }
	(DateTime rangeStart, DateTime rangeEnd) Range { get; }
}