using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Crazyball
{
	public class PauseManager : MonoBehaviour
	{
		/// <summary>
		/// This class manages pause and unpause states.
		/// </summary> 


		public static bool isPaused;        //is game already paused?

		public enum Page { PLAY, PAUSE }
		private Page currentPage = Page.PLAY;

		[SerializeField] UIDocument document;


        VisualElement root, pauseMenu;

		Button play, home;

        private void Start()
        {
			root = document.rootVisualElement.Q<VisualElement>("GameUI");
			pauseMenu = root.Q<VisualElement>("PauseMenu");

			play = root.Q<Button>("Play_PauseMenu");
			play.RegisterCallback<ClickEvent>(onPlayButtonClicked);

			home = root.Q<Button>("Home_PauseMenu");
			home.RegisterCallback<ClickEvent>(onHomeButtonClicked);
        }

        void Awake()
		{
			isPaused = false;
			Time.timeScale = 1.0f;
		}


		void Update()
		{
			//optional pause
			if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyUp(KeyCode.Escape))
			{
				//PAUSE THE GAME
				switch (currentPage)
				{
					case Page.PLAY:
						PauseGame();
						break;
					case Page.PAUSE:
						UnPauseGame();
						break;
					default:
						currentPage = Page.PLAY;
						break;
				}
			}

			//debug restart
			if (Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}

		void onPlayButtonClicked(ClickEvent evt)
		{
			UnPauseGame();
		}

		void onHomeButtonClicked(ClickEvent evt)
		{
            StartCoroutine(LoadSceneAsync("Menu_CrazyBall"));
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


        public void PauseGame()
		{
			if (GameController.gameOver)
				return;

			print("Game is Paused...");
			isPaused = true;
			Time.timeScale = 0;
			AudioListener.volume = 0;

			pauseMenu.style.display = DisplayStyle.Flex;

			currentPage = Page.PAUSE;
		}

		public void UnPauseGame()
		{
			print("Unpause");
			isPaused = false;
			Time.timeScale = 1.0f;
			AudioListener.volume = 1.0f;

            pauseMenu.style.display = DisplayStyle.None;
            
			currentPage = Page.PLAY;
		}

	}
}