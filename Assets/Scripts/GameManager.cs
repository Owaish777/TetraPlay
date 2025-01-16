 using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.IO;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public float _maxTime = 60;
    public float _transitionDuration = 1f;
    public float _delayTime = 2f;
    public float _timeOut = 60f;
    public float time = 60f;
    public int score = 0;
    //
    static public float maxTime;
    static public float transitionDuration;
    static public float delayTime;
    static public float timeOut;

    public string apiUrl;


    [Header("LeaderBoard Settings")]
    //Leaderboard values
    public int _maxCount = 10;
    public bool _resetdata = false;
    //
    public static int maxCount = 10;
    public static bool resetdata = false;

    [Header("Saved data file name")]
    public string _fileName = "Saved data";
    //
    //public static string fileName;
    public static string filePath = Path.GetDirectoryName(Application.dataPath);

    [Header("Arduino settings")]
    public string _portName = "COM10";
    public int _baudRate = 115200;
    //
    public static string portName;
    public static int baudRate;

   

    //UI Elements
    UIDocument document;
    public VisualElement root , homeScreen , gameScreen , leaderboardScreen , settingsScreen;
    

    State[] state;
    State currentState;
    private Dictionary<string, int> nameToIdx = new Dictionary<string, int>();
    
    public static Player player;
    
    
    public static GameManager instance;

    private void Start()
    {
        maxTime = _maxTime;
        transitionDuration = _transitionDuration;
        delayTime = _delayTime;
        timeOut = _timeOut;

        maxCount = _maxCount;
        resetdata = _resetdata;

        filePath += "/" + _fileName + ".csv";
        portName = _portName;
        baudRate = _baudRate;

        DatabaseInterection.apiUrl = apiUrl;
    }


    void Awake()
    {

        instance = this;


        //Initializing the states
        state = new State[4];
        nameToIdx.Add("settings", 0);
        nameToIdx.Add("home", 1);
        nameToIdx.Add("game", 2);
        nameToIdx.Add("leaderboard", 3);


        document = GetComponent<UIDocument>();
        root = document.rootVisualElement;

        //SettingsScreen setup
        settingsScreen = root.Q<VisualElement>("SettingsScreen");
        SettingsState settingsState = gameObject.AddComponent<SettingsState>();
        settingsState.screen = settingsScreen;
        state[0] = settingsState;
        //

        //gameScreen setup
        gameScreen = root.Q<VisualElement>("GameScreen");
        GameState gameState = gameObject.AddComponent<GameState>();
        gameState.screen = gameScreen;
        state[2] = gameState;
        //

        //HomeScreen setup
        homeScreen = root.Q<VisualElement>("HomeScreen");
        HomeState homeState = gameObject.AddComponent<HomeState>();
        homeState.screen = homeScreen;
        state[1] = homeState;

        homeState.enter();
        currentState = homeState;
        //

        //LeaderBoard setup
        leaderboardScreen = root.Q<VisualElement>("LeaderBoard");
        LeaderboardState leaderboardState = gameObject.AddComponent<LeaderboardState>();
        leaderboardState.screen = leaderboardScreen;
        state[3] = leaderboardState;
        //

        maxTime++;
        score = 0;
        time = maxTime;
    }

    public void changeStateTo(string newState)
    {
        currentState.screen.style.display = DisplayStyle.None;
        currentState.exit();

        currentState = state[nameToIdx[newState]];

        currentState.enter();
        currentState.screen.style.display = DisplayStyle.Flex;
        StartCoroutine(currentState.makeScreenVisible());
    }

    public void initializeGameValues()
    {
        score = 0;
        time = maxTime;
        
    }

    private void Update()
    {
        currentState.update();
    }

    public void hideSettingButton()
    {
        root.Q<Button>("Settings").style.display = DisplayStyle.None;
    }
}

[System.Serializable]
public struct Player
{
    public int id;
    public string name;
    public string email;

    public Player(int id ,  string name, string email)
    {
        this.id = id;
        this.name = name;
        this.email = email;
    }
}


[System.Serializable]
public struct PlayerScore
{
    public int playerID;
    public int score;

    public PlayerScore(int playerID, int score)
    {
        this.playerID = playerID;
        this.score = score;
    }
}

/*[System.Serializable]
public class ResponseObject
{
    public string error;   // For error messages
    public string message; // For success messages
}*/