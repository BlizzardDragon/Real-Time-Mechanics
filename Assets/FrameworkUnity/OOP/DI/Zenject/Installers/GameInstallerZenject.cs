namespace FrameworkUnity.OOP.Zenject.Installers
{
    public class GameInstallerZenject : BaseGameSystemsInstaller
    {
        protected override void InstallGameSystems()
        {
            Container.Bind<CurrencyStorage>().AsSingle();
            Container.Bind<AudioManager>().FromComponentsInHierarchy().AsSingle();
            
            Container.BindInterfacesTo<RealtimeSaveLoader>().AsSingle();
            Container.Bind<Chest>().FromComponentsInHierarchy().AsCached();
            
            Container.BindInterfacesTo<CurrencyStoragePresenter>().AsSingle();
            Container.Bind<ViewCurrency>().FromComponentsInHierarchy().AsCached();
            
            Container.Bind<RewardFactory>().AsSingle();
        }
    }
}