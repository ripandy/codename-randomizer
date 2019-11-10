using System;

namespace Randomizer.InterfaceAdapters
{
    public interface IActionHandler<out T>
    {
        void Subscribe(Action<T> onAction);
    }
}