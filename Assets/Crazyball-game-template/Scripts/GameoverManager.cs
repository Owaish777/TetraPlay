using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Unity.VisualScripting;

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
		[SerializeField] UIDocument document;

        VisualElement root , gameOverMenu;
		Button home, restart;

		void Start()
		{
            saveScore();

            root = document.rootVisualElement;
            gameOverMenu = root.Q<VisualElement>("GameOverMenu");

			home = root.Q<Button>("Home_GameOverMenu");
            home.RegisterCallback<ClickEvent>(onHomeButtonClicked);

            restart = root.Q<Button>("Restart_GameOverMenu");
            restart.RegisterCallback<ClickEvent>(onRestartButtonClicked);
        }

        private void Awake()
        {
			instance = this;
        }

		void onHomeButtonClicked(ClickEvent evt)
		{
			StartCoroutine(LoadSceneAsync("Menu_CrazyBall"));
		}

		void onRestartButtonClicked(ClickEvent evt)
		{
            StartCoroutine(LoadSceneAsync("Game_CrazyBall"));
        }

        IEnumerator LoadSceneAsync(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                Debug.Log("Loading progress: " + (progress * 100) + "%");
                yield return null;
            }
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
			/*//immediately save the last score
			PlayerPrefs.SetInt("lastScore", PlayerScoreManager.playerScore);
			//check if this new score is higher than saved bestScore.
			//if so, save this new score into playerPrefs. otherwise keep the last bestScore intact.
			int lastBestScore;
			lastBestScore = PlayerPrefs.GetInt("bestScore");
			if (PlayerManager.playerScore > lastBestScore)
				PlayerPrefs.SetInt("bestScore", PlayerManager.playerScore);*/
		}

	}
}