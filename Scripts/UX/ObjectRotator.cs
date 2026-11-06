using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    private float rotationSpeed = 0.2f;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                // Вращаем только по оси Y
                transform.Rotate(0, -touch.deltaPosition.x * rotationSpeed, 0);
            }
        }
    }
}