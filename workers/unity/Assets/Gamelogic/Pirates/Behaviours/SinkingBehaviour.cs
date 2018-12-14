using UnityEngine;

namespace Assets.Gamelogic.Pirates.Behaviours
{
    // Add this MonoBehaviour on client workers only
    public class SinkingBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Animation SinkingAnimation;

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }

        private void VisualiseSinking()
        {
            SinkingAnimation.Play();
        }
    }
}
