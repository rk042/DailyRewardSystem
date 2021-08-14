using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DailyRewardManager : MonoBehaviour
{

    [SerializeField] private Transform _parentOfItems;
    [SerializeField] private List<GameObject> _rewardItems=new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(SaveAndLoad.FilePath))
        {
            Debug.Log("file exist");

            //load data from file
            SaveAndLoad.LoadData();

            //set item to ui
            for (int i = 0; i < GameManager.itemClassesList.Count; i++)
            {
                if (GameManager.itemClassesList[i].itemName=="coin")
                {
                    GameObject temp = Instantiate(_rewardItems[0], _parentOfItems);
                    temp.GetComponent<Items>().Amount.GetComponent<Text>().text = GameManager.itemClassesList[i].itemAmount;
                    temp.GetComponent<Items>().DayCount.GetComponent<Text>().text = GameManager.itemClassesList[i].itemDay;
                    temp.GetComponent<Items>().ItemName = GameManager.itemClassesList[i].itemName;
                }
                else if (GameManager.itemClassesList[i].itemName == "dimon")
                {
                    GameObject temp = Instantiate(_rewardItems[1], _parentOfItems);
                    temp.GetComponent<Items>().Amount.GetComponent<Text>().text = GameManager.itemClassesList[i].itemAmount;
                    temp.GetComponent<Items>().DayCount.GetComponent<Text>().text = GameManager.itemClassesList[i].itemDay;
                    temp.GetComponent<Items>().ItemName = GameManager.itemClassesList[i].itemName;
                }
            }
        }
        else
        {
            for (int i = 0; i < 7; i++)
            {
                GameObject temp = Instantiate(_rewardItems[Random.Range(0, _rewardItems.Count)], _parentOfItems);

                if (temp.GetComponent<Items>().ItemName == "coin")
                {
                    temp.GetComponent<Items>().Amount.GetComponent<Text>().text = ((100 * i) + 100).ToString();
                    temp.GetComponent<Items>().DayCount.GetComponent<Text>().text = "Day " + (1 + i).ToString();

                }
                else if (temp.GetComponent<Items>().ItemName == "dimon")
                {
                    temp.GetComponent<Items>().Amount.GetComponent<Text>().text = (Random.Range(1, 7)).ToString();
                    temp.GetComponent<Items>().DayCount.GetComponent<Text>().text = "Day " + (1 + i).ToString();
                }
            }
        }
       
    }

    private void OnApplicationQuit()
    {
        for (int i = 0; i < _parentOfItems.childCount; i++)
        {
            ItemClass itemClass = new ItemClass();
            itemClass.itemAmount = _parentOfItems.GetChild(i).transform.GetComponent<Items>().Amount.GetComponent<Text>().text;
            itemClass.itemDay = _parentOfItems.GetChild(i).transform.GetComponent<Items>().DayCount.GetComponent<Text>().text;
            itemClass.itemName = _parentOfItems.GetChild(i).transform.GetComponent<Items>().ItemName;

            GameManager.itemClassesList.Add(itemClass);
        }

        DailyRewardData dailyRewardData = new DailyRewardData();
        dailyRewardData.itemClassesList = GameManager.itemClassesList;

        SaveAndLoad.SaveData(dailyRewardData);
    }
}
