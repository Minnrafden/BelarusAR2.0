using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public class ARObjectSaver : MonoBehaviour
{
    private string savePath;
    private List<GameObject> placedObjects = new List<GameObject>();

    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "ar_objects.json");
        LoadObjects();
    }

    public void AddObject(GameObject obj)
    {
        placedObjects.Add(obj);
        SaveObjects();
    }

    void SaveObjects()
    {
        List<ObjectData> data = new List<ObjectData>();

        foreach (GameObject obj in placedObjects)
        {
            if (obj != null)
            {
                data.Add(new ObjectData()
                {
                    position = obj.transform.position,
                    rotation = obj.transform.rotation,
                    scale = obj.transform.localScale
                });
            }
        }

        string json = JsonUtility.ToJson(new ObjectList() { objects = data }, true);
        File.WriteAllText(savePath, json);
        Debug.Log($"Объекты сохранены: {savePath}");
    }

    void LoadObjects()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            ObjectList data = JsonUtility.FromJson<ObjectList>(json);

            foreach (ObjectData objData in data.objects)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = objData.position;
                cube.transform.rotation = objData.rotation;
                cube.transform.localScale = objData.scale;
                cube.tag = "Cube";
                placedObjects.Add(cube);
            }
            Debug.Log($"Загружено {data.objects.Count} объектов");
        }
    }

    [Serializable]
    private class ObjectData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }

    [Serializable]
    private class ObjectList
    {
        public List<ObjectData> objects;
    }
}