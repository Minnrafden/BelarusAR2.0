using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class PlaceObjectOnPlane : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject objectToPlace;
    public ARObjectSaver objectSaver;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject lastPlacedObject;
    private Camera arCamera;

    void Start()
    {
        arCamera = Camera.main;

        if (arCamera == null)
        {
            Debug.LogError("AR камера не найдена!");
            enabled = false;
            return;
        }

        // Проверка поддержки AR
        if (ARSession.state == ARSessionState.Unsupported)
        {
            Debug.LogError("AR не поддерживается на этом устройстве!");
            enabled = false;
            return;
        }

        // Автопоиск компонентов если не назначены
        if (raycastManager == null)
            raycastManager = FindFirstObjectByType<ARRaycastManager>();

        Debug.Log("AR готов к работе. Состояние: " + ARSession.state);
    }

    void Update()
    {
        // Ждём стабильного трекинга
        if (ARSession.state != ARSessionState.SessionTracking)
            return;

        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        // Проверяем, попали ли в куб
        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = arCamera.ScreenPointToRay(touch.position);
            RaycastHit hit;

            // Проверка: если попали в куб, НЕ создаём новый
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Cube"))
                {
                    // Попали в куб - передаём управление скрипту вращения
                    Debug.Log("Тап по кубу - режим вращения");
                    return;
                }
            }

            // Если не попали в куб, проверяем плоскость
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                lastPlacedObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
                lastPlacedObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                // Настройка Rigidbody для предотвращения багов
                Rigidbody rb = lastPlacedObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                    rb.interpolation = RigidbodyInterpolation.Interpolate;
                }

                // Логирование в консоль
                Debug.Log($"Куб размещён на позиции: {hitPose.position}");

                // Проверка столкновения с другими кубами
                CheckStacking(lastPlacedObject);

                // Сохранение объекта
                if (objectSaver != null)
                {
                    objectSaver.AddObject(lastPlacedObject);
                }
            }
        }
    }

    void CheckStacking(GameObject newCube)
    {
        Collider[] colliders = Physics.OverlapBox(newCube.transform.position,
            newCube.transform.localScale / 2);

        foreach (Collider col in colliders)
        {
            if (col.gameObject != newCube && col.CompareTag("Cube"))
            {
                Debug.Log("Куб поставлен на другой куб!");
                // Можно добавить эффект или звук
                break;
            }
        }
    }
}