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
    
    public interface IInputPortInteractor<in TParam1, in TParam2> : IInputPortInteractor
    {
        new Action<TParam1, TParam2> InputHandler { get; }
    }
}