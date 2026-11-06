using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [Serializable]
    public class City
    {
        public string cityName;
        public Button cityButton;
        public string[] landmarkNames;
        public string[] landmarkPrefabNames;
        public string[] descriptions;
    }

    [Header("Города")]
    public City[] cities;

    [Header("UI Elements - Должны быть созданы в сцене")]
    public GameObject landmarksPanel;        // Панель выбора достопримечательностей
    public Transform landmarksContent;       // Content внутри ScrollView
    public Button backToMapButton;           // Кнопка "Назад на карту"
    public Button backToMenuButton;          // Кнопка "Назад в меню"
    public GameObject landmarkButtonPrefab;  // Префаб кнопки достопримечательности
    public TextMeshProUGUI cityTitleText;               // Текст с названием города

    private int selectedCityIndex = -1;

    void Start()
    {
        Debug.Log("MapManager запущен");

        // Проверяем настройки
        if (landmarksPanel == null) Debug.LogError("Landmarks Panel не назначен!");
        if (landmarksContent == null) Debug.LogError("Landmarks Content не назначен!");
        if (landmarkButtonPrefab == null) Debug.LogError("Landmark Button Prefab не назначен!");
        if (cityTitleText == null) Debug.LogError("City Title Text не назначен!");

        // Скрываем панель достопримечательностей
        if (landmarksPanel != null)
            landmarksPanel.SetActive(false);

        // Привязываем кнопки городов
        for (int i = 0; i < cities.Length; i++)
        {
            if (cities[i].cityButton == null)
            {
                Debug.LogError("Кнопка не назначена для города: " + cities[i].cityName);
                continue;
            }

            int index = i;
            cities[i].cityButton.onClick.AddListener(() => OnCitySelected(index));
        }

        // Кнопки навигации
        if (backToMapButton != null)
            backToMapButton.onClick.AddListener(OnBackToMap);

        if (backToMenuButton != null)
            backToMenuButton.onClick.AddListener(OnBackToMenu);
    }

    void OnCitySelected(int cityIndex)
    {
        selectedCityIndex = cityIndex;
        string cityName = cities[cityIndex].cityName;
        Debug.Log("Выбран город: " + cityName);

        // Обновляем заголовок
        if (cityTitleText != null)
            cityTitleText.text = cityName;

        // Показываем панель достопримечательностей
        if (landmarksPanel != null)
        {
            landmarksPanel.SetActive(true);
            UpdateLandmarksPanel(cityIndex);
        }
    }

    void UpdateLandmarksPanel(int cityIndex)
    {
        // Очищаем старые кнопки
        foreach (Transform child in landmarksContent)
        {
            Destroy(child.gameObject);
        }

        // Создаем новые кнопки
        for (int i = 0; i < cities[cityIndex].landmarkNames.Length; i++)
        {
            // Создаем кнопку из префаба
            GameObject buttonObj = Instantiate(landmarkButtonPrefab, landmarksContent);
            Button button = buttonObj.GetComponent<Button>();
            Text buttonText = buttonObj.GetComponentInChildren<Text>();

            if (buttonText != null)
                buttonText.text = cities[cityIndex].landmarkNames[i];

            // Сохраняем индексы
            int cityIdx = cityIndex;
            int landmarkIdx = i;

            // Назначаем обработчик
            button.onClick.AddListener(() => OnLandmarkSelected(cityIdx, landmarkIdx));
        }
    }

    void OnLandmarkSelected(int cityIndex, int landmarkIndex)
    {
        string cityName = cities[cityIndex].cityName;
        string landmarkName = cities[cityIndex].landmarkNames[landmarkIndex];
        string prefabName = cities[cityIndex].landmarkPrefabNames[landmarkIndex];

        Debug.Log($"Выбрано: {cityName} - {landmarkName}");

        // Сохраняем данные для AR сцены
        PlayerPrefs.SetString("SelectedCity", cityName);
        PlayerPrefs.SetString("SelectedLandmark", prefabName);
        PlayerPrefs.SetString("LandmarkName", landmarkName);
        PlayerPrefs.SetString("LandmarkDescription", cities[cityIndex].descriptions[landmarkIndex]);
        PlayerPrefs.Save();

        // Загружаем AR сцену
        SceneManager.LoadScene("ARViewScene");
    }

    public void OnBackToMap()
    {
        if (landmarksPanel != null)
            landmarksPanel.SetActive(false);
    }

    public void OnBackToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    // Кнопка Escape для теста
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (landmarksPanel != null && landmarksPanel.activeSelf)
                OnBackToMap();
            else
                OnBackToMenu();
        }
    }
}