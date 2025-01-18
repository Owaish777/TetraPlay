using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class DatabaseInterection 
{

    public static string apiUrl = "http://127.0.0.1:5000";

    static string getAllPlayerEndPoint = "/players";
    static string getPlayerEndPoint = "/players";
    static string postPlayerEndPoint = "/players";
    static string deletePlayerEndPoint = "/players";
    static string putPlayerEndPoint = "/players";
    //static string checkPlayerExistsEndPoint = "/player/check";

    static string postPlayerScoreEndPoint = "/scores/create";
    static string getAlllScoreEndPoint = "/scores";

    public static void StartCoroutine(IEnumerator coroutine)
    {
        CoroutineManager.Instance.RunCoroutine(coroutine);
    }

    public static void getAllPlayers(Action<UnityWebRequest> success , Action<UnityWebRequest> failer)
    {
        ApiService.GET(apiUrl + getAllPlayerEndPoint , success, failer);
    }

    public static void getPlayer(int playerID , Action<UnityWebRequest> success, Action<UnityWebRequest> failer)
    {
        ApiService.GET(apiUrl + getPlayerEndPoint + "/" + playerID.ToString(), success, failer);
    }

    public static void postPlayer(Player player , Action<UnityWebRequest> success, Action<UnityWebRequest> failer)
    {
        ApiService.POST(apiUrl + postPlayerEndPoint , JsonUtility.ToJson(player) , success, failer);
    }

    public static void deletePlayer(Player player, Action<UnityWebRequest> success, Action<UnityWebRequest> failer)
    {
        string jsonData = $"{{\"id\":{player.id}}}";
        ApiService.DELETE(apiUrl + deletePlayerEndPoint , jsonData , success, failer);
    }

    public static void putPlayer(Player newPlayerData , Action<UnityWebRequest> success, Action<UnityWebRequest> failer)
    {
        string jsonData = JsonUtility.ToJson(newPlayerData);
        ApiService.PUT(apiUrl + putPlayerEndPoint , jsonData, success, failer);
    }

    public static void postPlayerScore(PlayerScore score , Action<UnityWebRequest> success, Action<UnityWebRequest> failer)
    {
        string jsonData = JsonUtility.ToJson(score);
        ApiService.POST(apiUrl + postPlayerScoreEndPoint , jsonData , success, failer);
    }

    public static void getAllScores(Action<UnityWebRequest> success, Action<UnityWebRequest> failer)
    {
        ApiService.GET(apiUrl + getAlllScoreEndPoint, success, failer);
    }

}
