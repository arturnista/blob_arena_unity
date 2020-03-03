using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPauseCanvas : MonoBehaviour
{

    public float TimeToShow;
    public GameObject First;

    private CanvasGroup canvasGroup;
    
    void OnEnable()
    {
        EventSystem eventSystem = GameObject.FindObjectOfType<EventSystem>();
        eventSystem.SetSelectedGameObject(First);

        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        float timeRate = 1f / TimeToShow;
        float alpha = 0f;
        canvasGroup.alpha = alpha;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * timeRate;
            canvasGroup.alpha = alpha;
            yield return null;
        }
    }

}
