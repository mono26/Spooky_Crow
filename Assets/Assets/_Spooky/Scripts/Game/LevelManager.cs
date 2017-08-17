using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public SceneFader fader;

    public Button[] levelButtons;

    public string menuSceneName = "MainMenu";
    public string nextLevel = "Level02";
    public int levelToUnlock = 2;

    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
                levelButtons[i].interactable = false;
        }
    }

    public void Select(string levelName)
    {
        fader.FadeTo(levelName);
    }
}
