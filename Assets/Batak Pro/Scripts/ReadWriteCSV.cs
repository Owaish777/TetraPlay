using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;

public static class ReadWriteCSV
{
    public static void StartCoroutine(IEnumerator coroutine)
    {
        CoroutineManager.Instance.RunCoroutine(coroutine);
    }

    //Takes a struct entry ans filepath as input and store the data in the struct to the given file
    //in the same order as the struct is define.
    public static void WriteCSV<T>(T entry , string filePath)
    {
        Type structType = typeof(T);

        FieldInfo[] feilds = structType.GetFields();

        string seperator = ",";
        List<string> data = new List<string>{ };

        foreach (FieldInfo feild in feilds)
        {
            data.Add(feild.GetValue(entry).ToString());
        }
        string savsString = string.Join(seperator, data) + "\n";

        File.AppendAllText(filePath, savsString);
    }

    //Returns a list of the data that is in the filePath , stored in the given struct formet.
    public static List<T> ReadCSV<T>(string filePath, Func<string[], T> mapFunction)
    {
        List<T> dataList = new List<T>();

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            // Start from 1 to skip the header line
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                T data = mapFunction(values);
                dataList.Add(data);
            }
        }
        else
        {
            Debug.Log("CSV file not found!");
        }

        return dataList;
    }

    //Retuns top rows based on keySelector , for a csv filePath , in the given struct format.
    public static List<T> LoadTopRows<T>(string filePath, Func<string[], T> mapFunction, Func<T, int> keySelector, int topCount)
    {
        // Read data from CSV
        List<T> data = ReadCSV(filePath, mapFunction);
        List<T> topData = new List<T>();

        var topEntries = data.OrderByDescending(keySelector).Take(topCount);

        foreach (var entry in topEntries)
        {
            topData.Add(entry);
        }

        return topData;
    }



    public static void WriteSpreadSheet<T>(string url , T entry , string[] inputFieldIDs)
    {
        StartCoroutine(PostToGoogleForm<T>(url , entry , inputFieldIDs));
    }

    private static IEnumerator PostToGoogleForm<T>(string url, T entry, string[] inputFieldIDs)
    {

        WWWForm form = new WWWForm();

        // Use reflection to get fields of the struct
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);

        // Ensure we have the correct number of inputFieldIDs
        if (fields.Length != inputFieldIDs.Length)
        {
            Debug.LogError("Number of inputFieldIDs does not match the number of fields in the struct.");
            yield break;
        }

        for (int i = 0; i < fields.Length; i++)
        {
            string fieldName = inputFieldIDs[i];
            object value = fields[i].GetValue(entry);
            form.AddField(fieldName, value?.ToString() ?? string.Empty);
        }

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Form Submitted Successfully");
        }
        else
        {
            Debug.LogError("Form Submission Failed: " + www.error);
        }
    }

    

    //Loads the leaderboard data form saved data
    /* public static void LoadData()
     {
         if (PlayerPrefs.HasKey(LEADERBOARD_KEY))
         {
             string leaderboardData2 = PlayerPrefs.GetString(LEADERBOARD_KEY);
             LeaderboardEntryList loadedEntryList = JsonUtility.FromJson<LeaderboardEntryList>(leaderboardData2);

             leaderboardEntries = loadedEntryList.entries;
         }
         else
         {
             leaderboardEntries = new List<LeaderboardEntry>();
         }
     }*/

    //Saves leaderboard data
    /*public static void SaveData()
    {
        LeaderboardEntryList leaderboardEntryList = new LeaderboardEntryList { entries = leaderboardEntries };

        string leaderboardData = JsonUtility.ToJson(leaderboardEntryList);

        PlayerPrefs.SetString(LEADERBOARD_KEY, leaderboardData);
        PlayerPrefs.Save();
    }*/

}
