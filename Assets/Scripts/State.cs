using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using Unity.VisualScripting;

public abstract class State : MonoBehaviour
{

    public VisualElement screen;
    public abstract void enter();
    public abstract void exit();

    public abstract void update();

    public IEnumerator makeScreenVisible()
    {
        float elapsedTime = 0f;
        float duration = GameManager.transitionDuration;

        while (elapsedTime < duration)
        {
            
            float alpha = (elapsedTime / duration);
            screen.style.opacity = alpha;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        screen.style.opacity = 1f;
    }

    public IEnumerator makeScreenInvisible()
    {
        float elapsedTime = 0f;
        float duration = GameManager.transitionDuration;

        while (elapsedTime < duration)
        {
            float alpha = 1f - (elapsedTime / duration);
            screen.style.opacity = alpha;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        screen.style.opacity = 0f;
    }
}
