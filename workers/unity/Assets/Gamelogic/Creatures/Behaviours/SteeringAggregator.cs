using System.Collections.Generic;
using Assets.Gamelogic.Core;
using Improbable;
using Improbable.Core;
using Improbable.Gdk.GameObjectRepresentation;
using UnityEngine;

namespace Assets.Gamelogic.Pirates.Behaviours.Creatures
{
    // Enable this MonoBehaviour on UnityWorker (server-side) workers only
    public class SteeringAggregator : MonoBehaviour
    {
        /*
         * An entity with this MonoBehaviour will only be enabled for the single UnityWorker worker
         * which has write-access for its Position component.
         */
        [Require]
        private Position.Requirable.Writer PositionWriter;
        [Require]
        private Rotation.Requirable.Writer RotationWriter;

        [SerializeField] private SpatialOSComponent _spatialOsComponent;

        private Vector3 desiredHeading;
        private List<SteeringInfluence> steeringInfluences = new List<SteeringInfluence>();

        protected void OnEnable()
        {
            _spatialOsComponent = GetComponent<SpatialOSComponent>();
            desiredHeading = transform.forward;
            // Initialize entity's gameobject transform from Position component values
            transform.position = PositionWriter.Data.Coords.ToUnityVector() + _spatialOsComponent.Worker.Origin;
            transform.rotation = Quaternion.Euler(0.0f, RotationWriter.Data.Rotation, 0.0f);
        }

        public void AddSteeringInfluence(SteeringInfluence steeringInfluence)
        {
            steeringInfluences.Add(steeringInfluence);
        }

        public void RemoveSteeringInfluence(SteeringInfluence steeringInfluence)
        {
            steeringInfluences.Remove(steeringInfluence);
        }

        private void FixedUpdate()
        {
            InfluenceDesiredHeading();
            // Rotate gameobject around y-axis towards desired heading
            var newHeading = Vector3.RotateTowards(transform.forward, desiredHeading, SimulationSettings.MaxSteeringTurnAngle * Mathf.Deg2Rad, Time.deltaTime);
            var turnAngle = Vector3.Angle(transform.forward, newHeading);
            var sign = Mathf.Sign(Vector3.Dot(newHeading, transform.right));
            turnAngle *= sign;
            transform.Rotate(0, turnAngle, 0);
            // Move the creature forwards
            transform.Translate(transform.forward * SimulationSettings.SteeringMovementSpeed * Time.deltaTime, Space.World);

            // Synchronise transform across all workers
            PositionWriter.Send(new Position.Update() { Coords = (transform.position - _spatialOsComponent.Worker.Origin).ToCoordinates()});
            RotationWriter.Send(new Rotation.Update() { Rotation = ((uint)transform.rotation.eulerAngles.y)});
        }

        private void InfluenceDesiredHeading()
        {
            // Aggregate steering influences
            var influencedHeading = Vector3.zero;
            foreach (var steeringInfluence in steeringInfluences)
            {
                // Scale each contribution by its intended strength
                influencedHeading += steeringInfluence.GetInfluenceHeading() * steeringInfluence.GetInfluenceStrength();
            }
            desiredHeading = Vector3.Normalize(influencedHeading);
        }
    }
}
