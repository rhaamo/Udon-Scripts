
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
using ArchiTech.ProTV;

namespace OtterHaven {
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class TVSwitcher : UdonSharpBehaviour {
        [Header("Local TV")]
        public GameObject[] localTvElements;
        public TVManager localTvManager;
        public Image buttonLocal;

        [Header("Global TV")]
        public GameObject[] globalTVElements;
        public TVManager globalTvManager;
        public Image buttonGlobal;

        [Header("State colors")]
        public Color32 buttonOn = new Color(15/255f, 132/255f, 12/255f, 255/255f);
        public Color32 buttonOffGrey = new Color(89/255f, 89/255f, 89/255f, 255/255f);
        public Color32 buttonOffRed = new Color(135/255f, 0/255f, 0/255f, 255/255f);

        void Start() {
            localTvManager._Mute(); // mute the player by default
        }

        public void _showLocalTv() {
            Debug.Log("[OTR_TV_SWITCHER] Switch to Local TV.");
            globalTvManager._ChangeVolume(0.0f);
            localTvManager._ChangeVolume(0.3f);
            foreach (GameObject screen in globalTVElements) {
                screen.SetActive(false);
            }
            foreach (GameObject screen in localTvElements) {
                screen.SetActive(true);
            }
            buttonLocal.color = buttonOn;
            buttonGlobal.color = buttonOffGrey;
        }

        public void _showGlobalTv() {
            Debug.Log("[OTR_TV_SWITCHER] Switch to Global TV.");
            localTvManager._ChangeVolume(0.0f);
            globalTvManager._ChangeVolume(0.3f);
            foreach (GameObject screen in localTvElements) {
                screen.SetActive(false);
            }
            foreach (GameObject screen in globalTVElements) {
                screen.SetActive(true);
            }
            buttonLocal.color = buttonOffGrey;
            buttonGlobal.color = buttonOn;
        }
    }
}