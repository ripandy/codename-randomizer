using System;

namespace Randomizer.UseCases
{
    public interface IOutputPortInteractor
    {
    }

    public interface IOutputPortInteractor<T> : IOutputPortInteractor
    {
        Action<T> OutputHandler { get; set; }
    }
}