using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AttractionManager : MonoBehaviour
{
    
    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public Image dostImage;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI netyText;
    public Button buttonAR;

    [Header("Настройка достопримечательностей")]
    public Sprite logkaGerb;
    public Sprite teatroperaGerb;
    public Sprite soborKafGerb;
    public Sprite ratyshaGerb;
    public Sprite bobrKrepGerb;
    public Sprite osobGerb;
    public Sprite staryGorodGerb;
    public Sprite iezyitskiiGerb;
    public Sprite sofCobGerb;
    public Sprite brestKrepGerb;
    public Sprite kamenecGerb;
    public Sprite pokrSobGerb;
    public Sprite rympaskGerb;
    public Sprite gorogGerb;
    public Sprite loevGerb;
    public Sprite borisGerb;
    public Sprite lidGerb;
    public Sprite novogrydGerb;

    void Start()
    {
        string dost = DataBridge.SelectedAttraction;
        if (string.IsNullOrEmpty(dost)) dost = "Неизвестный город";

        titleText.text = dost;

        switch (dost)
        {
            case "Музей ложки":
                SetCityData("Музей деревянной ложки, который там находится, начинался с обычной домашней коллекции местной жительницы Анны Жданович и располагался в ее доме. А за 9 лет вырос в настоящую местную достопримечательность.", logkaGerb);
                break;

            case "Театр оперы и балета":
                SetCityData("Первые упоминания про театральную жизнь Минска относятся до середины XVII века во время существования Минского школьного театра при Минском иезуитском коллегиуме. С конца 1770-х годов в Минске выступали польские гастрольные театральные коллективы. ", teatroperaGerb);
                break;

            case "Кафедральный собор":
                SetCityData("До постройки современного собора на его месте находилась деревянная Воскресенская церковь, возведённая в 1620—1648 годах и сгоревшая в 1865 или 1867 году.", soborKafGerb);
                buttonAR.gameObject.SetActive(false);
                break;

            case "Ратуша":
                SetCityData("В 1578 году, через год после получения городом грамоты на магдебургское право, в Могилёве началось возведение городской ратуши. Первоначально ратуша была деревянной, поэтому неоднократно сгорала дотла, и её местонахождение менялось.", ratyshaGerb);
                buttonAR.gameObject.SetActive(false);
                break;

            case "Бобруйская крепость":
                SetCityData("Строительство начато в 1810 году, крепость сыграла существенную роль в Отечественной войне 1812 года. В 2002 году памятник истории и архитектуры «Бобруйская крепость» был внесён в Государственный список историко-культурных ценностей Республики Беларусь.", bobrKrepGerb);
                buttonAR.gameObject.SetActive(false);
                break;

            case "Особняк князей Мещерских":
                SetCityData("Особняк построен по инициативе князя Николая Мещерского, предводителя дворянства Мстиславского уезда, для своей дочери Марии в 1867 году. До 1917 года являлся центром светской жизни Климовичей.", osobGerb);
                buttonAR.gameObject.SetActive(false);
                break;

            case "Старый город":
                SetCityData("Исторический центр на рубеже экономики Двины, где переплетаются эпохи, с ключевыми достопримечательностями.", staryGorodGerb);
                buttonAR.gameObject.SetActive(false);
                break;

            case "Иезуитский коллегиум":
                SetCityData("Учреждение ордена иезуитов, существовавшее в Орше в 1612—1820 годах. Сохранившиеся здания коллегиума являются одной из самых узнаваемых достопримечательностей города.", iezyitskiiGerb);
                buttonAR.gameObject.SetActive(false);
                break;

            case "Софийский собор":
                SetCityData("Возведённый в середине XI века в византийском стиле храм был освящён во имя Святой Софии. До нашего времени Софийский собор сохранился в значительно изменённом виде, окончательно был перестроен в XVIII веке в стиле виленского барокко.", sofCobGerb);
                buttonAR.gameObject.SetActive(false);
                break;

            case "Брестская крепость":
                SetCityData("8 мая 1965 года Брестской крепости за её героическую оборону в июне 1941 года присвоено звание «Крепость-герой» — высшая степень отличия, которой удостоены единственная крепость и двенадцать городов Советского Союза", brestKrepGerb);
                buttonAR.gameObject.SetActive(false);
                break;

            case "Каменецкая башня":
                SetCityData("Наиболее хорошо сохранившаяся оборонительная башня волынского типа и одновременно самая высокая (29,4 м). Построена в 1271—1288 годах.", kamenecGerb);
                buttonAR.gameObject.SetActive(false);
                break;

            case "Покровский собор":
                SetCityData("старейший из существующих православных храмов в Барановичах, крупнейший в Беларуси памятник архитектуры неоклассицизма.", pokrSobGerb);
                buttonAR.gameObject.SetActive(false);
                break;

            case "Дворец Румянцевых и Паскевичей":
                SetCityData("Памятник архитектуры XVIII—XIX веков, главная достопримечательность города Гомеля, композиционный центр Гомельского дворцово-паркового ансамбля.", rympaskGerb);
                buttonAR.gameObject.SetActive(false);
                break;

            case "Городище":
                SetCityData("Археологический памятник, для которого характерно наличие ограждения. Городища представляют собой остатки укреплённых усадеб, крепостей, убежищ, святилищ и т. д. Помимо этого, городищами именуют остатки древних.", gorogGerb);
                buttonAR.gameObject.SetActive(false);
                break;

            case "Символические ворота Лоева":
                SetCityData("Привлекающая внимание архитектурная композиция, установленная при въезде в город.", loevGerb);
                buttonAR.gameObject.SetActive(false);
                break;

            case "Борисоглебская(Коложская) церковь":
                SetCityData("Одна из древнейших сохранившихся церквей Древней Руси, уникальный памятник чёрнорусского зодчества.", borisGerb);
                buttonAR.gameObject.SetActive(false);
                break;

            case "Лидский замок":
                SetCityData("Замок в Беларуси, в городе Лида, построенный в 1323 году по поручению князя Гедимина. Входил в линию защиты против крестоносцев Новогрудок — Крево — Медники — Троки.", lidGerb);
                buttonAR.gameObject.SetActive(false);
                break;

            case "Руины Новогрудского замка":
                SetCityData("Замок, некогда находившийся в городе Новогрудок, был одним из ключевых опорных пунктов и одной из мощнейших крепостей в Великом княжестве Литовском.", novogrydGerb);
                buttonAR.gameObject.SetActive(false);
                break;

            default:
                netyText.text = "Информация о достопримечательности " + dost + " пока не добавлена.";
                titleText.gameObject.SetActive(false);
                descriptionText.gameObject.SetActive(false);
                dostImage.gameObject.SetActive(false);
                buttonAR.gameObject.SetActive(false);
                break;
        }
    }


    void SetCityData(string desc, Sprite gerb)
    {
        descriptionText.text = desc;
        if (gerb != null) dostImage.sprite = gerb;
    }

    public void StartAR()
    {
        SceneManager.LoadScene("ARObjectScene");
    }

    public void BackToCity()
    {
        SceneManager.LoadScene("CityInfoScene");
    }
}