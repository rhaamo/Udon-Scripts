
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class MirrorToggler : UdonSharpBehaviour {

    [Header("Global")]
    public string mirrorName = "unnamed mirror";
    public Image buttonOff;
    [Header("LQ")]
    public GameObject MirrorLQ;
    public Image buttonLQ;
    [Header("HQ")]
    public GameObject MirrorHQ;
    public Image buttonHQ;
    [Header("Transparent")]
    public GameObject MirrorTransparent;
    public Image buttonTransparent;

    [Header("State colors")]
    public Color32 buttonOn = new Color(15/255f, 132/255f, 12/255f, 255/255f);
    public Color32 buttonOffGrey = new Color(89/255f, 89/255f, 89/255f, 255/255f);
    public Color32 buttonOffRed = new Color(135/255f, 0/255f, 0/255f, 255/255f);

    void Start() {
        Debug.Log("[OTR_MIRROR] Setting " + mirrorName + " to Off by default.");
        _allOff();
    }

    public void _toggleMirror(GameObject mirror, bool state) {
        if (Utilities.IsValid(mirror)) {
            mirror.SetActive(state);
        }
    }

    public void _toggleButton(Image btn, Color32 color) {
        if (Utilities.IsValid(btn)) {
            btn.color = color;
        }
    }

    public void _showLQ() {
        Debug.Log("[OTR_MIRROR] Toggle " + mirrorName + " to LQ.");
        _toggleMirror(MirrorHQ, false);
        _toggleMirror(MirrorLQ, true);
        _toggleMirror(MirrorTransparent, false);

        _toggleButton(buttonOff, buttonOffGrey);
        _toggleButton(buttonLQ, buttonOn);
        _toggleButton(buttonHQ, buttonOffGrey);
        _toggleButton(buttonTransparent, buttonOffGrey);
    }

    public void _showHQ() {
        Debug.Log("[OTR_MIRROR] Toggle " + mirrorName + " to HQ.");
        _toggleMirror(MirrorLQ, false);
        _toggleMirror(MirrorHQ, true);
        _toggleMirror(MirrorTransparent, false);

        _toggleButton(buttonOff, buttonOffGrey);
        _toggleButton(buttonLQ, buttonOffGrey);
        _toggleButton(buttonHQ, buttonOn);
        _toggleButton(buttonTransparent, buttonOffGrey);
    }

    public void _showTransparent() {
        Debug.Log("[OTR_MIRROR] Toggle " + mirrorName + " to Transparent.");
        _toggleMirror(MirrorLQ, false);
        _toggleMirror(MirrorHQ, false);
        _toggleMirror(MirrorTransparent, true);

        _toggleButton(buttonOff, buttonOffGrey);
        _toggleButton(buttonLQ, buttonOffGrey);
        _toggleButton(buttonHQ, buttonOffGrey);
        _toggleButton(buttonTransparent, buttonOn);
    }

    public void _allOff() {
        Debug.Log("[OTR_MIRROR] Toggle all " + mirrorName + " off.");
        _toggleMirror(MirrorLQ, false);
        _toggleMirror(MirrorHQ, false);
        _toggleMirror(MirrorTransparent, false);

        _toggleButton(buttonOff, buttonOn);
        _toggleButton(buttonLQ, buttonOffGrey);
        _toggleButton(buttonHQ, buttonOffGrey);
        _toggleButton(buttonTransparent, buttonOffGrey);
    }
}
