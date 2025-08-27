using UnityEngine;
using UnityEngine.UI;

public class CreditsMenu : BaseMenu
{
    public Button instructionBtn;
    public Button settingsBtn;
    public Button backBtn;

    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.CreditMenu;

        if (instructionBtn) instructionBtn.onClick.AddListener(() => SetNextMenu(MenuStates.InstructionsMenu));
        if (settingsBtn) settingsBtn.onClick.AddListener(() => SetNextMenu(MenuStates.SettingsMenu));
        if (backBtn) backBtn.onClick.AddListener(JumpBack);
    }
}
