using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class SettingsState : State
{
    TextField portName;
    TextField buadRate;
    Button home;

    string filePath = Path.GetDirectoryName(Application.dataPath) + "/Arduino Settings.csv";
    string header = "Port Name,Baud Rate\n";

    void Start()
    {
        //If data file not exist , then create one with header
        if (!File.Exists(filePath))
        {
            File.AppendAllText(filePath, header);
        }
        else
        {

            string[] lines = File.ReadAllLines(filePath);

            if(lines.Length > 1)
            {
                string[] values = lines[1].Split(',');

                GameManager.portName = values[0];
                GameManager.baudRate = int.Parse(values[1]);
            }
            
        }
    }


    public override void enter()
    {
        portName = screen.Q<TextField>("PortName");
        buadRate = screen.Q<TextField>("BuadRate");

        home = screen.Q<Button>("Back");

        home.RegisterCallback<ClickEvent>(onHomeButtonClicked);

        portName.value = GameManager.portName;
        buadRate.value = GameManager.baudRate.ToString();
 
    }
    public override void exit()
    {
        GameManager.portName = portName.text;
        GameManager.baudRate = int.Parse(buadRate.text);

        string seperator = ",";
        string[] data = { portName.text, buadRate.text };
        string savsString = string.Join(seperator, data) + "\n";
        File.WriteAllText(filePath, header + savsString);
    }

    public override void update()
    {
        
    }

    private void onHomeButtonClicked(ClickEvent evt)
    {
        GameManager.instance.changeStateTo("home");
    }
}