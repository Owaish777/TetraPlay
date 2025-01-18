using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crazyball
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject game;


        VisualElement mainMenu , gameUI;

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

            
            mainMenu = root.Q<VisualElement>("MainMenu");
            gameUI = root.Q<VisualElement>("GameUI");

        }

        void onStartButtonPressed(ClickEvent ext)
        {
            mainMenu.style.display = DisplayStyle.None;
            gameUI.style.display = DisplayStyle.Flex;

            game.SetActive(true);

            GameController.instance.setValues();
        }
    }

}
