using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class LeaderboardState : State
{
    public static List<PlayerScore> leaderboardEntries = new List<PlayerScore>();


    Button home;
    public Label[] entryName = new Label[10];
    public Label[] entryScore = new Label[10];
    public Label[] rank = new Label[10];
    public Label[] id = new Label[10];


    void Start()
    {
        
        //Referencing the variables.
        for (int i = 0; i < 10; i++)
        {
            entryName[i] = screen.Q<Label>(i.ToString());
            entryScore[i] = screen.Q<Label>(i.ToString() + "s");
            rank[i] = screen.Q<Label>((i + 1).ToString() + "a");
            id[i] = screen.Q<Label>(i.ToString() + "n");
        }

        //If data file not exist , then create one with header
        if (!File.Exists(GameManager.filePath))
        {
            string seperator = ",";
            string[] data = { "Name", "Score"};
            string savsString = string.Join(seperator, data) + "\n";
            File.AppendAllText(GameManager.filePath, savsString);
        }

        //If restData is true , then reset the data
        if (GameManager.resetdata)
        {
            string[] lines = File.ReadAllLines(GameManager.filePath);
            string header = lines[0];
            File.WriteAllText(GameManager.filePath, header + "\n");
        }
    }

    //Runs at the start of the state
    public override void enter()
    {
        home = screen.Q<Button>("Home");
        home.RegisterCallback<ClickEvent>(onHomeButtonClicked);

        

        //funtion to show the loaded data
        showEntries();

    }

    private void onHomeButtonClicked(ClickEvent evt)
    {
        switchStateToHomeState();
    }

    //Runs before the exit of the state.
    public override void exit()
    {
    }

    public override void update()
    {   
    }

    void switchStateToHomeState()
    {
        GameManager.instance.changeStateTo("home");
    }

    //Function to show the data
    void showEntries()
    {
        for (int i = 0; i < leaderboardEntries.Count; i++)
        {
            PlayerScore entry = leaderboardEntries[i];

            entryName[i].text = entry.playerID.ToString();
            entryScore[i].text = entry.score.ToString();
            rank[i].text = (i + 1).ToString() + ")";
        }
    }

}

