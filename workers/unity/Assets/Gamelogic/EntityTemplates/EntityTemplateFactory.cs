using Assets.Gamelogic.Core;
using Improbable.Core;
using Improbable.Ship;
using Random = UnityEngine.Random; // Used in lesson 2
using Improbable;
using Improbable.Gdk.Core;
using Improbable.Gdk.PlayerLifecycle;
using Improbable.PlayerLifecycle;
using Playground;
// Used in lesson 2
using UnityEngine;

namespace Assets.Gamelogic.EntityTemplates
{
    // Factory class with static methods used to define templates for every created entity.
    public static class EntityTemplateFactory
    {

        // Defines the template for the PlayerShip entity.
        public static EntityTemplate CreatePlayerShipTemplate(string workerId, Improbable.Vector3f position)
        {
            var clientAttribute = $"workerId:{workerId}";


            //set position to random for now
            position = new Vector3f(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));


            var playerEntityTemplate = EntityBuilder.Begin()
              // Add components to the entity, then set the access permissions for the component on the entity relative to the client or server worker ids.
              .AddPosition(position.X, position.Y, position.Z, clientAttribute)
              .AddMetadata(SimulationSettings.PlayerShipPrefabName, WorkerUtils.UnityGameLogic)
              .SetPersistence(false)
              .SetReadAcl(WorkerUtils.AllWorkerAttributes)
              .AddComponent(Rotation.Component.CreateSchemaComponentData(0), clientAttribute)
              .AddComponent(ShipControls.Component.CreateSchemaComponentData(0, 0), clientAttribute)
              .AddComponent(ClientAuthorityCheck.Component.CreateSchemaComponentData(), clientAttribute)
              .AddPlayerLifecycleComponents(workerId, clientAttribute, WorkerUtils.UnityGameLogic)

            ;

            return playerEntityTemplate.Build();
        }

        // Defines the template for the PlayerCreator entity.
        public static EntityTemplate CreatePlayerCreatorTemplate()
        {
            var playerCreatorEntityTemplate = EntityBuilder.Begin()
              // Add components to the entity, then set the access permissions for the component on the entity relative to the client or server worker ids.
              .AddPosition(-5, 0, 0, WorkerUtils.UnityGameLogic)
              .AddMetadata(SimulationSettings.PlayerCreatorPrefabName, WorkerUtils.UnityGameLogic)
              .SetPersistence(true)
              .SetReadAcl(WorkerUtils.AllWorkerAttributes)
              .AddComponent(PlayerCreator.Component.CreateSchemaComponentData(), WorkerUtils.UnityGameLogic)
              .Build();

            return playerCreatorEntityTemplate;
        }

        // Template definition for a Island snapshot entity
        static public EntityTemplate GenerateIslandEntityTemplate(Vector3 initialPosition, string prefabName)
        {
            var islandEntityTemplate = EntityBuilder.Begin()
              // Add components to the entity, then set the access permissions for the component on the entity relative to the client or server worker ids.
              .AddPosition(initialPosition.x, initialPosition.y, initialPosition.z, WorkerUtils.UnityGameLogic)
              .AddMetadata(prefabName, WorkerUtils.UnityGameLogic)
              .SetPersistence(true)
              .SetReadAcl(WorkerUtils.AllWorkerAttributes)
              .AddComponent(Rotation.Component.CreateSchemaComponentData(0), WorkerUtils.UnityGameLogic)
              .Build();

            return islandEntityTemplate;
        }

        // Template definition for a SmallFish snapshot entity
        static public EntityTemplate GenerateSmallFishTemplate(Vector3 initialPosition)
        {
            var smallFishTemplate = EntityBuilder.Begin()
              // Add components to the entity, then set the access permissions for the component on the entity relative to the client or server worker ids.
              .AddPosition(initialPosition.x, initialPosition.y, initialPosition.z, WorkerUtils.UnityGameLogic)
              .AddMetadata(SimulationSettings.SmallFishPrefabName, WorkerUtils.UnityGameLogic)
              .SetPersistence(true)
              .SetReadAcl(WorkerUtils.AllWorkerAttributes)
              .AddComponent(Rotation.Component.CreateSchemaComponentData(0), WorkerUtils.UnityGameLogic)
              .Build();

            return smallFishTemplate;
        }

        // Template definition for a LargeFish snapshot entity
        static public EntityTemplate GenerateLargeFishTemplate(Vector3 initialPosition)
        {
            var largeFishTemplate = EntityBuilder.Begin()
              // Add components to the entity, then set the access permissions for the component on the entity relative to the client or server worker ids.
              .AddPosition(initialPosition.x, initialPosition.y, initialPosition.z, WorkerUtils.UnityGameLogic)
              .AddMetadata(SimulationSettings.LargeFishPrefabName, WorkerUtils.UnityGameLogic)
              .SetPersistence(true)
              .SetReadAcl(WorkerUtils.AllWorkerAttributes)
              .AddComponent(Rotation.Component.CreateSchemaComponentData(0), WorkerUtils.UnityGameLogic)
              .Build();
            
            return largeFishTemplate;
        }
    }
}
