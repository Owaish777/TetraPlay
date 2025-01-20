using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

public static class ApiService
{
    public static void StartCoroutine(IEnumerator coroutine)
    {
        CoroutineManager.Instance.RunCoroutine(coroutine);
    }

    public static void GET(string url , Action<UnityWebRequest> success, Action<UnityWebRequest> failer)
    {
        StartCoroutine(Get(url  ,success, failer));
    }

    static IEnumerator Get(string url , Action<UnityWebRequest> success, Action<UnityWebRequest> failer)
    {
        // Create a UnityWebRequest for a GET request
        UnityWebRequest request = UnityWebRequest.Get(url);

        // Send the request and wait for a response
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("GET request success");
            success(request);
        }
        else
        {
            Debug.LogError("GET request failed");
            failer(request);
        }
    }


    public static void POST(string url ,string jsonData, Action<UnityWebRequest> success, Action<UnityWebRequest> failer)
    {
        StartCoroutine(Post(url , jsonData, success, failer));
    }

    static IEnumerator Post(string url , string jsonData, Action<UnityWebRequest> success, Action<UnityWebRequest> failer)
    {
        // Create a UnityWebRequest
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for a response
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("POST request success");
            success(request);
        }
        else
        {
            Debug.LogError("POST request failed");
            failer(request);
        }
    }


    public static void DELETE(string url, string jsonData, Action<UnityWebRequest> success, Action<UnityWebRequest> failer)
    {
        StartCoroutine(Delete(url, jsonData, success, failer));
    }

    static IEnumerator Delete(string url, string jsonData, Action<UnityWebRequest> success, Action<UnityWebRequest> failer)
    {
        // Create a UnityWebRequest for DELETE
        UnityWebRequest request = new UnityWebRequest(url , "DELETE");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for a response
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("DELETE request success");
            success(request);
        }
        else
        {
            Debug.LogError("DELETE request failed");
            failer(request);
        }
    }


    public static void PUT(string url, string jsonData , Action<UnityWebRequest> success, Action<UnityWebRequest> failer)
    {
        StartCoroutine(Put(url, jsonData , success , failer));
    }

    static IEnumerator Put(string url, string jsonData, Action<UnityWebRequest> success, Action<UnityWebRequest> failer)
    {
        // Create the UnityWebRequest for PUT
        UnityWebRequest request = new UnityWebRequest(url, "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for a response
        yield return request.SendWebRequest();


        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("PUT request success");
            success(request);
        }
        else
        {
            Debug.LogError("PUT request failed");
            failer(request);
        }
    }
}
