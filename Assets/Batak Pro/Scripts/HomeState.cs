using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEditor.PackageManager;
using System.Collections;

public class HomeState : State
{
    TextField nameFeild;
    TextField idFeild;
    Button submit, leaderboard , settings;
    Label errorText , loggingText;

    void referenceVariables()
    {
        nameFeild = screen.Q<TextField>("Name");
        idFeild = screen.Q<TextField>("Id");

        settings = screen.Q<Button>("Settings");
        submit = screen.Q<Button>("Submit");
        leaderboard = screen.Q<Button>("Leaderboard");

        errorText = screen.Q<Label>("Error");
        loggingText = screen.Q<Label>("LoginText");

        settings.RegisterCallback<ClickEvent>(onSettingsButtonClicked);
        submit.RegisterCallback<ClickEvent>(onSubmitButtonClicked);
        leaderboard.RegisterCallback<ClickEvent>(onLeaderboardButtonClicked);

        nameFeild.value = "NAME";
        idFeild.value = "REGISTRATION CODE";
    }

    public override void enter()
    {
        if (nameFeild == null) referenceVariables();

        errorText.style.display = DisplayStyle.None;

        //DatabaseInterection.deletePlayer(10 , success , failer);
        //DatabaseInterection.putPlayer( new Player() , success, failer);
    }

 


    public override void exit()
    {
        //DatabaseInterection.getPlayer(int.Parse(nameFeild.text), success, failer);
    }

    public override void update()
    {
    }

    void switchStateToGameState()
    {
        GameManager.instance.changeStateTo("game");
    }
    void switchStateToLeaderboard()
    {
        GameManager.instance.changeStateTo("leaderboard");
    }
    void switchStateToSettings()
    {
        GameManager.instance.changeStateTo("settings");
    }

    private void onSubmitButtonClicked(ClickEvent evt)
    {
        GameManager.player.id = int.Parse(nameFeild.value);

        DatabaseInterection.getPlayer(GameManager.player.id , getPlayerSuccess, getPlayerFailer);
        StartCoroutine(loginProcess());
        loggingText.style.display = DisplayStyle.Flex;
    }

    IEnumerator loginProcess(int ct = 0)
    {
        yield return new WaitForSeconds(0.5f);
        ct++;
        ct %= 3;
        string s = "";
        for (int i = 0; i < ct; i++) s += ".";
        loggingText.text = "Logging in" + s;
    }

    void getPlayerSuccess(UnityWebRequest request)
    {
        StopCoroutine(loginProcess());
        loggingText.style.display = DisplayStyle.None;
        switchStateToGameState();
    }
    void getPlayerFailer(UnityWebRequest request)
    {
        StopCoroutine(loginProcess());
        loggingText.style.display = DisplayStyle.None;
        errorText.style.display = DisplayStyle.Flex;
        if (request.responseCode == 404)
        {
            errorText.text = "Player not Registered";
            //Debug.Log("Player not Registered");
        }
       /* else if (request.responseCode == 400)
        {
            Debug.Log("Invalid input");
        }*/
        else
        {
            errorText.text = "Server error";
            //Debug.Log("Server error");
        }
    }
    private void onLeaderboardButtonClicked(ClickEvent ext)
    {
        switchStateToLeaderboard();
    }
    private void onSettingsButtonClicked(ClickEvent ext)
    {
        switchStateToSettings();
    }

}


