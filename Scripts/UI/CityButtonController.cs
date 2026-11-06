using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CityButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Настройки кнопки")]
    public Color normalColor = Color.blue;
    public Color hoverColor = Color.red;
    public Color selectedColor = Color.green;

    [Header("Ссылки")]
    public Image buttonImage;
    public Image cityIcon;

    private bool isSelected = false;

    void Start()
    {
        if (buttonImage == null)
            buttonImage = GetComponent<Image>();

        if (buttonImage != null)
            buttonImage.color = normalColor;
    }

    // При наведении мыши
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected && buttonImage != null)
            buttonImage.color = hoverColor;
    }

    // При уходе мыши
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected && buttonImage != null)
            buttonImage.color = normalColor;
    }

    // При выборе города
    public void SelectCity()
    {
        isSelected = true;
        if (buttonImage != null)
            buttonImage.color = selectedColor;
    }

    // Сброс выбора
    public void DeselectCity()
    {
        isSelected = false;
        if (buttonImage != null)
            buttonImage.color = normalColor;
    }
}