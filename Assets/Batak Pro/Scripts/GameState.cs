using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
//using System.IO;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
//using System.IO.Ports;
using System;
using UnityEngine.Networking;

public class GameState : State
{


    bool isRunning = false;
    bool isActive = false;
    public string dataFilePath;
    public Label timerLabel, scoreLabel;

    //SerialPort
    //SerialPort serialPort;

    private void Start()
    {
        timerLabel = screen.Q<Label>("Timer");
        scoreLabel = screen.Q<Label>("Score");

    }

    public override void enter()
    {
        

        GameManager.instance.initializeGameValues();
        timerLabel.text = (GameManager.instance.time - 1).ToString();
        scoreLabel.text = GameManager.instance.score.ToString();


        /*serialPort = new SerialPort(GameManager.portName, GameManager.baudRate);
        try
        {
            serialPort.Open();
        }
        catch
        {
        }*/
    }
    public override void exit()
    {
        //GameManager.player.score = GameManager.instance.score;

        //DatabaseInterection.postPlayerScore(GameManager.player.playerID, GameManager.instance.score, success, failer);

        //CustomDatabaseInterection.postPlayerScore(GameManager.player.playerID, GameManager.instance.score);


        isRunning = false;
        isActive = false;
    }

    void success(UnityWebRequest request)
    {
        Debug.Log("player score uploaded");
    }
    void failer(UnityWebRequest request)
    {
        Debug.Log("failer");
    }


    public override void update()
    {
        /*if (serialPort.IsOpen && serialPort.BytesToRead > 0)
        {
            int c = serialPort.ReadChar();

            if (c == '1')
            {
                increaseScore();
            }
            if (c == 'r')
            {
                resetGame();
            }
            if(c == 's')
            {
                startGame();
            }
        }*/

        if (Input.GetKeyDown(KeyCode.S))
        {
            increaseScore();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetGame();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            startGame();
        }
    }

    void increaseScore()
    {
        GameManager.instance.score++;
        scoreLabel.text = GameManager.instance.score.ToString();
    }
    void resetGame()
    {
        if (!isActive)
        {
            if (isRunning) StopCoroutine(startTimer());
            GameManager.instance.changeStateTo("home");
        }
    }
    void startGame()
    {
        if(!isRunning) StartCoroutine(startTimer());
    }

    IEnumerator startTimer()
    {
        isRunning = true;
        isActive = true;

        timerLabel.style.visibility = Visibility.Visible;

        while (GameManager.instance.time > 1)
        {
            GameManager.instance.time -= Time.deltaTime;
            timerLabel.text = ((int)GameManager.instance.time).ToString();
            yield return null;
        }

        timerLabel.style.visibility = Visibility.Hidden;
        isActive = false;
        yield return new WaitForSeconds(GameManager.timeOut);
        isRunning = false;
        resetGame();
    }
}
