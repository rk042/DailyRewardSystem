using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    public Text DayCount;         
    public Text Amount;
    public string ItemName;

    public bool IsAveilble;
    public bool IsCollected;


    private void Start()
    {
        //if is collected so remove button and deseble item
        if (IsCollected)
        {
            IsCollectedMethod();
        }
    }

    private void IsCollectedMethod()
    {        
        this.GetComponent<Image>().color = Color.black;
        this.transform.Find("Image").transform.GetComponent<Image>().color = Color.white;
        this.transform.Find("Name Text UI").transform.GetComponent<Text>().color = Color.white;
        this.transform.Find("Amount").transform.GetComponent<Text>().color = Color.white;
        this.transform.Find("Day Text").transform.GetComponent<Text>().color = Color.white;

        this.GetComponent<Button>().enabled = false;
    }

    public void btn_collectItem()
    {
        if (IsAveilble)
        {
            Debug.Log("item collected");

            IsCollected = true;
            IsCollectedMethod();
        }
        else
        {
            Debug.Log("item is not aveilble");
        }
    }
}
