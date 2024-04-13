using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace OtterHaven {
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class OttSendEventOnTrigger : UdonSharpBehaviour {
        public UdonBehaviour OnPlayerTriggerEnter_Event;
        public string OnPlayerTriggerEnter_EventName;
        public UdonBehaviour OnPlayerTriggerStay_Event;
        public string OnPlayerTriggerStay_EventName;
        public UdonBehaviour OnPlayerTriggerExit_Event;
        public string OnPlayerTriggerExit_EventName;

        public override void OnPlayerTriggerEnter(VRCPlayerApi player) {
            if (Networking.LocalPlayer.IsValid() && player.Equals(Networking.LocalPlayer) && OnPlayerTriggerEnter_Event && !OnPlayerTriggerEnter_EventName.Equals("")) {
                Debug.Log($"[OTR_SEND_EVENT_TRIGGER] Player '{player.displayName}' entered into '{gameObject.name}', sending event {OnPlayerTriggerEnter_EventName}");
                OnPlayerTriggerEnter_Event.SendCustomEvent(OnPlayerTriggerEnter_EventName);
            }
        }

        public override void OnPlayerTriggerStay(VRCPlayerApi player) {
            if (Networking.LocalPlayer.IsValid() && player.Equals(Networking.LocalPlayer) && OnPlayerTriggerStay_Event && !OnPlayerTriggerStay_EventName.Equals("")) {
                //Debug.Log($"[OTR_SEND_EVENT_TRIGGER] Player '{player.displayName}' stayed into '{gameObject.name}', sending event {OnPlayerTriggerStay_EventName}");
                OnPlayerTriggerStay_Event.SendCustomEvent(OnPlayerTriggerStay_EventName);
            }
        }

        public override void OnPlayerTriggerExit(VRCPlayerApi player) {
            if (Networking.LocalPlayer.IsValid() && player.Equals(Networking.LocalPlayer) && OnPlayerTriggerExit_Event && !OnPlayerTriggerExit_EventName.Equals("")) {
                Debug.Log($"[OTR_SEND_EVENT_TRIGGER] Player '{player.displayName}' left '{gameObject.name}', sending event {OnPlayerTriggerExit_EventName}");
                OnPlayerTriggerExit_Event.SendCustomEvent(OnPlayerTriggerExit_EventName);
            }
        }
    }
}
