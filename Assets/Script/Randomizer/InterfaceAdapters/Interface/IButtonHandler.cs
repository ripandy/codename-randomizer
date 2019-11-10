using System;

namespace Randomizer.InterfaceAdapters
{
    public interface IButtonHandler
    {
        void Subscribe(Action onPress);
    }
}