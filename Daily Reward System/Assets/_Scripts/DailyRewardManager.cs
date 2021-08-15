using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class DailyRewardManager : MonoBehaviour
{

    [SerializeField] private Transform _parentOfItems;
    [SerializeField] private List<GameObject> _rewardItems=new List<GameObject>();

    private DateTime WeekStartDate;
    private DateTime TodayDate;



    // Start is called before the first frame update
    void Start()
    {
        //check if frist time game lunch
        if (PlayerPrefs.HasKey(PlayerPrefManager.weekStartDate))
        {
            WeekStartDate = Convert.ToDateTime(PlayerPrefManager.getWeekStartDate()).Date;
        }
        else
        {
            WeekStartDate = DateTime.Today.Date;
        }
       

        Debug.Log("Week Start Date "+WeekStartDate);

        TodayDate = DateTime.Today.Date;

        Debug.Log("Today Date "+TodayDate);

        Debug.Log("Week end date " + WeekStartDate.AddDays(7).Date);

        if (WeekStartDate.AddDays(7).Date.Day<TodayDate.Day)
        {
            Debug.Log(WeekStartDate.AddDays(7).Date + "  week end  "+TodayDate);
            File.Delete(SaveAndLoad.FilePath);
            WeekStartDate = TodayDate;

            //save in playerprefs
            PlayerPrefManager.saveWeekStartDate(WeekStartDate.ToString());
        }
        else
        {
            Debug.Log("week not end");
        }

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
                    temp.GetComponent<Items>().IsCollected = GameManager.itemClassesList[i].IsCollected;
                    setItemUnCollectable(temp);
                }
                else if (GameManager.itemClassesList[i].itemName == "dimon")
                {
                    GameObject temp = Instantiate(_rewardItems[1], _parentOfItems);
                    temp.GetComponent<Items>().Amount.GetComponent<Text>().text = GameManager.itemClassesList[i].itemAmount;
                    temp.GetComponent<Items>().DayCount.GetComponent<Text>().text = GameManager.itemClassesList[i].itemDay;
                    temp.GetComponent<Items>().ItemName = GameManager.itemClassesList[i].itemName;
                    temp.GetComponent<Items>().IsCollected = GameManager.itemClassesList[i].IsCollected;
                    setItemUnCollectable(temp);
                }               
            }
        }
        else
        {
            for (int i = 0; i < 7; i++)
            {
                GameObject temp = Instantiate(_rewardItems[UnityEngine.Random.Range(0, _rewardItems.Count)], _parentOfItems);

                if (temp.GetComponent<Items>().ItemName == "coin")
                {
                    temp.GetComponent<Items>().Amount.GetComponent<Text>().text = ((100 * i) + 100).ToString();
                    temp.GetComponent<Items>().DayCount.GetComponent<Text>().text = "Day " + (1 + i).ToString();

                }
                else if (temp.GetComponent<Items>().ItemName == "dimon")
                {
                    temp.GetComponent<Items>().Amount.GetComponent<Text>().text = (UnityEngine.Random.Range(1, 7)).ToString();
                    temp.GetComponent<Items>().DayCount.GetComponent<Text>().text = "Day " + (1 + i).ToString();
                }

                setItemUnCollectable(temp);
            }
        }

        //set item for is aveilble for collect
        int count = TodayDate.Day - WeekStartDate.Day;

        Debug.Log(count);

        for (int i = 0; i < count; i++)
        {
            if (!_parentOfItems.transform.GetChild(i).transform.GetComponent<Items>().IsCollected)
            {
                _parentOfItems.transform.GetChild(i).transform.GetComponent<Items>().IsAveilble = true;
            }
            
            setAveilbleForCollected(_parentOfItems.transform.GetChild(i).transform.gameObject);
        }
    }

    private void setItemUnCollectable(GameObject temp)
    {
        temp.transform.Find("Image").transform.GetComponent<Image>().color=Color.black;
        temp.transform.Find("Name Text UI").transform.GetComponent<Text>().color=Color.black;
        temp.transform.Find("Amount").transform.GetComponent<Text>().color=Color.black;
        temp.transform.Find("Day Text").transform.GetComponent<Text>().color=Color.black;
    }
    
    private void setAveilbleForCollected(GameObject temp)
    {
        temp.transform.Find("Image").transform.GetComponent<Image>().color=Color.white;
        temp.transform.Find("Name Text UI").transform.GetComponent<Text>().color=Color.white;
        temp.transform.Find("Amount").transform.GetComponent<Text>().color=Color.white;
        temp.transform.Find("Day Text").transform.GetComponent<Text>().color=Color.white;
    }

    private void OnApplicationQuit()
    {
        //clear old item
        GameManager.itemClassesList.Clear();

        for (int i = 0; i < _parentOfItems.childCount; i++)
        {
            ItemClass itemClass = new ItemClass();
            itemClass.itemAmount = _parentOfItems.GetChild(i).transform.GetComponent<Items>().Amount.GetComponent<Text>().text;
            itemClass.itemDay = _parentOfItems.GetChild(i).transform.GetComponent<Items>().DayCount.GetComponent<Text>().text;
            itemClass.itemName = _parentOfItems.GetChild(i).transform.GetComponent<Items>().ItemName;
            itemClass.IsCollected = _parentOfItems.GetChild(i).transform.GetComponent<Items>().IsCollected;

            GameManager.itemClassesList.Add(itemClass);
        }

        DailyRewardData dailyRewardData = new DailyRewardData();
        dailyRewardData.itemClassesList = GameManager.itemClassesList;

        SaveAndLoad.SaveData(dailyRewardData);
    }

    public void btn_Quit()
    {
        Debug.LogError("application quite");
        Application.Quit();
    }
}
