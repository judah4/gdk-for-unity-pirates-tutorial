using Improbable;
using Improbable.Gdk.Subscriptions;
using UnityEngine;
using Improbable.Worker.CInterop;
using Unity.Transforms;
using Position = Improbable.Position;

namespace Assets.Gamelogic.Pirates.Behaviours
{
    // Add this MonoBehaviour on both client and server-side workers
    public class TransformReceiver : MonoBehaviour
    {
        // Inject access to the entity's Position and Rotation components
        [Require] protected PositionReader PositionReader;

        [SerializeField] private LinkedEntityComponent _spatialOsComponent;

        void OnEnable()
        {
            _spatialOsComponent = GetComponent<LinkedEntityComponent>();

            // Initialize entity's gameobject transform from Position and Rotation component values
            transform.position = PositionReader.Data.Coords.ToUnityVector() + _spatialOsComponent.Worker.Origin;

            // Register callback for when component changes
            PositionReader.OnUpdate+=(OnPositionUpdated);

            OnRun();

        }

        void OnDisable()
        {
        }

        protected virtual void OnRun()
        {

        }

        // Callback for whenever one or more property of the Position standard library component is updated
        void OnPositionUpdated(Position.Update update)
        {
            /*
             * Only update the transform if this component is on a worker which isn't authorative over the
             * entity's Position standard library component.
             * This synchronises the entity's local representation on the worker with that of the entity on
             * whichever worker is authoritative over its Position and is responsible for its movement.
             */
			if (PositionReader.Authority == Authority.NotAuthoritative)
            {
                if (update.Coords.HasValue)
                {
                    transform.position = update.Coords.Value.ToUnityVector() + _spatialOsComponent.Worker.Origin;
                }
            }
        }

    }
}
