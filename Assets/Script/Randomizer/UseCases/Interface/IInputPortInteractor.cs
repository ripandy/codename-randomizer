using System;

namespace Randomizer.UseCases
{
    public interface IInputPortInteractor
    {
        Action InputHandler { get; }
    }
    
    public interface IInputPortInteractor<in T> : IInputPortInteractor
    {
        new Action<T> InputHandler { get; }
    }
}