
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;

namespace OtterHaven {
    [UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
    public class RespawnObject : UdonSharpBehaviour {
        public VRCObjectSync theObject;

        public void respawnObject() {
            theObject.Respawn();
        }

        public override void Interact() {
            this.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "respawnObject");
        }
    }
}