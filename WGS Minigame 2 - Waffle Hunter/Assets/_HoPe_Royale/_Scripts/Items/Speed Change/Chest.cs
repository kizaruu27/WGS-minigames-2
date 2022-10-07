using UnityEngine;

public class Chest : MonoBehaviour
{
    public WeightedRandomList<Transform> lootTable;

    public Transform itemHolder;

    MeshRenderer Pls;


    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ShowItem();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 4f);
        }
    }

    void HideItem()
    {
        itemHolder.localScale = Vector3.zero;
        itemHolder.gameObject.SetActive(false);

        foreach (Transform child in itemHolder)
        {
            Destroy(child.gameObject);
        }
    }

    void ShowItem()
    {
        Transform item = lootTable.GetRandom();
        Instantiate(item, itemHolder);
        itemHolder.gameObject.SetActive(true);
    }
}
