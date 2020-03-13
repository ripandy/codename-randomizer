using Randomizer.ExternalFrameworks.DataStores;
using UnityEngine;
using Zenject;

namespace Randomzer.Installer
{
    [CreateAssetMenu(fileName = "ItemDataInstaller", menuName = "Installers/ItemDataInstaller")]
    public class ItemDataInstaller : ScriptableObjectInstaller<ItemDataInstaller>
    {
        [SerializeField] private ItemDataStore dataStore;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ItemDataStore>().FromInstance(dataStore);
        }
    }
}