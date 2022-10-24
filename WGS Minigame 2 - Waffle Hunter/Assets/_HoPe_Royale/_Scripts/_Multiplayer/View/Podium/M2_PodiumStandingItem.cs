
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class M2_PodiumStandingItem : MonoBehaviour
{
    [Header("Text Component")]
    public TextMeshProUGUI Rank;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Score;

    public void SetColorItem(bool IsMine = false)
    {
        if (IsMine)
        {
            Image colorItem = gameObject.GetComponent<Image>();
            colorItem.color = Color.green;

            TextMeshProUGUI[] colorTextItem = gameObject.GetComponentsInChildren<TextMeshProUGUI>();

            foreach (var item in colorTextItem)
            {
                item.color = Color.black;
            }
        }
    }

    public void SetHighlightPlayerDC()
    {
        Image colorItem = gameObject.GetComponent<Image>();
        colorItem.color = Color.red;

        TextMeshProUGUI[] colorTextItem = gameObject.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (var item in colorTextItem)
        {
            item.color = Color.white;
        }
    }
}