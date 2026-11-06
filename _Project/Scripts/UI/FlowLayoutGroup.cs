using UnityEngine;
using UnityEngine.UI;

// Этот компонент заменяет стандартный Layout Group
// Он автоматически переносит элементы на новую строку
[AddComponentMenu("Layout/Flow Layout Group")]
public class FlowLayoutGroup : LayoutGroup
{
    [Header("Отступы между элементами")]
    public float spacingX = 8f;  // горизонтальный отступ между тегами
    public float spacingY = 8f;  // вертикальный отступ между строками

    // Здесь Unity спрашивает: "какой минимальный размер тебе нужен?"
    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        SetLayoutInputForAxis(0, 0, -1, 0);
    }

    public override void CalculateLayoutInputVertical()
    {
        // Считаем высоту которая нам нужна
        float height = CalculateTotalHeight();
        SetLayoutInputForAxis(height, height, -1, 1);
    }

    // Вычисляем полную высоту контейнера
    float CalculateTotalHeight()
    {
        float containerWidth = rectTransform.rect.width
                             - padding.left
                             - padding.right;
        float x = 0f;
        float y = 0f;
        float rowHeight = 0f;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            var child = rectChildren[i];

            float childWidth = LayoutUtility.GetPreferredWidth(child);
            float childHeight = LayoutUtility.GetPreferredHeight(child);

            // Не влезает в строку — переносим
            if (x + childWidth > containerWidth && i > 0)
            {
                y += rowHeight + spacingY;
                x = 0f;
                rowHeight = 0f;
            }

            x += childWidth + spacingX;
            rowHeight = Mathf.Max(rowHeight, childHeight);
        }

        return y + rowHeight + padding.top + padding.bottom;
    }

    // Расставляем элементы по позициям
    public override void SetLayoutHorizontal() { }

    public override void SetLayoutVertical()
    {
        float containerWidth = rectTransform.rect.width
                             - padding.left
                             - padding.right;

        float x = padding.left;
        float y = padding.top;
        float rowHeight = 0f;

        // Сначала определяем высоты строк
        for (int i = 0; i < rectChildren.Count; i++)
        {
            var child = rectChildren[i];

            float childWidth = LayoutUtility.GetPreferredWidth(child);
            float childHeight = LayoutUtility.GetPreferredHeight(child);

            // Перенос на новую строку
            if (x + childWidth > containerWidth + padding.left && i > 0)
            {
                y += rowHeight + spacingY;
                x = padding.left;
                rowHeight = 0f;
            }

            // Устанавливаем позицию элемента
            SetChildAlongAxis(child, 0, x, childWidth);
            SetChildAlongAxis(child, 1, y, childHeight);

            x += childWidth + spacingX;
            rowHeight = Mathf.Max(rowHeight, childHeight);
        }
    }
}