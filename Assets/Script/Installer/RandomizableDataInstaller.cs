using Randomizer.ExternalFrameworks;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "RandomizableDataInstaller", menuName = "Installers/RandomizableDataInstaller")]
public class RandomizableDataInstaller : ScriptableObjectInstaller<RandomizableDataInstaller>
{
    [SerializeField] private RandomizableDataStore dataStore;
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<RandomizableDataStore>().FromInstance(dataStore);
    }
}