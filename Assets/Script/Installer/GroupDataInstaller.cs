using Randomizer.ExternalFrameworks.DataStores;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GroupDataInstaller", menuName = "Installers/GroupDataInstaller")]
public class GroupDataInstaller : ScriptableObjectInstaller<GroupDataInstaller>
{
    [SerializeField] private GroupDataStore dataStore;
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<GroupDataStore>().FromInstance(dataStore);
    }
}