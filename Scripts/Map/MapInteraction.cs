using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems; // Чтобы клики не проходили сквозь кнопки UI

public class MapInteraction : MonoBehaviour
{
    public Camera arCamera;
    public TextMeshProUGUI infoText;
    public string targetLayer = "Map";

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 inputPos = Input.touchCount > 0 ? (Vector3)Input.GetTouch(0).position : Input.mousePosition;
            Ray ray = arCamera.ScreenPointToRay(inputPos);

            // Используем Raycast для 2D физики
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, 1 << LayerMask.NameToLayer(targetLayer));

            if (hit.collider != null)
            {
                string regionName = hit.collider.gameObject.name;
                Debug.Log("Попали в 2D объект: " + regionName);
                DataBridge.SelectedRegion = regionName;
                SceneManager.LoadScene("MinskRegionInfoScene");
            }
            else
            {
                Debug.Log("Промах! Убедись, что на объекте стоит Polygon Collider 2D или Box Collider 2D");
            }
        }
    }
}