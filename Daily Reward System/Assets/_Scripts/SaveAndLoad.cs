using UnityEngine;
using System.IO;
public static class SaveAndLoad 
{

    public readonly static string FilePath = Application.persistentDataPath + "/dailyRewards.json";

    /// <summary>
    /// as name suggest save into json file in local mmachine
    /// </summary>
    /// <param name="Data"> DailyRewardData object</param>
    public static void SaveData(DailyRewardData Data)
    {
        Debug.Log("save data");

        //path of file
        string filepath = FilePath;
        
        //conver object to json
        string json = JsonUtility.ToJson(Data);
        
        Debug.Log(json);
        
        //save file
        File.WriteAllText(filepath, json);                
    }        

    public static void LoadData() 
    {
        if (File.Exists(FilePath))
        {
            Debug.Log("LoadData");

            string load = File.ReadAllText(FilePath);

            DailyRewardData dailyRewardData = new DailyRewardData();
            dailyRewardData = JsonUtility.FromJson<DailyRewardData>(load);

            GameManager.itemClassesList = dailyRewardData.itemClassesList;
        }       
    }
}
