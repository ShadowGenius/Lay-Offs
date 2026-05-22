using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitcher : MonoBehaviour
{

    public static void Change(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}