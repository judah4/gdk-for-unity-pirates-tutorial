using System.Collections;
using System.Collections.Generic;
using Improbable.Core;
using Improbable.Gdk.Subscriptions;
using Improbable.Worker.CInterop;
using UnityEngine;

namespace Assets.Gamelogic.Pirates.Behaviours
{

    public class ShipTransformReceiver : TransformReceiver
    {
        [Require] protected ClientAuthorityCheckReader _clientAuthorityCheck;

        protected override void OnRun()
        {
            if (_clientAuthorityCheck.Authority == Authority.Authoritative)
                enabled = false;
        }

    }

}