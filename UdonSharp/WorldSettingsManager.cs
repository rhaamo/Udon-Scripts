﻿
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace OtterHaven {
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class WorldSettingsManager : UdonSharpBehaviour {
        [Header("Objects")]
        [SerializeField] private bool pens = true;
        [SerializeField] private bool joinAlert = true;
        [SerializeField] private bool games = false;
        [SerializeField] private bool clocks = true;
        [SerializeField] private bool polaroids = true;
        [SerializeField] private bool potato = false;
        [SerializeField] private bool sfx = true;
        [SerializeField] private bool doorColliders = true;
        [SerializeField] private bool fireworks = true;
        
        [Header("Object References")]
        public GameObject[] objsPens;
        public Button[] togglesPens;

        public GameObject[] objsJoinAlert;
        public Button[] togglesJoinAlert;

        public GameObject[] objsGames;
        public Button[] togglesGames;

        public GameObject[] objsClocks;
        public Button[] togglesClocks;

        public GameObject[] objsPolaroids;
        public Button[] togglesPolaroids;
        
        public GameObject[] objsSfx;
        public Button[] togglesSfx;

        public BoxCollider[] objsDoorColliders;
        public Button[] togglesDoorColliders;
        
        public GameObject[] objsFireworks;
        public Button[] togglesFireworks;

        [Header("Post-Processing")]
        public GameObject darknessNormal;
        public GameObject darknessDark;
        public GameObject darknessDarker;
        public GameObject darknessMoreDarker;

        public Image[] toggleDarknessNormal;
        public Image[] toggleDarknessDark;
        public Image[] toggleDarknessDarker;
        public Image[] toggleDarknessMoreDarker;

        [Header("Potato")]
        public Button[] togglesPotato;
        public GameObject[] potatoDisable;
        public MeshRenderer potatoMeshRendererWater;
        public Material potatoMatWaterOn;
        public Material potatoMatWaterOff;

        [Header("State colors")]
        public Color32 buttonOn = new Color(15/255f, 132/255f, 12/255f, 255/255f);
        public Color32 buttonOffGrey = new Color(89/255f, 89/255f, 89/255f, 255/255f);
        public Color32 buttonOffRed = new Color(135/255f, 0/255f, 0/255f, 255/255f);

        public void Start() {
            Debug.Log("[OTR_WRLD_SETTINGS] Applying default world settings...");
            _ApplyAllSettings();
            Debug.Log("[OTR_WRLD_SETTINGS] Applying default world settings... done");
        }

        public void _ApplyAllSettings() {
            setStates(objsPens, togglesPens, pens);
            setStates(objsJoinAlert, togglesJoinAlert, joinAlert);
            setStates(objsGames, togglesGames, games);
            setStates(objsClocks, togglesClocks, clocks);
            setStates(objsPolaroids, togglesPolaroids, polaroids);
            setStates(objsSfx, togglesSfx, sfx);
            setButtons(togglesPotato, potato);
            setButtons(togglesDoorColliders, doorColliders);
            setButtons(togglesFireworks, fireworks);
            _setDarknessNormal();
        }

        private void setStates(GameObject[] objs, Button[] buttons, bool state) {
            string stateStr = "true";
            if (!state) {
                stateStr = "false";
            }
            foreach (GameObject obj in objs) {
                if (!Utilities.IsValid(obj)) { continue; }

                Debug.Log("[OTR_WRLD_SETTINGS] Setting GameObject state " + stateStr + " to " + obj.name);
                obj.SetActive(state);
            }
            foreach (Button button in buttons) {
                if (!Utilities.IsValid(button)) { continue; }

                Debug.Log("[OTR_WRLD_SETTINGS] Setting Button color state " + stateStr + " to " + button.name);
                if (state) {
                    button.GetComponentInChildren<TextMeshProUGUI>().text = "on";
                    button.GetComponent<Image>().color = buttonOn;
                } else {
                    button.GetComponentInChildren<TextMeshProUGUI>().text = "off";
                    button.GetComponent<Image>().color = buttonOffRed;
                }
            }
        }

        private void setStates(BoxCollider[] objs, Button[] buttons, bool state) {
            string stateStr = "true";
            if (!state) {
                stateStr = "false";
            }
            foreach (BoxCollider obj in objs) {
                if (!Utilities.IsValid(obj)) { continue; }

                Debug.Log("[OTR_WRLD_SETTINGS] Setting BoxCollider state " + stateStr + " to " + obj.name);
                obj.enabled = state;
            }
            foreach (Button button in buttons) {
                if (!Utilities.IsValid(button)) { continue; }

                Debug.Log("[OTR_WRLD_SETTINGS] Setting Button color state " + stateStr + " to " + button.name);
                if (state) {
                    button.GetComponentInChildren<TextMeshProUGUI>().text = "on";
                    button.GetComponent<Image>().color = buttonOn;
                } else {
                    button.GetComponentInChildren<TextMeshProUGUI>().text = "off";
                    button.GetComponent<Image>().color = buttonOffRed;
                }
            }
        }

        private void setButtons(Button[] buttons, bool state) {
            string stateStr = "true";
            if (!state) {
                stateStr = "false";
            }
            foreach (Button button in buttons) {
                if (!Utilities.IsValid(button)) { continue; }

                Debug.Log("[OTR_WRLD_SETTINGS] Setting Button color state " + stateStr + " to " + button.name);
                if (state) {
                    button.GetComponentInChildren<TextMeshProUGUI>().text = "on";
                    button.GetComponent<Image>().color = buttonOn;
                } else {
                    button.GetComponentInChildren<TextMeshProUGUI>().text = "off";
                    button.GetComponent<Image>().color = buttonOffRed;
                }
            }
        }

        public void _togglePens() {
            pens = !pens;
            setStates(objsPens, togglesPens, pens);
        }

        public void _toggleJoinAlert() {
            joinAlert = !joinAlert;
            setStates(objsJoinAlert, togglesJoinAlert, joinAlert);
        }

        public void _toggleGames() {
            games = !games;
            setStates(objsGames, togglesGames, games);
        }

        public void _toggleClocks() {
            clocks = !clocks;
            setStates(objsClocks, togglesClocks, clocks);
        }

        public void _togglePolaroids() {
            polaroids = !polaroids;
            setStates(objsPolaroids, togglesPolaroids, polaroids);
        }

        public void _toggleSfx() {
            sfx = !sfx;
            setStates(objsSfx, togglesSfx, sfx);
        }

        public void _toggleDoorsColliders() {
            doorColliders = !doorColliders;
            setStates(objsDoorColliders, togglesDoorColliders, doorColliders);
        }

        public void _toggleFireworks() {
            fireworks = !fireworks;
            setStates(objsFireworks, togglesFireworks, fireworks);
        }

        public void _togglePotato() {
            potato = !potato;
            if (potato) {
                // disable stuff
                polaroids = false;
                pens = false;
                potatoMeshRendererWater.material = potatoMatWaterOff;
            } else {
                // re-enable it (except VRSL)
                polaroids = true;
                pens = true;
                potatoMeshRendererWater.material = potatoMatWaterOn;
            }

            // Apply state
            setStates(objsPolaroids, togglesPolaroids, polaroids);
            setStates(objsPens, togglesPens, pens);
        }

        public void _setDarknessNormal() {
            Debug.Log("[OTR_WRLD_SETTINGS] Setting Darkness to normal");
            //darknessNormal.SetActive(true);
            if (Utilities.IsValid(darknessDark))
                darknessDark.SetActive(false);
            if (Utilities.IsValid(darknessDarker))
                darknessDarker.SetActive(false);
            if (Utilities.IsValid(darknessMoreDarker))
                darknessMoreDarker.SetActive(false);
            foreach (Image image in toggleDarknessNormal) {
                if (!Utilities.IsValid(image)) { continue; }
                image.color = buttonOn;
            }
            foreach (Image image in toggleDarknessDark) {
                if (!Utilities.IsValid(image)) { continue; }
                image.color = buttonOffGrey;
            }
            foreach (Image image in toggleDarknessDarker) {
                if (!Utilities.IsValid(image)) { continue; }
                image.color = buttonOffGrey;
            }
            foreach (Image image in toggleDarknessMoreDarker) {
                if (!Utilities.IsValid(image)) { continue; }
                image.color = buttonOffGrey;
            }
        }

        public void _setDarknessDark() {
            Debug.Log("[OTR_WRLD_SETTINGS] Setting Darkness to dark");
            //darknessNormal.SetActive(false);
            if (Utilities.IsValid(darknessDark))
                darknessDark.SetActive(true);
            if (Utilities.IsValid(darknessDarker))
                darknessDarker.SetActive(false);
            if (Utilities.IsValid(darknessMoreDarker))
                darknessMoreDarker.SetActive(false);
            foreach (Image image in toggleDarknessNormal) {
                if (!Utilities.IsValid(image)) { continue; }
                image.color = buttonOffGrey;
            }
            foreach (Image image in toggleDarknessDark) {
                if (!Utilities.IsValid(image)) { continue; }
                image.color = buttonOn;
            }
            foreach (Image image in toggleDarknessDarker) {
                if (!Utilities.IsValid(image)) { continue; }
                image.color = buttonOffGrey;
            }
            foreach (Image image in toggleDarknessMoreDarker) {
                if (!Utilities.IsValid(image)) { continue; }
                image.color = buttonOffGrey;
            }
        }
        
        public void _setDarknessDarker() {
            Debug.Log("[OTR_WRLD_SETTINGS] Setting Darkness to darker");
            //darknessNormal.SetActive(false);
            if (Utilities.IsValid(darknessDark))
                darknessDark.SetActive(false);
            if (Utilities.IsValid(darknessDarker))
                darknessDarker.SetActive(true);
            if (Utilities.IsValid(darknessMoreDarker))
                darknessMoreDarker.SetActive(false);
            foreach (Image image in toggleDarknessNormal) {
                if (!Utilities.IsValid(image)) { continue; }
                image.color = buttonOffGrey;
            }
            foreach (Image image in toggleDarknessDark) {
                if (!Utilities.IsValid(image)) { continue; }
                image.color = buttonOffGrey;
            }
            foreach (Image image in toggleDarknessDarker) {
                if (!Utilities.IsValid(image)) { continue; }
                image.color = buttonOn;
            }
            foreach (Image image in toggleDarknessMoreDarker) {
                if (!Utilities.IsValid(image)) { continue; }
                image.color = buttonOffGrey;
            }
        }

        public void _setDarknessMoreDarker() {
            Debug.Log("[OTR_WRLD_SETTINGS] Setting Darkness to MORE darker");
            //darknessNormal.SetActive(false);
            if (Utilities.IsValid(darknessDark))
                darknessDark.SetActive(false);
            if (Utilities.IsValid(darknessDarker))
                darknessDarker.SetActive(false);
            if (Utilities.IsValid(darknessMoreDarker))
                darknessMoreDarker.SetActive(true);
            foreach (Image image in toggleDarknessNormal) {
                if (!Utilities.IsValid(image)) { continue; }
                image.color = buttonOffGrey;
            }
            foreach (Image image in toggleDarknessDark) {
                if (!Utilities.IsValid(image)) { continue; }
                image.color = buttonOffGrey;
            }
            foreach (Image image in toggleDarknessDarker) {
                if (!Utilities.IsValid(image)) { continue; }
                image.color = buttonOffGrey;
            }
            foreach (Image image in toggleDarknessMoreDarker) {
                if (!Utilities.IsValid(image)) { continue; }
                image.color = buttonOn;
            }
        }

        public void _worldBuildMismatch() {
            Debug.Log("[OTR_WORLD_SETTINGS] New world build detected !");
        }

        public void _worldBuildMatch() {
            Debug.Log("[OTR_WORLD_SETTINGS] World build is up to date.");
        }

        public void _worldBuildTimeout() {
            Debug.Log("[OTR_WORLD_SETTINGS] World build check timeout.");
        }
    }
}