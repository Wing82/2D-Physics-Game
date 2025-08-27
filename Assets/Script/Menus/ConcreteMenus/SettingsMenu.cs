using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : BaseMenu
{
    public Button instructionBtn;
    public Button creditsBtn;
    public Button backBtn;

    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.SettingsMenu;

        if (instructionBtn) instructionBtn.onClick.AddListener(() => SetNextMenu(MenuStates.InstructionsMenu));
        if (creditsBtn) creditsBtn.onClick.AddListener(() => SetNextMenu(MenuStates.CreditMenu));
        if (backBtn) backBtn.onClick.AddListener(JumpBack);
    }


}
