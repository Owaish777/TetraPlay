using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.UIElements;

namespace Crazyball
{
	public class PlayerScoreManager : MonoBehaviour
	{
		/// <summary>
		/// Main Player Score manager class.
		/// We calculate the score in this class. 
		/// </summary>
		public static int playerScore;          //player score
												//public Text scoreTextDynamic;     //gameobject which shows the score on screen

		[SerializeField] UIDocument document;

		VisualElement root;
		Label scoreText;

        private void Start()
        {
			root = document.rootVisualElement.Q<VisualElement>("GameUI");
            scoreText = root.Q<Label>("ScoreText");
        }

        void Awake()
		{
			playerScore = 0;
			//Disable screen dimming on mobile devices
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			//Playe the game with a fixed framerate in all platforms
			Application.targetFrameRate = 60;

			
		}


		void Update()
		{
			if (!GameController.gameOver)
				calculateScore();
		}


		///***********************************************************************
		/// calculate player's score
		/// Score is a combination of gameplay duration (while player is still alive) 
		/// and a multiplier for the current level.
		///***********************************************************************
		void calculateScore()
		{
			if (!PauseManager.isPaused)
			{
				playerScore += (int)(GameController.currentLevel * Mathf.Log(GameController.currentLevel + 1, 2));
				scoreText.text = "" + playerScore.ToString();
			}
		}




		void playSfx(AudioClip _sfx)
		{
			GetComponent<AudioSource>().clip = _sfx;
			if (!GetComponent<AudioSource>().isPlaying)
				GetComponent<AudioSource>().Play();
		}
	}
}