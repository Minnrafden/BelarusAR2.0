using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CameraPoint[] cameras;
    [SerializeField] private int activePriority;
    [SerializeField] private int inactivePriority;

    private int current;

    private void Awake()
    {
        cameras[0].cam.Priority = activePriority;
    }

    public void Setcamera(int id)
    {
        if (id < 0 || id >= cameras.Length || id == current)
        {
            return;
        }

        cameras[current].cam.Priority = inactivePriority;
        cameras[current].onClose.Invoke();

        cameras[id].cam.Priority = activePriority;
        current = id;

        cameras[id].onOpen.Invoke();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (cameras[current].previousCam != null)
            {
                for (int i = 0; i < cameras.Length; i++)
                {
                    if (cameras[i].cam == cameras[current].previousCam)
                    {
                        Setcamera(i);
                        return;
                    }
                }
            }
        }
    }


    [System.Serializable]
    public struct CameraPoint
    {
        public CinemachineCamera cam;
        public CinemachineCamera previousCam;
        public UnityEvent onOpen;
        public UnityEvent onClose;

    }
}
