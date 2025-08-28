using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionMenu : BaseMenu
{
    public Button mainMenuBtn;
    public Button settingsBtn;
    public Button creditsBtn;
    public Button backBtn;

    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.InstructionsMenu;

        if (mainMenuBtn) mainMenuBtn.onClick.AddListener(() => SceneManager.LoadScene("TitleScreen"));
        if (settingsBtn) settingsBtn.onClick.AddListener(() => SetNextMenu(MenuStates.SettingsMenu));
        if (creditsBtn) creditsBtn.onClick.AddListener(() => SetNextMenu(MenuStates.CreditMenu));
        if (backBtn) backBtn.onClick.AddListener(JumpBack);
    }
}
