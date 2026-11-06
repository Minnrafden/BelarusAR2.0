using UnityEngine;
using UnityEngine.SceneManagement;

public class BackDemo1 : MonoBehaviour
{
    public void BackDemo()
    {
        SceneManager.LoadScene("Demo1");
    }

    public void BackDemo2()
    {
        SceneManager.LoadScene("AttractionInfoScene");
    }
}
