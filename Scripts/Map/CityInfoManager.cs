using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CityInfoManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI cityNameText;
    public Image gerbImage;
    public TextMeshProUGUI descriptionText;
    public Transform scrollContent;
    public Button attractionButtonPrefab;


    [Header("Настройки городов Минской области")]
    public Sprite minskGerb;
    public Sprite berezinoGerb;
    public Sprite borisovGerb;

    [Header("Настройка городов Могилевской области")]
    public Sprite mogilevGerb;
    public Sprite bobryiskGerb;
    public Sprite klimovichiGerb;

    [Header("Настройка городов Витебская области")]
    public Sprite vitebskGerb;
    public Sprite orshaGerb;
    public Sprite polothkGerb;

    [Header("Настройка городов Брестская области")]
    public Sprite brestGerb;
    public Sprite kamenethGerb;
    public Sprite baranovichiGerb;

    [Header("Настройка городов Гомельская области")]
    public Sprite gomelGerb;
    public Sprite vetkaGerb;
    public Sprite loevGerb;

    [Header("Настройка городов Гродненская области")]
    public Sprite grodnoGerb;
    public Sprite lidaGerb;
    public Sprite novogrydokGerb;

    void Start()
    {
        string city = DataBridge.SelectedCity;
        if (string.IsNullOrEmpty(city)) city = "Неизвестный город";

        cityNameText.text = city;

        foreach (Transform child in scrollContent)
        {
            Destroy(child.gameObject);
        }

        switch (city)
        {
            //Минская область
            case "Березино":
                SetCityData("Город в Минской области Беларуси в 100 км к востоку от Минска. Административный центр Березинского района. Стоит на реке Березине. Население — 11 250 человек.", berezinoGerb);
                AddAttractionButton("Музей ложки");
                break;

            case "Минск":
                SetCityData("Столица и крупнейший город Республики Беларусь." +
                    "Минская агломерация занимает площадь 1600 км² с населением 2,7 млн человек (2018).", minskGerb);
                AddAttractionButton("Театр оперы и балета");
                break;

            case "Борисов":
                SetCityData("Город в Минской области Республики Беларусь. Территория города — 46 км². Население города — 134 732 человек (2025)[5]. Стоит на реке Березина, в 77 км[6] от Минска. В 2002 году Борисов отметил 900-летие.", borisovGerb);
                AddAttractionButton("Кафедральный собор");
                break;


            //Могилевская область
            case "Могилев":
                SetCityData("В основе названия может лежать личное имя Могила, о чём свидетельствует наличие притяжательного суффикса «-ёв». Говорится, что это название происходит от имени князя Льва Даниловича Могия (могучий лев)", mogilevGerb);
                AddAttractionButton("Ратуша");
                break;

            case "Бобруйск":
                SetCityData("Название Бобруйска происходит от названия речки Бобруйки, впадающей в Березину на месте современного Бобруйска. Согласно В. Топорову и О. Трубачёву, название Бобруйки — балтское и связано с литовским bebrus «бобр».", bobryiskGerb);
                AddAttractionButton("Бобруйская крепость");
                break;

            case "Климовичи":
                SetCityData("Начиная с 1996 года в г. Климовичи ежегодно в июне проходит фестиваль детского творчества «Золотая пчелка». С 2001 года он носит статус международного.", klimovichiGerb);
                AddAttractionButton("Особняк князей Мещерских");
                break;


            //Витебская область
            case "Витебск":
                SetCityData("Второй по возрасту город Беларуси (974 год) после Полоцка. Возник на высоких берегах Западной Двины и Витьбы, давшей ему имя, на пути из варяг в греки.", vitebskGerb);
                AddAttractionButton("Старый город");
                break;

            case "Орша":
                SetCityData("Расположен на Днепре при впадении в него реки Оршицы в 80 км к югу от Витебска и в 202 км на восток от Минска. Крупный железнодорожный узел[тарашк.], автомобильный узел Беларуси и транспортной артерии Европа — Россия — Украина.", orshaGerb);
                AddAttractionButton("Иезуитский коллегиум");
                break;

            case "Полоцк":
                SetCityData("Численность населения — 79 285 человек (на 1 января 2025 года). Древнейший город Беларуси, впервые упоминается в «Повести временных лет» под 862 годом.", polothkGerb);
                AddAttractionButton("Софийский собор");
                break;


            //Брестская область
            case "Брест":
                SetCityData("Город был центром Берестейской земли и одним из важнейших городов Киевской Руси и Галицко-Волынского княжества. В 2019 году Брест отметил 1000-летие", brestGerb);
                AddAttractionButton("Брестская крепость");
                break;

            case "Каменец":
                SetCityData("Город на западе Беларуси в Брестской области на реке Лесная, расположен в 40 км к северу от Бреста.", kamenethGerb);
                AddAttractionButton("Каменецкая башня");
                break;

            case "Барановичи":
                SetCityData("Город областного подчинения в Беларуси, административный центр Барановичского района Брестской области, крупный железнодорожный узел в направлении Минска, Бреста, Волковыска, Лиды, Лунинца и Слуцка.", baranovichiGerb);
                AddAttractionButton("Покровский собор");
                break;


            //Гомельская область
            case "Гомель":
                SetCityData("Более двух столетий визитной карточкой города на реке Сож является великолепный дворец Румянцевых и Паскевичей", gomelGerb);
                AddAttractionButton("Дворец Румянцевых и Паскевичей");
                break;

            case "Ветка":
                SetCityData("Город (с 1925 года) в Гомельской области Республики Беларусь на реке Сож, стоит на востоке Белорусского Полесья, в 22 км от железнодорожной станции Гомель", vetkaGerb);
                AddAttractionButton("Городище");
                break;

            case "Лоев":
                SetCityData("Городской посёлок в Гомельской области Республики Беларусь. Расположен при впадении реки Сож в Днепр. Административный центр Лоевского района. Численность населения — 6 028 человек (на 1 января 2025 года).", loevGerb);
                AddAttractionButton("Символические ворота Лоева");
                break;


            //Гродненская область
            case "Гродно":
                SetCityData("Гродно — один из самых старых городов Беларуси. Он впервые упоминается в древнерусских источниках в 1005 году. В течение 2014 года город носил звание «Культурная столица Беларуси»", grodnoGerb);
                AddAttractionButton("Борисоглебская(Коложская) церковь");
                break;

            case "Лида":
                SetCityData("Расположен в центральной части Гродненской области на пересечении автомобильных и железнодорожных путей (Минск-Гродно и Вильнюс-Слоним).", lidaGerb);
                AddAttractionButton("Лидский замок");
                break;

            case "Новогрудок":
                SetCityData("Находится на Новогрудской возвышенности в 162 км от Гродно и 22 км от железнодорожной станции в Новоельне на линии Барановичи — Лида.", novogrydokGerb);
                AddAttractionButton("Руины Новогрудского замка");
                break;


            default:
                descriptionText.text = "Информация о городе " + city + " пока не добавлена.";
                cityNameText.gameObject.SetActive(false);
                gerbImage.gameObject.SetActive(false);
                foreach (Transform child in scrollContent)
                {
                    Destroy(child.gameObject);
                }
                break;
        }
    }


    void SetCityData(string desc, Sprite gerb)
    {
        descriptionText.text = desc;
        if (gerb != null) gerbImage.sprite = gerb;
    }


    void AddAttractionButton(string name)
    {
        Button btn = Instantiate(attractionButtonPrefab, scrollContent);
        btn.GetComponentInChildren<TextMeshProUGUI>().text = name;

        btn.onClick.AddListener(() => OnAttractionClick(name));
    }

    public void OnAttractionClick(string attrName)
    {
        DataBridge.SelectedAttraction = attrName; //Дост
        SceneManager.LoadScene("AttractionInfoScene");
    }
    public void OnBackButton(string button)
    {
        SceneManager.LoadScene("MinskRegionInfoScene");
    }
}