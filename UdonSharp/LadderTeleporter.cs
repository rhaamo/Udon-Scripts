
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace OtterHaven {
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class LadderTeleporter : UdonSharpBehaviour {
        [Header("TP Transforms")]
        public Transform topTransform;
        public Transform bottomTransform;

        private void TeleportTo(Transform tf) {
            if (!Utilities.IsValid(tf)) {
                Debug.Log("[OTR_COMFY_TP] Invalid transform");
                return;
            }
            VRCPlayerApi lp = Networking.LocalPlayer;
            if (!Utilities.IsValid(lp)) {
                Debug.Log("[OTR_COMFY_TP] Invalid LocalPlayer");
                return;
            }
            lp.TeleportTo(tf.position, tf.rotation);
        }

        public void _tpToBottom() {
            Debug.Log("[OTR_COMFY_TP] Teleporting to bottom");
            TeleportTo(bottomTransform);
        }

        public void _tpToTop() {
            Debug.Log("[OTR_COMFY_TP] Teleporting to top");
            TeleportTo(topTransform);
        }
    }
}
