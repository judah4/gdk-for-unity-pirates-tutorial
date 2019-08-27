using Assets.Gamelogic.Core;
using Improbable;
using Improbable.Core;
using Improbable.Ship;
using Random = UnityEngine.Random; // Used in lesson 2
using Improbable.Gdk.Core;
using Improbable.Gdk.PlayerLifecycle;
using Improbable.Gdk.TransformSynchronization;
using Playground;
// Used in lesson 2
using UnityEngine;

namespace Assets.Gamelogic.EntityTemplates
{
    // Factory class with static methods used to define templates for every created entity.
    public static class EntityTemplateFactory
    {

        // Defines the template for the PlayerShip entity.
        public static EntityTemplate CreatePlayerShipTemplate(string clientWorkerId,
            byte[] serializedArguments)
        {
            var clientAttribute = EntityTemplate.GetWorkerAccessAttribute(clientWorkerId);// CommonRequirementSets.SpecificClientOnly(_clientWorkerId);


            //set position to random for now
            var position = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));


            var template = new EntityTemplate();
            template.AddComponent(new Position.Snapshot() { Coords = position.ToCoordinates() }, WorkerUtils.UnityGameLogic);
            template.AddComponent(new Metadata.Snapshot() { EntityType = SimulationSettings.PlayerShipPrefabName }, WorkerUtils.UnityGameLogic);
            //template.AddComponent(new Persistence.Snapshot(), WorkerUtils.UnityGameLogic);
            template.AddComponent(new ShipControls.Snapshot(), clientAttribute);
            template.AddComponent(new ClientAuthorityCheck.Snapshot(), clientAttribute);

            PlayerLifecycleHelper.AddPlayerLifecycleComponents(template, clientWorkerId, WorkerUtils.UnityGameLogic);
            TransformSynchronizationHelper.AddTransformSynchronizationComponents(template, WorkerUtils.UnityGameLogic, location: position);


            template.SetReadAccess(WorkerUtils.AllWorkerAttributes);
            template.SetComponentWriteAccess(EntityAcl.ComponentId, WorkerUtils.UnityGameLogic);


            return template;
        }

        // Defines the template for the PlayerCreator entity.
        public static EntityTemplate CreatePlayerCreatorTemplate()
        {
            var pos = new Coordinates(-5, 0, 0);

            var template = new EntityTemplate();
            template.AddComponent(new Position.Snapshot() { Coords = pos }, WorkerUtils.UnityGameLogic);
            template.AddComponent(new Metadata.Snapshot() { EntityType = SimulationSettings.PlayerCreatorPrefabName }, WorkerUtils.UnityGameLogic);
            template.AddComponent(new Persistence.Snapshot(), WorkerUtils.UnityGameLogic);
            template.AddComponent(new PlayerCreator.Snapshot(), WorkerUtils.UnityGameLogic);

            TransformSynchronizationHelper.AddTransformSynchronizationComponents(template, WorkerUtils.UnityGameLogic, location: pos.ToUnityVector());


            template.SetReadAccess(WorkerUtils.AllWorkerAttributes);
            template.SetComponentWriteAccess(EntityAcl.ComponentId, WorkerUtils.UnityGameLogic);

            return template;
        }

        // Template definition for a Island snapshot entity
        static public EntityTemplate GenerateIslandEntityTemplate(Vector3 initialPosition, string prefabName)
        {
            var template = new EntityTemplate();
            template.AddComponent(new Position.Snapshot() { Coords = initialPosition.ToCoordinates() }, WorkerUtils.UnityGameLogic);
            template.AddComponent(new Metadata.Snapshot() { EntityType = prefabName }, WorkerUtils.UnityGameLogic);
            template.AddComponent(new Persistence.Snapshot(), WorkerUtils.UnityGameLogic);

            TransformSynchronizationHelper.AddTransformSynchronizationComponents(template, WorkerUtils.UnityGameLogic, location: initialPosition);


            template.SetReadAccess(WorkerUtils.AllWorkerAttributes);
            template.SetComponentWriteAccess(EntityAcl.ComponentId, WorkerUtils.UnityGameLogic);


            return template;
        }

        // Template definition for a SmallFish snapshot entity
        static public EntityTemplate GenerateSmallFishTemplate(Vector3 initialPosition)
        {
            var template = new EntityTemplate();
            template.AddComponent(new Position.Snapshot() { Coords = initialPosition.ToCoordinates() }, WorkerUtils.UnityGameLogic);
            template.AddComponent(new Metadata.Snapshot() { EntityType = SimulationSettings.SmallFishPrefabName }, WorkerUtils.UnityGameLogic);
            template.AddComponent(new Persistence.Snapshot(), WorkerUtils.UnityGameLogic);

            TransformSynchronizationHelper.AddTransformSynchronizationComponents(template, WorkerUtils.UnityGameLogic, location: initialPosition);


            template.SetReadAccess(WorkerUtils.AllWorkerAttributes);
            template.SetComponentWriteAccess(EntityAcl.ComponentId, WorkerUtils.UnityGameLogic);


            return template;

        }

        // Template definition for a LargeFish snapshot entity
        static public EntityTemplate GenerateLargeFishTemplate(Vector3 initialPosition)
        {
            var template = new EntityTemplate();
            template.AddComponent(new Position.Snapshot() { Coords = initialPosition.ToCoordinates() }, WorkerUtils.UnityGameLogic);
            template.AddComponent(new Metadata.Snapshot() { EntityType = SimulationSettings.LargeFishPrefabName }, WorkerUtils.UnityGameLogic);
            template.AddComponent(new Persistence.Snapshot(), WorkerUtils.UnityGameLogic);

            TransformSynchronizationHelper.AddTransformSynchronizationComponents(template, WorkerUtils.UnityGameLogic, location: initialPosition);


            template.SetReadAccess(WorkerUtils.AllWorkerAttributes);
            template.SetComponentWriteAccess(EntityAcl.ComponentId, WorkerUtils.UnityGameLogic);


            return template;

        }
    }
}
