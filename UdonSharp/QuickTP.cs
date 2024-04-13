
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace OtterHaven {
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class QuickTP : UdonSharpBehaviour {
        [Header("TP Transforms")]
        public Transform spawnTransform;
        public Transform houseTransform;
        public Transform chillTransform;
        public Transform theaterTransform;
        public Transform kartsTransform;

        private void TeleportTo(Transform tf) {
            if (!Utilities.IsValid(tf)) {
                Debug.Log("[OTR_QUICK_TP] Invalid transform");
                return;
            }
            VRCPlayerApi lp = Networking.LocalPlayer;
            if (!Utilities.IsValid(lp)) {
                Debug.Log("[OTR_QUICK_TP] Invalid LocalPlayer");
                return;
            }
            lp.TeleportTo(tf.position, tf.rotation);
        }

        public void _tpToSpawn() {
            Debug.Log("[OTR_QUICK_TP] Teleporting to spawn");
            TeleportTo(spawnTransform);
        }

        public void _toToHouse() {
            Debug.Log("[OTR_QUICK_TP] Teleporting to house");
            TeleportTo(houseTransform);
        }

        public void _toToChill() {
            Debug.Log("[OTR_QUICK_TP] Teleporting to chill");
            TeleportTo(chillTransform);
        }

        public void _toToTheater() {
            Debug.Log("[OTR_QUICK_TP] Teleporting to theater");
            TeleportTo(theaterTransform);
        }
        
        public void _toToKarts() {
            Debug.Log("[OTR_QUICK_TP] Teleporting to karts");
            TeleportTo(kartsTransform);
        }
    }
}
