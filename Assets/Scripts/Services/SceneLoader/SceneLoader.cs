using UnityEngine.SceneManagement;

namespace Services.SceneLoader
{
    public sealed class SceneLoader
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}