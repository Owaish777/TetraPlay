using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PlayFabService
{

    public static void Login(Action onSuccess, Action<string> onError)
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, result => onSuccess?.Invoke(),
            error => onError?.Invoke(error.GenerateErrorReport()));
    }

    public static void Login(string id , string playerName , Action onSuccess, Action<string> onError)
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = id,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request,
            result =>
            {
                SetPlayerDisplayName(playerName);
                onSuccess?.Invoke();
            },
            error => onError?.Invoke(error.GenerateErrorReport()));
    }

    /*public static void Login(Action onSuccess, Action<string> onError)
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, result => onSuccess?.Invoke(),
            error => onError?.Invoke(error.GenerateErrorReport()));
    }*/

    public static void SetPlayerDisplayName(string playerName)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = playerName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request,
            result => Debug.Log($"Display name updated to: {result.DisplayName}"),
            error => Debug.LogError($"Failed to set display name: {error.GenerateErrorReport()}"));
    }

    public static void SubmitScore(string statisticName, int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = statisticName,
                    Value = score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request,
            result => Debug.Log($"Score submitted successfully for {statisticName}!"),
            error => Debug.LogError($"Failed to submit score: {error.GenerateErrorReport()}"));
    }

    public static void GetLeaderboard(string statisticName, int maxResults)
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = statisticName,
            StartPosition = 0, // Start from the top
            MaxResultsCount = maxResults
        };

        PlayFabClientAPI.GetLeaderboard(request,
            result =>
            {
                Debug.Log($"Leaderboard for {statisticName}:");
                foreach (var entry in result.Leaderboard)
                {
                    Debug.Log($"Rank: {entry.Position + 1}, Player: {entry.DisplayName ?? "Anonymous"}, Score: {entry.StatValue}");
                }
            },
            error => Debug.LogError($"Failed to retrieve leaderboard: {error.GenerateErrorReport()}"));
    }
}
