using Assets.Gamelogic.Core;
using UnityEngine;

namespace Assets.Gamelogic.Pirates.Behaviours
{
    // Add this MonoBehaviour on UnityWorker (server-side) workers only
    public class TakeDamage : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other != null && other.gameObject.tag == SimulationSettings.CannonballTag)
            {
            }
        }
    }
}