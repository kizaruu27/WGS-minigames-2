using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.InteropServices;
using M2_SimpleJSON;
using RoyaleMinigames.Models.Http.PlayerInfo;
using RoyaleMinigames.Services.Http;
using RoyaleMinigames.Services.Photon;
using RoyaleMinigames.View.Loading;


namespace RoyaleMinigames.Manager.Networking
{
    public class M2_HttpManager : MonoBehaviour
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string GetToken();
#endif

        private bool deviceType;
        private M2_MPlayerInfo result;

        readonly string EditorToken = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjIiLCJpYXQiOjE2NTI4NTQ4NjEsImV4cCI6MTY4NDQxMTc4N30.WgPvma6Sn6bSgMcB09gCSmTB11np8RQG0ZLkBvB-AZ4";
        readonly string BuildToken = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjQiLCJpYXQiOjE2NTM2MjE0NzIsImV4cCI6MTY4NTE3ODM5OH0.PsKoprNpr3sudUxukyYA58d1Hx6amSWWIOj4YERBMGQ";
        string localToken;

        // authorization token for WebGL
        string authToken;
        string urlToken;
        M2_HttpClientV2 client;

        bool isStopRequest;
        JSONNode data;
        Scene currScene;

        private void Awake()
        {
            GetToken();

            client = new M2_HttpClientV2(
                        M2_HttpConfig.BASE_URL,
                        new M2_HttpOptions(),
                        authToken
                    );

            currScene = SceneManager.GetActiveScene();
        }

        private void Update()
        {
            if (currScene.name == "M2_WGS1_Login" && !isStopRequest) GetUserData();
            // GetComponent<PhotonServer>().Connect("Play Test");
        }


        void GetToken()
        {

#if UNITY_WEBGL && !UNITY_EDITOR
            urlToken = "Bearer " + GetToken();
#endif
            localToken = (M2_CheckPlatform.isMacUnity || M2_CheckPlatform.isWindowsUnity) ? EditorToken : BuildToken;
            deviceType = M2_CheckPlatform.isWeb && (!M2_CheckPlatform.isMacUnity || !M2_CheckPlatform.isWindowsUnity);
            authToken = deviceType ? urlToken : localToken;
        }


        void GetUserData()
        {
            M2_LoginStatus.instance.StepperMessage("Getting user data...");
            StartCoroutine(client.Get(M2_HttpConfig.ENDPOINT["user"], (res) =>
            {
                isStopRequest = res != null;
                M2_LoginStatus.instance.StepperMessage("process user data...");

                if (isStopRequest)
                {
                    PlayerPrefs.SetString("token", authToken);
                    PlayerPrefs.SetString("LocalPlayerData", res["data"].ToString());

                    GetComponent<M2_PhotonServer>().Connect(JSON.Parse(PlayerPrefs.GetString("LocalPlayerData"))["full_name"]);
                }
            }));
        }
    }
}