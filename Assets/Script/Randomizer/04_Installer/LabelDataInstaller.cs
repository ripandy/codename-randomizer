using Randomizer.ExternalFrameworks.DataStores;
using UnityEngine;
using Zenject;

namespace Randomzer.Installer
{
    [CreateAssetMenu(fileName = "LabelDataInstaller", menuName = "Installers/LabelDataInstaller")]
    public class LabelDataInstaller : ScriptableObjectInstaller<LabelDataInstaller>
    {
        [SerializeField] private LabelDataStore dataStore;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<LabelDataStore>().FromInstance(dataStore);
        }
    }
}