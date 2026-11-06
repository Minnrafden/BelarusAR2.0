using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class WallPlacement : MonoBehaviour
{
    public GameObject mapPrefab;
    private GameObject spawnedMap;
    private ARRaycastManager raycastManager;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            // Проверяем, попал ли луч в какую-либо плоскость
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                ARPlaneManager planeManager = GetComponent<ARPlaneManager>();
                ARPlane plane = planeManager.GetPlane(hits[0].trackableId);

                Debug.Log($"Попали в плоскость: {plane.alignment}");

                // Проверяем, что стена вертикальная
                if (plane.alignment == PlaneAlignment.Vertical)
                {
                    if (spawnedMap == null)
                    {
                        spawnedMap = Instantiate(mapPrefab, hitPose.position, hitPose.rotation);
                        // Принудительно ставим масштаб, если префаб слишком большой
                        spawnedMap.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        Debug.Log("Карта создана!");
                    }
                    else
                    {
                        spawnedMap.transform.position = hitPose.position;
                        spawnedMap.transform.rotation = hitPose.rotation;
                        Debug.Log("Карта перемещена!");
                    }
                }
                else
                {
                    Debug.Log("Это не вертикальная стена (пол или потолок)");
                }
            }
            else
            {
                Debug.Log("Луч не нашел ни одной плоскости. Посканируй стену подольше.");
            }
        }
    }
}