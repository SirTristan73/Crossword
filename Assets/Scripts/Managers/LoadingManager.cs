using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : PersistentSingleton<LoadingManager>
{
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
