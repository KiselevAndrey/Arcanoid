using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }

    public void LoadScene(Object scene)
    {
        SceneManager.LoadScene(scene.name);
    }

    public void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    public void ResetScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }
}
