using Assets.Gamelogic.Core;
using UnityEngine;

namespace Assets.Gamelogic.Pirates.Behaviours.Creatures
{
    // Enable this MonoBehaviour on UnityWorker (server-side) workers only
    public abstract class SteeringInfluence : MonoBehaviour
    {
        protected static SteeringAggregator SteeringInfluenceAggregator;
        [SerializeField]
        protected float InfluenceStrength;

        public void OnEnable()
        {
            InfluenceStrength = SimulationSettings.DefaultInfluenceStrength;
            SteeringInfluenceAggregator = gameObject.GetComponent<SteeringAggregator>();
            if (SteeringInfluenceAggregator != null)
            {
                SteeringInfluenceAggregator.AddSteeringInfluence(this);
            }
        }

        public void OnDisable()
        {
            if (SteeringInfluenceAggregator != null)
            {
                SteeringInfluenceAggregator.RemoveSteeringInfluence(this);
            }
        }

        virtual public Vector3 GetInfluenceHeading()
        {
            return gameObject.transform.forward;
        }

        public float GetInfluenceStrength()
        {
            return InfluenceStrength;
        }
    }
}