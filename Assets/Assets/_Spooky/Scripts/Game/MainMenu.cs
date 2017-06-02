using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public string levelToLoad = "LevelSelect";

    public SceneFader sceneFader;

    public void Play()
    {
        Debug.Log("Me estan undiendo");
        sceneFader.FadeTo(levelToLoad);
    }

    public void Quit()
    {
        Debug.Log("Exciting...");
        Application.Quit();
    }

}
