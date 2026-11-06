using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attraction
{
    public string id;
    public string name;
    public string description;
    public string type;
    public string century;
    public float rating;
    public bool hasARModel;
    public bool isUNESCO;
    public string address;
    public string photoName;
    public double latitude;
    public double longitude;
}

[System.Serializable]
public class City
{
    public string id;
    public string name;
    public int founded;
    public long population;
    public string description;
    public string photoName;
    public List<Attraction> attractions;
}

[System.Serializable]
public class Region
{
    public string id;
    public string name;
    public string fullName;
    public string iconName;
    public bool isCapital;
    public bool isAvailable;
    public string capitalCity;
    public string description;
    public float areaKm2;
    public List<City> cities;

    public int TotalAttractions()
    {
        int count = 0;
        if (cities != null)
            foreach (var c in cities)
                if (c.attractions != null)
                    count += c.attractions.Count;
        return count;
    }
}

[System.Serializable]
public class BelarusData
{
    public List<Region> regions;
}