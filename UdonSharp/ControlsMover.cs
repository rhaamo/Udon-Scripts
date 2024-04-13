
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace OtterHaven {
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ControlsMover : UdonSharpBehaviour {
        [Header("Controls")]
        public GameObject ControlsGroup;
        public Transform InitialPosition;
        public Transform HousePosition;
        public Image btnShowHouse;
        public Image btnHideHouse;


        [Header("State colors")]
        public Color32 buttonOn = new Color(15/255f, 132/255f, 12/255f, 255/255f);
        public Color32 buttonOffGrey = new Color(89/255f, 89/255f, 89/255f, 255/255f);
        public Color32 buttonOffRed = new Color(135/255f, 0/255f, 0/255f, 255/255f);

        bool isAtInitialPosition = true;

        void Start() {
        }

        public void _spawnAtHouse() {
            ControlsGroup.transform.position = HousePosition.position;
            isAtInitialPosition = false;
            btnShowHouse.color = buttonOn;
            btnHideHouse.color = buttonOffGrey;
        }

        public void _spawnAtInitialPosition() {
            ControlsGroup.transform.position = InitialPosition.position;
            isAtInitialPosition = true;
            btnShowHouse.color = buttonOffGrey;
            btnHideHouse.color = buttonOn;
        }
    }
}
