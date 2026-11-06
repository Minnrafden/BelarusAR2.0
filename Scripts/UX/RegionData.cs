using System.Collections.Generic;
using UnityEngine;

public class RegionData : MonoBehaviour
{
    [Header("Название области")]
    public string regionName;

    [Header("Список городов")]
    public List<string> cities = new List<string>();

    // Можно также сразу добавить ссылки на префабы достопримечательностей
    [Header("Достопримечательности (префабы)")]
    public List<GameObject> attractionPrefabs = new List<GameObject>();
}