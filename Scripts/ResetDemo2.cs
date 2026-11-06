using UnityEngine;
using UnityEngine.SceneManagement;

namespace SlimUI.ModernMenu
{
    public class ResetDemo2 : MonoBehaviour
    {
        void Update()
        {
            bool isKeyPressed = Input.GetKeyDown(KeyCode.R);
            bool isScreenTapped = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;

            if (isKeyPressed || isScreenTapped)
            {
                SceneManager.LoadScene("ARObjectScene");
            }
        }
    }
}