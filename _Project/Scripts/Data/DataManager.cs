using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    // Передаём между сценами
    public static Region SelectedRegion;
    public static City SelectedCity;
    public static Attraction SelectedAttraction;

    private BelarusData data;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else Destroy(gameObject);
    }

    void LoadData()
    {
        var json = Resources.Load<TextAsset>("Data/belarus");
        if (json != null)
        {
            data = JsonUtility.FromJson<BelarusData>(json.text);
            Debug.Log($"Загружено регионов: {data.regions.Count}");
        }
        else
            Debug.LogError("belarus.json не найден в Resources/Data/!");
    }

    public List<Region> GetRegions() =>
        data?.regions ?? new List<Region>();

    public List<Attraction> GetRecommended()
    {
        var list = new List<Attraction>();
        foreach (var r in GetRegions())
            foreach (var c in r.cities)
                foreach (var a in c.attractions)
                    if (a.isUNESCO) list.Add(a);
        return list;
    }

    public Sprite LoadPhoto(string photoName)
    {
        if (string.IsNullOrEmpty(photoName)) return null;
        return Resources.Load<Sprite>($"Photos/{photoName}");
    }

    public Sprite LoadIcon(string iconName)
    {
        if (string.IsNullOrEmpty(iconName)) return null;
        return Resources.Load<Sprite>($"Icons/{iconName}");
    }

    public int TotalRegions() => GetRegions().Count;

    public int TotalCities()
    {
        int n = 0;
        foreach (var r in GetRegions())
            n += r.cities?.Count ?? 0;
        return n;
    }

    public int TotalAttractions()
    {
        int n = 0;
        foreach (var r in GetRegions()) n += r.TotalAttractions();
        return n;
    }
}