using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AppManager : MonoBehaviour
{
    // Состояния приложения
    public enum AppState { Menu, PlacingMap, MapInteraction, PlacingObject, ViewingObject }
    [Header("Current Status")]
    public AppState currentState = AppState.Menu;

    [Header("AR Components")]
    public ARRaycastManager raycastManager;
    public ARAnchorManager anchorManager;
    public ARPlaneManager planeManager;
    public Camera arCamera;

    [Header("Prefabs")]
    public GameObject mapPrefab;          // Префаб всей карты Беларуси
    public GameObject reticlePrefab;      // Префаб кружка (курсора)
    private GameObject spawnedReticle;
    private GameObject spawnedMap;
    private GameObject currentAttraction; // Текущая выбранная достопримечательность

    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject cityMenuPanel;
    public GameObject attractionMenuPanel;
    public GameObject scanHintPanel;
    public Text hintText;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject attractionToPlace; // Временная переменная для выбранного префаба

    void Start()
    {
        // Создаем курсор заранее, но скрываем его
        spawnedReticle = Instantiate(reticlePrefab);
        spawnedReticle.SetActive(false);

        ShowPanel(mainMenuPanel);
    }

    void Update()
    {
        // Логика отображения кружка (курсора) на полу
        if (currentState == AppState.PlacingMap || currentState == AppState.PlacingObject)
        {
            UpdateReticle();
        }

        // Обработка нажатий на экран
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Игнорируем нажатия, если палец над UI элементом
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) return;

            if (touch.phase == TouchPhase.Began)
            {
                HandleTouch(touch.position);
            }
        }
    }

    // --- ЛОГИКА КУРСОРОВ ---
    void UpdateReticle()
    {
        var screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        if (raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            spawnedReticle.transform.position = hitPose.position;
            spawnedReticle.transform.rotation = hitPose.rotation;
            spawnedReticle.SetActive(true);

            // Меняем цвет кружка в зависимости от задачи
            var renderer = spawnedReticle.GetComponentInChildren<SpriteRenderer>(); // Или Image, если это Canvas
            if (renderer != null)
            {
                if (currentState == AppState.PlacingMap)
                    renderer.color = Color.white;
                else
                    renderer.color = Color.black;
            }
        }
        else
        {
            spawnedReticle.SetActive(false);
        }
    }

    // --- ОБРАБОТКА ТАПОВ ---
    void HandleTouch(Vector2 touchPos)
    {
        switch (currentState)
        {
            case AppState.PlacingMap:
                if (spawnedReticle.activeSelf) PlaceMap();
                break;

            case AppState.MapInteraction:
                // Рейкаст в 3D объекты (области карты)
                Ray ray = arCamera.ScreenPointToRay(touchPos);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    RegionData data = hit.collider.GetComponent<RegionData>();
                    if (data != null)
                    {
                        OpenCityMenu(data);
                    }
                }
                break;

            case AppState.PlacingObject:
                if (spawnedReticle.activeSelf) PlaceAttraction();
                break;
        }
    }

    // --- ФУНКЦИИ ДЕЙСТВИЙ ---

    public void StartPlacementMode() // Вызывается кнопкой "Старт"
    {
        currentState = AppState.PlacingMap;
        ShowPanel(scanHintPanel);
        hintText.text = "Найдите ровную поверхность для карты";
        planeManager.SetTrackablesActive(true); // Показываем белые точки сканирования
    }

    void PlaceMap()
    {
        if (spawnedMap != null) return; // Только одна карта

        spawnedMap = Instantiate(mapPrefab, spawnedReticle.transform.position, spawnedReticle.transform.rotation);

        // Добавляем Якорь, чтобы карта "запомнила" место в комнате
        spawnedMap.AddComponent<ARAnchor>();

        currentState = AppState.MapInteraction;
        spawnedReticle.SetActive(false);
        scanHintPanel.SetActive(false);
        planeManager.SetTrackablesActive(false); // Скрываем точки сканирования
    }

    void OpenCityMenu(RegionData data)
    {
        cityMenuPanel.SetActive(true);
        // Здесь можно передать название области в UI
        Debug.Log("Выбрана область: " + data.regionName);
    }

    // Вызывается из UI при выборе достопримечательности
    public void SelectAttractionPrefab(GameObject prefab)
    {
        attractionToPlace = prefab;
        cityMenuPanel.SetActive(false);
        attractionMenuPanel.SetActive(false);

        currentState = AppState.PlacingObject;
        planeManager.SetTrackablesActive(true); // Снова сканируем поверхность
        scanHintPanel.SetActive(true);
        hintText.text = "Наведите на пол (Черный кружок)";
    }

    void PlaceAttraction()
    {
        // Удаляем старую достопримечательность, если она была (по ТЗ - ОДНА)
        if (currentAttraction != null) Destroy(currentAttraction);

        currentAttraction = Instantiate(attractionToPlace, spawnedReticle.transform.position, Quaternion.identity);

        // Поворачиваем модель лицом к игроку
        Vector3 lookPos = arCamera.transform.position - currentAttraction.transform.position;
        lookPos.y = 0;
        currentAttraction.transform.rotation = Quaternion.LookRotation(lookPos);

        // Добавляем скрипт вращения пальцем
        currentAttraction.AddComponent<ObjectRotator>();

        currentState = AppState.ViewingObject;
        spawnedReticle.SetActive(false);
        scanHintPanel.SetActive(false);
        planeManager.SetTrackablesActive(false);
    }

    // --- UI HELPER ---
    void ShowPanel(GameObject panelToShow)
    {
        mainMenuPanel.SetActive(panelToShow == mainMenuPanel);
        cityMenuPanel.SetActive(panelToShow == cityMenuPanel);
        attractionMenuPanel.SetActive(panelToShow == attractionMenuPanel);
        scanHintPanel.SetActive(panelToShow == scanHintPanel);
    }
}