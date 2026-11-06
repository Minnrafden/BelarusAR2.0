using UnityEngine;

public class FixZPositions : MonoBehaviour
{
    [ContextMenu("Fix All Z Positions")]
    void FixAll()
    {
        var all = GetComponentsInChildren<RectTransform>();
        foreach (var rt in all)
        {
            Vector3 pos = rt.localPosition;
            pos.z = 0f;
            rt.localPosition = pos;
        }
        Debug.Log($"Исправлено {all.Length} объектов!");
    }
}