namespace Randomizer.InterfaceAdapters
{
    public interface IViewContainer
    {
        ContentType Type { get; set; }
        ContentAnchor Anchor { get; set; }
    }
}