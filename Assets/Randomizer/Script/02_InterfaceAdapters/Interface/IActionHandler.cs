using System;

namespace Randomizer.InterfaceAdapters
{
    public interface IActionHandler
    {
        Action OnAction { set; }
        bool Active { get; set; }
    }

    public interface IActionHandler<out T> : IActionHandler
    {
        T Value { get; }
    }
}