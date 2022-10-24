using UnityEngine;
using TMPro;

namespace RunMinigames.View.Loading
{
    public class M2_LoginStatus : MonoBehaviour
    {

        public static M2_LoginStatus instance;
        TextMeshProUGUI StatusMessage;

        public bool isConnectingToServer { get; set; }

        private void Awake()
        {
            instance = this;
            StatusMessage = GetComponent<TextMeshProUGUI>();
        }


        public void StepperMessage(string step)
        {
            StatusMessage.text = step;
        }
    }
}