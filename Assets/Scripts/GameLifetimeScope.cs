using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace WorldOfDreams
{
    public class GameLifetimeScope : LifetimeScope
    {
        [Header("Scene References")]
        [SerializeField] private PlayerAnimatorRegistrator player;
        [SerializeField] private AnimationUIHandler animationUiHandler;
        [SerializeField] private ResourceBarView hpBar, manaBar;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<AnimationControlService>(Lifetime.Scoped);
            builder.RegisterComponent(player);
            builder.RegisterComponent(animationUiHandler);

            RegisterUI(builder);
        }

        private void RegisterUI(IContainerBuilder builder) 
        {
            ResourceService resourceService = new ResourceService();
            builder.RegisterInstance<ResourceService>(resourceService).AsSelf().AsImplementedInterfaces();

            hpBar.Bind(resourceService.HpStream);
            manaBar.Bind(resourceService.ManaStream);
        }
    }
}