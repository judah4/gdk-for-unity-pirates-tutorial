using Improbable;
using Improbable.Core;
using Improbable.Gdk.GameObjectRepresentation;
using UnityEngine;
using Improbable.Worker.CInterop;

namespace Assets.Gamelogic.Pirates.Behaviours
{
    // Add this MonoBehaviour on both client and server-side workers
    public class TransformReceiver : MonoBehaviour
    {
        // Inject access to the entity's Position and Rotation components
        [Require] private Position.Requirable.Reader PositionReader;
        [Require] private Rotation.Requirable.Reader RotationReader;

        [SerializeField] private SpatialOSComponent _spatialOsComponent;

        void OnEnable()
        {
            _spatialOsComponent = GetComponent<SpatialOSComponent>();

            // Initialize entity's gameobject transform from Position and Rotation component values
            transform.position = PositionReader.Data.Coords.ToUnityVector() + _spatialOsComponent.Worker.Origin;
            transform.rotation = Quaternion.Euler(0.0f, RotationReader.Data.Rotation, 0.0f);

            // Register callback for when component changes
            PositionReader.ComponentUpdated+=(OnPositionUpdated);
            RotationReader.ComponentUpdated += (OnRotationUpdated);
        }

        void OnDisable()
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

        // Callback for whenever one or more property of the Rotation component is updated
        void OnRotationUpdated(Rotation.Update update)
        {
            /*
             * Only update the transform if this component is on a worker which isn't authorative over the
             * entity's Rotation component.
             * This synchronises the entity's local representation on the worker with that of the entity on
             * whichever worker is authoritative over its Rotation and is responsible for its movement.
             */
			if (RotationReader.Authority == Authority.NotAuthoritative)
            {
                if (update.Rotation.HasValue)
                {
                    transform.rotation = Quaternion.Euler(0.0f, update.Rotation.Value, 0.0f);
                }
            }
        }
    }
}
