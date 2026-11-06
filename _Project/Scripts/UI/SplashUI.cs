using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using System.Collections;

public class SplashUI : MonoBehaviour
{
    [Header("Анимируемые группы")]
    public CanvasGroup logoGroup;
    public CanvasGroup titleGroup;
    public CanvasGroup tagsGroup;
    public CanvasGroup buttonsGroup;

    [Header("Кнопки")]
    public Button scanButton;
    public Button skipButton;

    [Header("Теги")]
    public GameObject tagPrefab;
    public RectTransform tagsContainer;
    public string[] tagNames = { "AR просмотр", "3D модели", "Аудиогид", "Оффлайн" };

    void Start()
    {
        // Создаём теги
        foreach (var tag in tagNames)
        {
            var t = Instantiate(tagPrefab, tagsContainer);
            t.GetComponentInChildren<TextMeshProUGUI>().text = tag;
        }

        // Скрываем через Alpha
        logoGroup.alpha = 0f;
        titleGroup.alpha = 0f;
        tagsGroup.alpha = 0f;
        buttonsGroup.alpha = 0f;

        StartCoroutine(PlayIntro());

        scanButton.onClick.AddListener(OnScanPressed);
        skipButton.onClick.AddListener(OnSkipPressed);
    }

    IEnumerator PlayIntro()
    {
        yield return new WaitForSeconds(0.3f);

        yield return logoGroup.DOFade(1f, 0.6f).WaitForCompletion();
        yield return titleGroup.DOFade(1f, 0.5f).WaitForCompletion();
        yield return tagsGroup.DOFade(1f, 0.4f).WaitForCompletion();
        yield return buttonsGroup.DOFade(1f, 0.4f).WaitForCompletion();
    }

    public void OnScanPressed()
    {
        scanButton.transform.DOScale(0.95f, 0.1f)
            .OnComplete(() =>
            {
                scanButton.transform.DOScale(1f, 0.1f);
                SceneManager.LoadScene("02_ARScanning");
            });
    }

    public void OnSkipPressed()
    {
        SceneManager.LoadScene("MinskRegionInfoScene");
    }
}