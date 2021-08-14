using System.Collections.Generic;

[System.Serializable]
public class DailyRewardData
{
    public List<ItemClass> itemClassesList = new List<ItemClass>();
}

[System.Serializable]
public class ItemClass
{
    public string itemName;
    public string itemAmount;
    public string itemDay;
}
