using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CubeRotationController : MonoBehaviour
{
    public GameObject rotationWheelPrefab;
    public float holdTimeToRotate = 2f; // Время удержания для активации вращения

    private GameObject currentRotationWheel;
    private Transform selectedCube;
    private Camera arCamera;
    private bool isRotating = false;
    private float holdTimer = 0f;
    private bool isHolding = false;
    private Vector2 startTouchPosition;

    void Start()
    {
        arCamera = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount == 0)
        {
            isHolding = false;
            holdTimer = 0f;
            return;
        }

        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Cube"))
                {
                    selectedCube = hit.collider.transform;
                    isHolding = true;
                    startTouchPosition = touch.position;
                    Debug.Log("Начато удержание куба");
                }
                break;

            case TouchPhase.Stationary:
                if (isHolding && selectedCube != null)
                {
                    holdTimer += Time.deltaTime;

                    // Показываем кружок после 2 секунд
                    if (holdTimer >= holdTimeToRotate && !isRotating)
                    {
                        isRotating = true;
                        ShowRotationWheel(startTouchPosition);
                        Debug.Log("Активировано вращение куба");
                    }
                }
                break;

            case TouchPhase.Moved:
                if (isRotating && selectedCube != null)
                {
                    // Вращаем куб
                    Vector2 delta = touch.position - startTouchPosition;
                    selectedCube.Rotate(Vector3.up, -delta.x * 0.5f, Space.World);
                    selectedCube.Rotate(Vector3.right, delta.y * 0.5f, Space.World);
                    startTouchPosition = touch.position;

                    // Обновляем позицию кружка
                    if (currentRotationWheel != null)
                        currentRotationWheel.transform.position = touch.position;
                }
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                if (isRotating)
                {
                    Debug.Log("Завершено вращение куба");
                }
                else if (isHolding)
                {
                    Debug.Log("Короткое нажатие на куб");
                }

                isHolding = false;
                isRotating = false;
                holdTimer = 0f;
                HideRotationWheel();
                selectedCube = null;
                break;
        }
    }

    void ShowRotationWheel(Vector2 screenPosition)
    {
        if (currentRotationWheel == null && rotationWheelPrefab != null)
        {
            currentRotationWheel = Instantiate(rotationWheelPrefab,
                FindFirstObjectByType<Canvas>().transform);
        }

        if (currentRotationWheel != null)
        {
            currentRotationWheel.SetActive(true);
            currentRotationWheel.transform.position = screenPosition;
            Debug.Log("Кружок вращения показан");
        }
    }

    void HideRotationWheel()
    {
        if (currentRotationWheel != null)
        {
            currentRotationWheel.SetActive(false);
            Debug.Log("Кружок вращения скрыт");
        }
    }
}