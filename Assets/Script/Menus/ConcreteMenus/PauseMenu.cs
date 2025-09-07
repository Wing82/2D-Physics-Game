using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : BaseMenu
{
    public Button resumeBtn;
    public Button mainMenuBtn;
    public Button settingsBtn;
    public Button quitBtn;

    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.PauseMenu;

        //Time.timeScale = 0f; // Pause the game

        if (resumeBtn) resumeBtn.onClick.AddListener(() => SetNextMenu(MenuStates.BirdCannon));
        if (mainMenuBtn) mainMenuBtn.onClick.AddListener(() => SceneManager.LoadScene("StartScreen"));
        if (settingsBtn) settingsBtn.onClick.AddListener(() => SetNextMenu(MenuStates.SettingsMenu));
        if (quitBtn) quitBtn.onClick.AddListener(QuitGame);
    }
}
