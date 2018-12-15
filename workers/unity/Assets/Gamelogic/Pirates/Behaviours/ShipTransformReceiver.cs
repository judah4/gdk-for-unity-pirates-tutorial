using System.Collections;
using System.Collections.Generic;
using Improbable.Core;
using Improbable.Gdk.GameObjectRepresentation;
using Improbable.Worker.CInterop;
using UnityEngine;

namespace Assets.Gamelogic.Pirates.Behaviours
{

    public class ShipTransformReceiver : TransformReceiver
    {
        [Require] protected ClientAuthorityCheck.Requirable.Reader _clientAuthorityCheck;

        protected override void OnRun()
        {
            if (_clientAuthorityCheck.Authority == Authority.Authoritative)
                enabled = false;
        }

    }

}