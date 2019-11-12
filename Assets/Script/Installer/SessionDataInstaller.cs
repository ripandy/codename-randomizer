using Randomizer.ExternalFrameworks;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SessionDataInstaller", menuName = "Installers/SessionDataInstaller")]
public class SessionDataInstaller : ScriptableObjectInstaller<SessionDataInstaller>
{
    [SerializeField] private SessionDataStore dataStore;
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<SessionDataStore>().FromInstance(dataStore);
    }    
}