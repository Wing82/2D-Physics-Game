using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : BaseMenu
{
    public Button mainMenuBtn;
    public Button quitBtn;

    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.GameOverMenu;

        if (mainMenuBtn) mainMenuBtn.onClick.AddListener(() => SceneManager.LoadScene("StartScreen"));
        if (quitBtn) quitBtn.onClick.AddListener(QuitGame);
    }
}
