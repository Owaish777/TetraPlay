using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crazyball
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject game;

        UIDocument document;
        VisualElement root , mainMenu , gameUI;

        Button startButton;
        
        void Start()
        {
            document = GetComponent<UIDocument>();
            root = document.rootVisualElement;


            startButton = root.Q<Button>("StartButton");
            startButton.RegisterCallback<ClickEvent>(onStartButtonPressed);

            
            mainMenu = root.Q<VisualElement>("MainMenu");
            gameUI = root.Q<VisualElement>("GameUI");

        }

        void onStartButtonPressed(ClickEvent ext)
        {
            mainMenu.style.display = DisplayStyle.None;
            gameUI.style.display = DisplayStyle.Flex;

            game.SetActive(true);
            //Debug.Log(GameController.instance);

            GameController.instance.setValues();
        }
    }

}
