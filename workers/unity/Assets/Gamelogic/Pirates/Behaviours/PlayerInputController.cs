using Assets.Gamelogic.Core;
using Assets.Gamelogic.Pirates.Cannons;
using Improbable.Gdk.GameObjectRepresentation;
using Improbable.Ship;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Gamelogic.Pirates.Behaviours
{
    // Add this MonoBehaviour on client workers only
    public class PlayerInputController : MonoBehaviour
    {
        /* 
         * Client will only have write-access for their own designated PlayerShip entity's ShipControls component,
         * so this MonoBehaviour will be enabled on the client's designated PlayerShip GameObject only and not on
         * the GameObject of other players' ships.
         */
        [Require]
        private ShipControls.Requirable.Writer ShipControlsWriter;

        private CannonFirer cannonFirer;

        void OnEnable()
        {
            cannonFirer = GetComponent<CannonFirer>();
        }

        void Update()
        {
            ShipControlsWriter.Send(new ShipControls.Update() { 
                TargetSpeed = (Mathf.Clamp01(Input.GetAxis("Vertical"))),
                TargetSteering = (Input.GetAxis("Horizontal"))
                });

            if (Input.GetKeyDown(KeyCode.Q))
            {
                // Port broadside (Fire the left cannons)
                if (cannonFirer != null)
                {
                    cannonFirer.AttemptToFireCannons(-transform.right);
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                // Starboard broadside (Fire the right cannons)
                if (cannonFirer != null)
                {
                    cannonFirer.AttemptToFireCannons(transform.right);
                }

            }
        }
    }
}
