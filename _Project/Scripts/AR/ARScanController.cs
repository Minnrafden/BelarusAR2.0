using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARScanController : MonoBehaviour
{
    [Header("AR компоненты")]
    public ARPlaneManager planeManager;

    [Header("UI элементы")]
    public UnityEngine.UI.Image progressBarFill;
    public RectTransform progressBarBG;
    public TextMeshProUGUI progressLabel;
    public TextMeshProUGUI progressPercent;
    public RectTransform scanLine;
    public CanvasGroup bottomPanel;

    [Header("Настройки")]
    [Tooltip("Площадь в м² для 100%")]
    public float targetArea = 0.3f;

    private float progress = 0f;
    private bool isDone = false;
    private RectTransform progressFillRect;

    void Start()
    {
        progressFillRect = progressBarFill.GetComponent<RectTransform>();

        // Прогресс-бар начинается пустым
        progressFillRect.localScale = new Vector3(0f, 1f, 1f);

        // Нижняя панель скрыта
        bottomPanel.alpha = 0f;

        // Анимация появления нижней панели
        DOTween.Sequence()
            .AppendInterval(0.5f)
            .Append(bottomPanel.DOFade(1f, 0.4f))
            .Join(bottomPanel.transform
                .DOMoveY(bottomPanel.transform.position.y, 0.4f)
                .SetRelative(true)
                .SetEase(Ease.OutCubic));

        AnimateScanLine();
    }

    void AnimateScanLine()
    {
        if (scanLine == null) return;

        // Начинаем сверху, движемся вниз и повторяем
        float canvasHeight = GetComponentInParent<Canvas>()
            .GetComponent<RectTransform>().rect.height;

        scanLine.anchoredPosition = new Vector2(0f, canvasHeight * 0.4f);

        scanLine.DOAnchorPosY(-canvasHeight * 0.4f, 2f)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.InOutSine);
    }

    void Update()
    {
        if (isDone) return;

        // Считаем площадь обнаруженных AR плоскостей
        float area = 0f;
        foreach (var plane in planeManager.trackables)
            area += plane.size.x * plane.size.y;

        progress = Mathf.Clamp01(area / targetArea);

        // Обновляем прогресс-бар через Scale (pivot должен быть X=0)
        progressFillRect.localScale = new Vector3(progress, 1f, 1f);

        // Обновляем процент
        int pct = Mathf.RoundToInt(progress * 100f);
        progressPercent.text = $"{pct}%";

        // Меняем текст в зависимости от прогресса
        if (pct < 30)
            progressLabel.text = "Сканирование стены..";
        else if (pct < 70)
            progressLabel.text = "Обнаружена поверхность..";
        else if (pct < 100)
            progressLabel.text = "Почти готово..";
        else
        {
            isDone = true;
            OnScanComplete();
        }
    }

    void OnScanComplete()
    {
        progressLabel.text = "Поверхность сканирована!";
        progressPercent.text = "100%";
        progressPercent.color = UIColors.Success;

        //DOTween.Sequence()
        //    .AppendInterval(0.8f)
        //    .AppendCallback(() => SceneManager.LoadScene("03_Map"));
    }

    public void OnSkipPressed() => SceneManager.LoadScene("MinskRegionInfoScene");
    public void OnBackPressed() => SceneManager.LoadScene("00_Splash");
}