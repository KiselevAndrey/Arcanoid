using UnityEngine;
using UnityEngine.SceneManagement;

public static class ManagerSceneStatic
{    public static void Exit()
    {
        Application.Quit();
    }

    public static void LoadScene(Object scene)
    {
        SceneManager.LoadScene(scene.name);
    }

    public static void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
