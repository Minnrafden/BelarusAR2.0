// Добавь компонент на GlowCircle
using UnityEngine;
using UnityEngine.UI;

public class GlowEffect : MonoBehaviour
{
    private Image img;

    void Start()
    {
        img = GetComponent<Image>();
        // Анимация пульсации
        StartCoroutine(Pulse());
    }

    System.Collections.IEnumerator Pulse()
    {
        while (true)
        {
            // Увеличиваем
            float t = 0;
            while (t < 1f)
            {
                t += Time.deltaTime * 0.8f;
                float scale = Mathf.Lerp(1f, 1.15f, t);
                transform.localScale = Vector3.one * scale;

                Color c = img.color;
                c.a = Mathf.Lerp(0.6f, 0.3f, t);
                img.color = c;
                yield return null;
            }
            // Уменьшаем
            t = 0;
            while (t < 1f)
            {
                t += Time.deltaTime * 0.8f;
                float scale = Mathf.Lerp(1.15f, 1f, t);
                transform.localScale = Vector3.one * scale;

                Color c = img.color;
                c.a = Mathf.Lerp(0.3f, 0.6f, t);
                img.color = c;
                yield return null;
            }
        }
    }
}