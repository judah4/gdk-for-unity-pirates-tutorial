using Improbable.Gdk.Core;
using Improbable.Gdk.PlayerLifecycle;
using Unity.Entities;
using UnityEngine;
using Assets.Gamelogic.EntityTemplates;

namespace Playground
{
    public static class OneTimeInitialisation
    {
        private static bool initialized;

        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            if (initialized)
            {
                return;
            }

            initialized = true;
            WorldsInitializationHelper.SetupInjectionHooks();
            PlayerLoopManager.RegisterDomainUnload(WorldsInitializationHelper.DomainUnloadShutdown, 1000);

            // Setup template to use for player on connecting client
            PlayerLifecycleConfig.CreatePlayerEntityTemplate = EntityTemplateFactory.CreatePlayerShipTemplate;
        }
    }
}
