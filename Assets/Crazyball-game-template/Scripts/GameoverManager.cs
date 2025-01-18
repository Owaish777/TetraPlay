using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Crazyball
{
	public class GameoverManager : MonoBehaviour
	{
		/// <summary>
		/// Gameover manager class.
		/// </summary>

		public static GameoverManager instance;

		//public Text scoreText;                    //gameobject which shows the score on screen
		//public Text bestScoreText;                //gameobject which shows the best saved score on screen

        VisualElement root , gameOverMenu;
		Button home, restart;

		void Start()
		{
            saveScore();

            root = UIManager.root;
            gameOverMenu = root.Q<VisualElement>("GameOverMenu");

			//home = root.Q<VisualElement>()
        }

        private void Awake()
        {
			instance = this;
        }

        public void displayGameOverMenu()
		{
            gameOverMenu.style.display = DisplayStyle.Flex;

            //Set the new score on the screen
            //scoreText.text = PlayerManager.playerScore.ToString();
			//bestScoreText.text = PlayerPrefs.GetInt("bestScore").ToString();
		}



		///***********************************************************************
		/// Save player score
		///***********************************************************************
		void saveScore()
		{
			//immediately save the last score
			PlayerPrefs.SetInt("lastScore", PlayerManager.playerScore);
			//check if this new score is higher than saved bestScore.
			//if so, save this new score into playerPrefs. otherwise keep the last bestScore intact.
			int lastBestScore;
			lastBestScore = PlayerPrefs.GetInt("bestScore");
			if (PlayerManager.playerScore > lastBestScore)
				PlayerPrefs.SetInt("bestScore", PlayerManager.playerScore);
		}

	}
}