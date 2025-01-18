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
		public GameObject pausePlane;       //we move this plane over all other elements in the game to simulate the pause

		public enum Page { PLAY, PAUSE }
		private Page currentPage = Page.PLAY;


        VisualElement root, pauseMenu;

        private void Start()
        {
			root = UIManager.root;
			pauseMenu = root.Q<VisualElement>("PauseMenu");
        }

        void Awake()
		{
			isPaused = false;
			Time.timeScale = 1.0f;
			if (pausePlane)
				pausePlane.SetActive(false);
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