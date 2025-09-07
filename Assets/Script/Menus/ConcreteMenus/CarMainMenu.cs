using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarMainMenu : BaseMenu
{
    public Button startButton;
    public Button instructionsButton;
    public Button settingsButton;
    public Button creditsButton;
    public Button quitButton;

    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.CarLevelMenu;

        if (startButton) startButton.onClick.AddListener(() => SceneManager.LoadScene("CarChase"));
        if (instructionsButton) instructionsButton.onClick.AddListener(() => SetNextMenu(MenuStates.InstructionsMenu));
        if (settingsButton) settingsButton.onClick.AddListener(() => SetNextMenu(MenuStates.SettingsMenu));
        if (creditsButton) creditsButton.onClick.AddListener(() => SetNextMenu(MenuStates.CreditMenu));
        if (quitButton) quitButton.onClick.AddListener(QuitGame);
    }
}
