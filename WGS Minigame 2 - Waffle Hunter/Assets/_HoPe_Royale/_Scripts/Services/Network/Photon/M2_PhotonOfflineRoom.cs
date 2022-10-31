using Photon.Pun;
using Photon.Realtime;

namespace RoyaleMinigames.Services.Photon
{
    public class M2_PhotonOfflineRoom : MonoBehaviourPunCallbacks
    {
        public void OnCreateRoom()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.CreateRoom("Offline Room", new RoomOptions() { MaxPlayers = 1, BroadcastPropsChangeToAll = true });
            }
        }
    }
}