using UnityEngine;
using System;

public static class PlayerPrefManager
{

    public readonly static string weekStartDate = "weekStartDate";

    public static void saveWeekStartDate(string dateTime)
    {
        PlayerPrefs.SetString(weekStartDate, dateTime);
    }

    public static string getWeekStartDate()
    {
        return PlayerPrefs.GetString(weekStartDate);
    }
}
