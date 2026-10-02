using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader // new stuff
{

    public enum Scene
    {
        MainMenuScene,
        MainScene,
        LoadingScene
    }

    private static Scene targetScene; // Is public because only the callback then uses it actually

    public static void Load(Scene _targetScene)
    {
        Loader.targetScene = _targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString()); // We load the LoadingScene and the script gets activated which the calls the LoaderCallback
    }

    // This is so that we know FOR SURE that the Loading scene was rendered!
    public static void LoaderCallback() // Gets called every first Update the loading scene...
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
