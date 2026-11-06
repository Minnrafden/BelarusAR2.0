using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Кнопки главного меню")]
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;

    [Header("Панели")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    [Header("Настройки")]
    public Slider volumeSlider;
    public Button settingsBackButton;

    void Start()
    {
        // Проверяем что кнопки назначены
        if (playButton == null) Debug.LogError("PlayButton не назначен!");
        if (settingsButton == null) Debug.LogError("SettingsButton не назначен!");
        if (quitButton == null) Debug.LogError("QuitButton не назначен!");

        // Назначаем обработчики
        playButton.onClick.AddListener(OnPlayButtonClick);
        settingsButton.onClick.AddListener(OnSettingsButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);

        // Настройки
        if (settingsBackButton != null)
            settingsBackButton.onClick.AddListener(OnSettingsBackClick);

        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.7f);
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        // Скрываем панель настроек
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        // Показываем главное меню
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
    }

    void OnPlayButtonClick()
    {
        Debug.Log("Загружаем карту...");
        SceneManager.LoadScene("MapScene");
    }

    public void OnSettingsButtonClick()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    public void OnSettingsBackClick()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }

    public void OnVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
        AudioListener.volume = value;
    }

    public void OnQuitButtonClick()
    {
        Debug.Log("Выход из приложения");
        Application.Quit();

        // Для редактора
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}