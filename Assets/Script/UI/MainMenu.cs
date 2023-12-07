using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Transform gameLogo;
    [SerializeField]
    private Transform tutorPanel;
    [SerializeField]
    private Transform guideLine;


    private void Start()
    {
        tutorPanel.gameObject.SetActive(false);

        gameLogo.GetComponent<CanvasGroup>().alpha = 0f;
        gameLogo.GetComponent<CanvasGroup>().DOFade(1, 2f).SetUpdate(true);
        
    }

    public void ShowTutorPanel()
    {
        tutorPanel.gameObject.SetActive(true);
        guideLine.gameObject.SetActive(true);
        FadeIn(tutorPanel.GetComponent<CanvasGroup>(), guideLine.GetComponent<RectTransform>());

    }

    public void HideTutorPanel()
    {
        StartCoroutine(FadeOut(tutorPanel.GetComponent<CanvasGroup>(), guideLine.GetComponent<RectTransform>()));

    }   

    private void FadeIn(CanvasGroup canvasGroup ,RectTransform rectTransform)
    {
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1, .3f).SetUpdate(true);

        rectTransform.anchoredPosition = new Vector3(1280, 0, 0);
        rectTransform.DOAnchorPos(new Vector2(0, 0), .3f, false).SetEase(Ease.OutQuint).SetUpdate(true);
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup, RectTransform rectTransform)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0, .3f).SetUpdate(true);

        rectTransform.anchoredPosition = new Vector3(0, 0, 0);
        rectTransform.DOAnchorPos(new Vector2(-1280, 0), .3f, false).SetEase(Ease.OutQuint).SetUpdate(true);

        yield return new WaitForSecondsRealtime(.3f);
        guideLine.gameObject.SetActive(true);
        tutorPanel.gameObject.SetActive(false);
    }

}
