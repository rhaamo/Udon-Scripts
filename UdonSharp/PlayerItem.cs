
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

// Based on https://github.com/Varneon/UdonEssentials/tree/main/Assets/Varneon/Udon%20Prefabs/Essentials/Playerlist

namespace Varneon.UdonPrefabs.Essentials {
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class PlayerItem : UdonSharpBehaviour {
        [HideInInspector] public VRCPlayerApi vrcPlayer;
        public HiddenPlayerList hiddenPlayers;
        private bool localUserHasSuperRights = false;

        public void _Configure(VRCPlayerApi playerInput, bool hasSuperRights) {
            vrcPlayer = playerInput;
            localUserHasSuperRights = hasSuperRights;
        }

        public void _OnClick() {
            // If the player ID is in the hidden list, then disallow teleport
            if (!Utilities.IsValid(hiddenPlayers)) {
                Debug.Log("[OTR_PLAYERLIST] No Hidden player list was provided.");
            }
            
            if (hiddenPlayers._IsPlayerIdHidden(vrcPlayer.playerId) && !localUserHasSuperRights) {
                Debug.Log("[OTR_PLAYERLIST] Was asked to TP to user '" + vrcPlayer.displayName + "' but TP is forbidden");
                return;
            }

            // Not in the list, you can go
            Vector3 pos = vrcPlayer.GetPosition();
            Debug.Log("[OTR_PLAYERLIST] Was asked to TP to user '" + vrcPlayer.displayName + "' at x: " + pos.x + " y: " + pos.y + " z: " + pos.z);
            if (Utilities.IsValid(vrcPlayer)) {
                VRCPlayerApi lp = Networking.LocalPlayer;
                lp.TeleportTo(vrcPlayer.GetPosition(), vrcPlayer.GetRotation());
            } else {
                // Invalid player means the player list data is old :(
                Debug.Log("[OTR_PLAYERLIST] Cannot TP, vrcPlayer reported Invalid by VRC API");
            }
        }
    }
}