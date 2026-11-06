using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

[RequireComponent(typeof(ARRaycastManager))]
public class ARModelSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct LandmarkModel
    {
        public string attractionName;
        public GameObject modelPrefab;
    }

    public List<LandmarkModel> modelsLibrary;
    private GameObject spawnedObject;
    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        //DataBridge.Start();
        raycastManager = GetComponent<ARRaycastManager>();
        Debug.Log($"[AR] Система готова. Ждем: {DataBridge.SelectedAttraction}");
    }

    void Update()
    {
        Vector2 inputPosition = Vector2.zero;
        bool isInputDetected = false;

        // 1. Проверка ТАПА (Телефон)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                inputPosition = touch.position;
                isInputDetected = true;
            }
        }
        // 2. Проверка МЫШКИ (Ноут)
        else if (Input.GetMouseButtonDown(0))
        {
            inputPosition = Input.mousePosition;
            isInputDetected = true;
            Debug.Log("[AR] Тест на ноуте: Клик мышкой");
        }
        // 3. Проверка клавиши R (Ноут)
        else if (Input.GetKeyDown(KeyCode.R))
        {
            inputPosition = new Vector2(Screen.width / 2, Screen.height / 2);
            isInputDetected = true;
            Debug.Log("[AR] Тест на ноуте: Нажата R (центр экрана)");
        }

        if (isInputDetected)
        {
            HandlePlacement(inputPosition);
        }
    }

    void HandlePlacement(Vector2 pos)
    {
        if (raycastManager.Raycast(pos, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (spawnedObject == null)
            {
                string currentAttraction = DataBridge.SelectedAttraction;
                GameObject prefab = modelsLibrary.Find(x => x.attractionName == currentAttraction).modelPrefab;

                if (prefab != null)
                {
                    spawnedObject = Instantiate(prefab, hitPose.position, hitPose.rotation);
                    Debug.Log($"<color=green>[AR] УСПЕХ!</color> '{currentAttraction}' создана. Используй стандартные жесты для управления.");
                }
                else
                {
                    Debug.LogError($"[AR] Модель для '{currentAttraction}' не найдена в списке!");
                }
            }
            else
            {
                // Если объект уже есть, перемещаем его в новую точку касания
                spawnedObject.transform.position = hitPose.position;
                Debug.Log("[AR] Модель перемещена.");
            }
        }
    }
}