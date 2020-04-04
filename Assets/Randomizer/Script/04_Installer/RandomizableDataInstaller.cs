using Randomizer.ExternalFrameworks.DataStores;
using UnityEngine;
using Zenject;

namespace Randomzer.Installer
{
    [CreateAssetMenu(fileName = "RandomizableDataInstaller", menuName = "Installers/RandomizableDataInstaller")]
    public class RandomizableDataInstaller : ScriptableObjectInstaller<RandomizableDataInstaller>
    {
        [SerializeField] private RandomizableDataStore dataStore;

        public override void InstallBindings()
        {
            dataStore.LoadFromJson();
            Container.BindInterfacesTo<RandomizableDataStore>().FromInstance(dataStore);
        }
    }
}