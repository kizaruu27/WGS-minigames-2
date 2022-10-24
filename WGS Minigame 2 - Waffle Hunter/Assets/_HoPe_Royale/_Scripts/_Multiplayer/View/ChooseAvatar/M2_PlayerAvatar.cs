using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using RoyaleMinigames.Manager.Room;
using TMPro;

namespace RoyaleMinigames.View.PlayerAvatar
{
    public class M2_PlayerAvatar : MonoBehaviourPunCallbacks
    {
        [Header("Player Component")]
        public TextMeshProUGUI playerName;
        public Image playerAvatar;
        public Sprite[] avatars;
        public int avatarIndex;
        public GameObject statusReady;

        [Header("Change Properties")]
        public GameObject objectButton;
        public int _AvIndex;

        // [SerializeField] GameObject playbutton;

        ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
        Player player;

        private void Awake()
        {
            playerProperties["playerAvatar"] = PlayerPrefs.GetInt("playerAvatar");
            PhotonNetwork.LocalPlayer.CustomProperties = playerProperties;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);

            statusReady.SetActive(true);

            playerProperties["statusReady"] = false;
            PhotonNetwork.LocalPlayer.CustomProperties = playerProperties;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        }

        public void OnClickShowDisplayAvatar()
        {
            M2_RoomManager.instance.DisplayAvaParent.SetActive(true);
            M2_RoomManager.instance.AvaToggleGroup.SetAllTogglesOff();
        }

        public void OnClickGetAvatarIndex(int _index)
        {
            _AvIndex = _index;
        }

        public void OnClickSetAvatarIndex()
        {
            avatarIndex = _AvIndex;

            PlayerPrefs.SetInt("playerAvatar", avatarIndex);

            playerProperties["playerAvatar"] = (int)_AvIndex;
            PhotonNetwork.LocalPlayer.CustomProperties = playerProperties;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);

            M2_RoomManager.instance.DisplayAvaParent.SetActive(false);
        }

        public void PlayerReady()
        {
            playerProperties["statusReady"] = true;
            PhotonNetwork.LocalPlayer.CustomProperties = playerProperties;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);

            if (PhotonNetwork.LocalPlayer.IsLocal)
                M2_RoomManager.instance.SetPlayerReady();
        }

        public void SetPlayerInfo(Player _player)
        {
            playerName.text = _player.NickName;
            player = _player;
            UpdatePlayerItem(player);
        }

        public void ApplyLocalChanges()
        {
            objectButton.SetActive(true);
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {
            if (player == targetPlayer)
            {
                UpdatePlayerItem(targetPlayer);
            }
        }

        void UpdatePlayerItem(Player player)
        {

            if (player.CustomProperties.ContainsKey("playerAvatar"))
            {
                playerAvatar.sprite = avatars[(int)player.CustomProperties["playerAvatar"]];
                playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
            }
            else
            {
                playerProperties["playerAvatar"] = 0;
            }

            if (player.CustomProperties.ContainsKey("statusReady") && player.CustomProperties["statusReady"].Equals(true))
            {
                // Debug.Log("player ready");
                statusReady.gameObject.SetActive(false);
            }

        }
    }
}
