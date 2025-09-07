using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueMenu : BaseMenu
{
    public Button continueBtn;
    public Button mainMenuBtn;
    public Button quitBtn;

    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.ContinueMenu;

        if (continueBtn) continueBtn.onClick.AddListener(() => SetNextMenu(MenuStates.BirdCannon));
        if (mainMenuBtn) mainMenuBtn.onClick.AddListener(() => SceneManager.LoadScene("StartScreen"));
        if (quitBtn) quitBtn.onClick.AddListener(QuitGame);
    }
}
