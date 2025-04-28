using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameLoader : MonoBehaviour
{
    public void LoadMiniGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
