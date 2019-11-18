using System.Collections.Generic;
using Randomizer.InterfaceAdapters;

namespace Randomizer.ExternalFrameworks.Handlers
{
    public class ZenjectInitializers : Zenject.IInitializable
    {
        private readonly IList<IInitializable> _initializables;

        private ZenjectInitializers(IList<IInitializable> initializables)
        {
            _initializables = initializables;
        }

        public void Initialize()
        {
            foreach (var initializable in _initializables)
            {
                initializable.Initialize();
            }
        }
    }
}