using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Crazyball
{
    public class UIManager : MonoBehaviour
    {
        //[SerializeField] GameObject game;


        //VisualElement mainMenu , gameUI;

        Button startButton;

        [HideInInspector]
        public UIDocument document;
        public static VisualElement root;


        void Start()
        {
            document = GetComponent<UIDocument>();
            root = document.rootVisualElement;

            startButton = root.Q<Button>("PlayButton");
            startButton.RegisterCallback<ClickEvent>(onStartButtonPressed);

        }

        void onStartButtonPressed(ClickEvent ext)
        {
            //SceneManager.scene
            //mainMenu.style.display = DisplayStyle.None;
            //gameUI.style.display = DisplayStyle.Flex;

            //game.SetActive(true);

            StartCoroutine(LoadSceneAsync("Game_CrazyBall"));

            //GameController.instance.setValues();
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
    }

}
