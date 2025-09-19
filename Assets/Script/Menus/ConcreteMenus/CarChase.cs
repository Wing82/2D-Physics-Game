using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarChase : BaseMenu
{
    public TMP_Text timerText;
    public TMP_Text livesText;

    private bool countdownDone = false; // Flag to check if countdown is done

    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.CarChase; // Set the state to CarChase

        GameManager.Instance.OnTimerPrepare += ShowGetReady;

        GameManager.Instance.OnTimerValueChanged += TimerValueChanged;

        ShowGetReady();

        livesText.text = $"Lives: {GameManager.Instance.lives}"; // Assuming a default of 3 lives
        GameManager.Instance.OnLifeValueChanged += LifeValueChanged;
    }

    private void ShowGetReady() => timerText.text = "Get Ready!";

    private void TimerValueChanged(int value)
    {
        if (!countdownDone)
        {
            // During initial countdown (3,2,1), show numbers
            if (value > 0 && value <= 3)
            {
                timerText.text = value.ToString();
                return;
            }

            // Countdown finished, switch to main timer
            countdownDone = true;
        }

        if (value < 10) timerText.text = $"Timer: 0{value}"; // Show timer in single digits
        else timerText.text = $"Timer: {value}"; // Show timer in double digits

        if (value == 0)
        {
            // Timer has reached zero, handle game over logic
            timerText.text = "Time's Up!";
            SceneManager.LoadScene("GameOverScreen"); // Load Game Over scene
            return;
        }
    }

    private void LifeValueChanged(int value) => livesText.text = $"Lives: {value}";

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnTimerPrepare -= ShowGetReady;
            GameManager.Instance.OnTimerValueChanged -= TimerValueChanged;
            GameManager.Instance.OnLifeValueChanged -= LifeValueChanged;
        }
    }
}
