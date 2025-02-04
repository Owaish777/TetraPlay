using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crazyball
{
    public class GameUIManager : MonoBehaviour
    {
        [HideInInspector]
        public UIDocument document;
        public static VisualElement root;

        private void Awake()
        {
            document = GetComponent<UIDocument>();
            root = document.rootVisualElement;
        }

    }

}
