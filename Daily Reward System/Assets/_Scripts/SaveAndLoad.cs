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

        if (File.Exists(FilePath))
        {
            File.Delete(FilePath);

            Debug.LogError("file deleted");
        }

        //path of file
        string filepath = FilePath;
        
        //conver object to json
        string json = JsonUtility.ToJson(Data);
        
        Debug.Log("save "+json);
        
        //save file
        File.WriteAllText(filepath, json);                
    }        

    public static void LoadData() 
    {
        if (File.Exists(FilePath))
        {            
            string load = File.ReadAllText(FilePath);

            Debug.Log("LoadData " + load);

            DailyRewardData dailyRewardData = new DailyRewardData();
            dailyRewardData = JsonUtility.FromJson<DailyRewardData>(load);

            GameManager.itemClassesList.Clear();

            GameManager.itemClassesList = dailyRewardData.itemClassesList;
        }       
    }
}
