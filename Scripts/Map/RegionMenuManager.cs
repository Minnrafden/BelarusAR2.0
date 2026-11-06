using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegionMenuManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI titleText;
    public Image gerbImage;
    public Transform scrollContent;
    public Button attractionButtonPrefab;
    public TextMeshProUGUI nety;

    [Header("Настройки областей")]
    public Sprite minskGerb;
    public Sprite mogGerb;
    public Sprite vitGerb;
    public Sprite brestGerb;
    public Sprite grodGerb;
    public Sprite gomelGerb;

    void Start()
    {
        //DataBridge.Start();
        titleText.text = $"{DataBridge.SelectedRegion} область";
        string city = DataBridge.SelectedRegion;

        foreach (Transform child in scrollContent)
        {
            Destroy(child.gameObject);
        }

        switch (city)
        {
            case "Минская":
                SetCityData(minskGerb);
                AddAttractionButton("Минск");
                AddAttractionButton("Березино");
                AddAttractionButton("Борисов");
                break;

            case "Могилевская":
                SetCityData(mogGerb);
                AddAttractionButton("Могилев");
                AddAttractionButton("Бобруйск");
                AddAttractionButton("Климовичи");
                break;

            case "Витебская":
                SetCityData(vitGerb);
                AddAttractionButton("Витебск");
                AddAttractionButton("Орша");
                AddAttractionButton("Полоцк");
                break;

            case "Брестская":
                SetCityData(brestGerb);
                AddAttractionButton("Брест");
                AddAttractionButton("Каменец");
                AddAttractionButton("Барановичи");
                break;

            case "Гомельская":
                SetCityData(gomelGerb);
                AddAttractionButton("Гомель");
                AddAttractionButton("Ветка");
                AddAttractionButton("Лоев");
                break;

            case "Гродненская":
                SetCityData(grodGerb);
                AddAttractionButton("Гродно");
                AddAttractionButton("Лида");
                AddAttractionButton("Новогрудок");
                break;

            default:
                nety.text = "Информация о " + city + " область пока не добавлена.";
                titleText.gameObject.SetActive(false);
                gerbImage.gameObject.SetActive(false);
                foreach (Transform child in scrollContent)
                {
                    Destroy(child.gameObject);
                }
                break;
        }

    }


    void SetCityData(Sprite gerb)
    {
        if (gerb != null) gerbImage.sprite = gerb;
    }


    void AddAttractionButton(string name)
    {
        Button btn = Instantiate(attractionButtonPrefab, scrollContent);
        btn.GetComponentInChildren<TextMeshProUGUI>().text = name;

        btn.onClick.AddListener(() => OnCityClick(name));
    }


    public void OnCityClick(string cityName)
    {
        DataBridge.SelectedCity = cityName;
        SceneManager.LoadScene("CityInfoScene");
    }

    public void OnBackButton(string button)
    {
        SceneManager.LoadScene("02_ARScanning");
    }
}