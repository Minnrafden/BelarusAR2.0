using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AutoWallPlacement : MonoBehaviour
{
    [Header("Настройки модели")]
    public GameObject mapPrefab;
    public float mapScale = 0.1f;

    [Header("Настройки позиции (в метрах)")]
    public float heightOffset = 1.2f;    // Вверх
    public float forwardOffset = 0.5f; // Вперед от стены (чтобы не мерцала)
    public float leftOffset = 1.2f;    // Влево от центра

    private GameObject spawnedMap;
    private ARPlaneManager planeManager;

    void Awake() => planeManager = GetComponent<ARPlaneManager>();

    void OnEnable() => planeManager.planesChanged += OnPlanesChanged;
    void OnDisable() => planeManager.planesChanged -= OnPlanesChanged;

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        // 1. Создаем карту, если нашли вертикальную стену
        foreach (var plane in args.added)
        {
            if (spawnedMap == null && plane.alignment == PlaneAlignment.Vertical)
            {
                spawnedMap = Instantiate(mapPrefab);
                spawnedMap.transform.localScale = Vector3.one * mapScale;
            }
        }

        // 2. Постоянно обновляем её положение вслед за стеной
        foreach (var plane in args.updated)
        {
            if (spawnedMap != null && plane.alignment == PlaneAlignment.Vertical)
            {
                UpdateMapTransform(plane);
            }
        }
    }

    private void UpdateMapTransform(ARPlane plane)
    {
        // Вычисляем направления относительно стены
        Vector3 wallNormal = plane.normal; // Смотрит ИЗ стены
        Vector3 wallLeft = Vector3.Cross(plane.normal, Vector3.up); // Направление "влево"

        // Рассчитываем итоговую позицию
        Vector3 finalPos = plane.center;
        finalPos += Vector3.up * heightOffset;      // Поднимаем
        finalPos += wallNormal * forwardOffset;    // Выдвигаем вперед
        finalPos += wallLeft * leftOffset;         // Сдвигаем влево

        spawnedMap.transform.position = finalPos;

        // Поворот: лицом к пользователю
        if (wallNormal != Vector3.zero)
        {
            spawnedMap.transform.rotation = Quaternion.LookRotation(-wallNormal, Vector3.up);
        }
    }
}