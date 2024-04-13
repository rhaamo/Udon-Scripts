#pragma warning disable IDE0044 // Making serialized fields readonly hides them from the inspector
#pragma warning disable 649

using System;
using System.Globalization;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VRC.SDKBase;

// Based on https://github.com/Varneon/UdonEssentials/tree/main/Assets/Varneon/Udon%20Prefabs/Essentials/Playerlist

namespace Varneon.UdonPrefabs.Essentials
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class Playerlist : UdonSharpBehaviour
    {
        #region Serialized Fields

        [Header("Settings")]
        [SerializeField]
        private Groups groups;
        public HiddenPlayerList hiddenPlayers;

        [Space]
        [Header("References")]
        [SerializeField]
        private Transform PlayerList;

        [SerializeField]
        private Text TextPlayersOnline, TextInstanceLifetime, TextInstanceMaster, TextTimeInWorld;

        [SerializeField]
        private GameObject PlayerListItem;

        [SerializeField]
        private GameObject roleNameItem;

        [SerializeField]
        private string GroupNameAdmin = "Admins";
        [SerializeField]
        private string GroupNameMods = "Mods";

        #endregion

        #region Private Variables

        [UdonSynced] private long instanceStartTime = 0;
        private long utcNow;
        private long localJoinTime = 0;
        private VRCPlayerApi localPlayer;
        private VRCPlayerApi[] players;
        private int playerCount;
        private float updateTimer = 0f;
        private const int
            INDEX_ID = 1,
            INDEX_VR = 4, // Changed from a sprite to a text, using INDEX_TEXT_VR
            INDEX_GROUP1 = 5,
            INDEX_GROUP2 = 6,
            INDEX_TEXT_ID = 0,
            INDEX_TEXT_VR = 1,
            INDEX_TEXTMESHPRO_NAME = 0,
            INDEX_TEXT_TIME = 2;
        private const NumberStyles HEX_NUMSTYLE = NumberStyles.HexNumber;

        private bool hasSuperRights = false;

        #endregion

        private void Start() {
            localPlayer = Networking.LocalPlayer;

            UpdateInstanceMaster();

            UpdateUtcTime();

            localJoinTime = utcNow;

            if (localPlayer.isMaster) {
                instanceStartTime = utcNow;

                RequestSerialization();
            }

            hasSuperRights = checkLocalUserRights();
        }

        private bool checkLocalUserRights() {
            if (!Utilities.IsValid(localPlayer)) {
                return false; // idk wtf
            }

            string displayName = localPlayer.displayName;

            // Get the group indices of the player based on the displayName of the player
            int[] groupIndices = groups._GetGroupIndicesOfPlayer(displayName);

            // Iterate through every group that the player is part of
            foreach (int index in groupIndices) {
                // The following information can be fetched from groups based on the index of the group
                string groupName = groups._GetGroupName(index);
                Sprite groupIcon = groups._GetGroupIcon(index);
                string groupArgs = groups._GetGroupArguments(index);

                Debug.Log($"[OTR_PLAYERLIST] {displayName} <color=silver>Is part of group:</color> {groupName}\n<color=silver>Group icon:</color> {(groupIcon != null ? groupIcon.name : "null")}\n<color=silver>Group arguments:</color> {groupArgs}");
                if (groupName == GroupNameAdmin || groupName == GroupNameMods) {
                    Debug.Log("[OTR_PLAYERLIST] User has super rights through Admin role.");
                    roleNameItem.GetComponent<TextMeshProUGUI>().text = "admin";
                    return true;
                }
                if (groupName == GroupNameMods) {
                    Debug.Log("[OTR_PLAYERLIST] User has super rights through Moderator role.");
                    roleNameItem.GetComponent<TextMeshProUGUI>().text = "moderator";
                    return true;
                }
            }

            roleNameItem.GetComponent<TextMeshProUGUI>().text = "user";

            return false; // in any cases, nope,
        }

        private void Update() {
            updateTimer += Time.deltaTime;

            if (updateTimer >= 1f) {
                UpdateUtcTime();

                TextInstanceLifetime.text = GetDuration(instanceStartTime);
                TextTimeInWorld.text = GetDuration(localJoinTime);

                updateTimer = 0f;
            }
        }

        private string GetDuration(long ticks)
        {
            return TimeSpan.FromTicks(utcNow - ticks).ToString(@"hh\:mm\:ss");
        }

        private void UpdateUtcTime()
        {
            utcNow = DateTime.UtcNow.ToFileTimeUtc();
        }

        private void UpdateTotalPlayerCount(int count)
        {
            players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];

            VRCPlayerApi.GetPlayers(players);

            playerCount = (count > playerCount) ? count : playerCount;

            TextPlayersOnline.text = $"{players.Length - ((count < 0) ? 1 : 0)} / {playerCount}";
        }

        private void AddPlayer(VRCPlayerApi player)
        {
            if (player.playerId > playerCount) { UpdateTotalPlayerCount(player.playerId); }

            GameObject newPlayerlistPanel = VRCInstantiate(PlayerListItem);

            newPlayerlistPanel.SetActive(true);

            Transform t = newPlayerlistPanel.transform;
            t.SetParent(PlayerList.transform);
            t.localPosition = Vector3.zero;
            t.localEulerAngles = Vector3.zero;
            t.localScale = Vector3.one;

            // Update standard Text
            Text[] texts = t.GetComponentsInChildren<Text>(true);
            texts[INDEX_TEXT_ID].text = player.playerId.ToString();
            // texts[INDEX_TEXT_NAME].text = player.displayName;
            texts[INDEX_TEXT_TIME].text = (player.playerId < localPlayer.playerId) ? "Joined before you" : DateTime.UtcNow.ToLocalTime().ToString("dd MMM hh:mm:ss");

            // Update TextMeshPro elements
            TextMeshProUGUI[] textMeshPros = t.GetComponentsInChildren<TextMeshProUGUI>(true);
            textMeshPros[INDEX_TEXTMESHPRO_NAME].text = player.displayName;

            if (player.IsUserInVR()) {
                texts[INDEX_TEXT_VR].text = "VR";
            } else {
                texts[INDEX_TEXT_VR].text = "PC";
            }

            if (groups) { ApplyGroupsInfo(t, player.displayName); }

            // At the end, configure the U# script on the new player list panel
            // This will permits handling the TP for example
            PlayerItem scriptPlayerItem = newPlayerlistPanel.GetComponent<PlayerItem>();
            scriptPlayerItem._Configure(player, hasSuperRights);
            Debug.Log("[OTR_PLAYERLIST] Added new player in list");
        }

        private void ApplyGroupsInfo(Transform playlistPanel, string displayName)
        {
            int[] playerGroupIndices = groups._GetGroupIndicesOfPlayer(displayName);

            int shownGroupCount = 0;

            bool customColorApplied = false;

            for (int i = 0; i < playerGroupIndices.Length; i++)
            {
                int groupIndex = playerGroupIndices[i];

                string groupArguments = groups._GetGroupArguments(groupIndex);

                if (!customColorApplied && groupArguments.Contains("-playerlistFrameColor"))
                {
                    string hex = GetArgumentValue(groupArguments, "-playerlistFrameColor").Replace("#", "");

                    playlistPanel.GetComponent<Image>().color = new Color(
                        byte.Parse(hex.Substring(0, 2), HEX_NUMSTYLE) / 255f,
                        byte.Parse(hex.Substring(2, 2), HEX_NUMSTYLE) / 255f,
                        byte.Parse(hex.Substring(4, 2), HEX_NUMSTYLE) / 255f
                        );

                    customColorApplied = true;

                    if (shownGroupCount == 2) { break; }
                }

                if (shownGroupCount < 2)
                {
                    GameObject imageGO = playlistPanel.GetChild(shownGroupCount == 0 ? INDEX_GROUP1 : INDEX_GROUP2).gameObject;

                    if (!groupArguments.Contains("-noPlayerlistIcon"))
                    {
                        Sprite icon = groups._GetGroupIcon(groupIndex);

                        if (icon != null)
                        {
                            imageGO.SetActive(true);

                            imageGO.GetComponent<Image>().sprite = icon;

                            shownGroupCount++;
                        }
                    }
                }
            }
        }

        private string GetArgumentValue(string args, string arg)
        {
            int argPos = args.IndexOf(arg);

            if (argPos >= 0)
            {
                argPos += arg.Length;

                int argBreak = args.IndexOf(' ', argPos);

                return args.Substring(argPos + 1, argBreak < 0 ? args.Length - argPos - 1 : argBreak - argPos);
            }

            return string.Empty;
        }

        private void RemovePlayer(int id)
        {
            for (int i = 0; i < PlayerList.childCount; i++)
            {
                Transform item = PlayerList.GetChild(i);

                if (Convert.ToInt32(item.GetChild(INDEX_ID).GetComponent<Text>().text) == id)
                {
                    Destroy(item.gameObject);

                    return;
                }
            }
        }

        private void UpdateInstanceMaster()
        {
            VRCPlayerApi master = Networking.GetOwner(gameObject);

            if (Utilities.IsValid(master)) { TextInstanceMaster.text = master.displayName; }
        }

        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            Debug.Log("[OTR_PLAYERLIST] OnPlayerJoined Adding player...");
            AddPlayer(player);
        }

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            Debug.Log("[OTR_PLAYERLIST] OnPlayerLeft Removing player...");
            if (Utilities.IsValid(player)) { RemovePlayer(player.playerId); }

            UpdateInstanceMaster();

            UpdateTotalPlayerCount(-1);
        }
    }
}