using System.Numerics;

namespace Randomizer.InterfaceAdapters
{
    public interface IView
    {
        bool Visible { get; set; }
        Vector2 Position { get; set; }
    }
}