using TMPro;
using UnityEngine;

public class BirdCannon : BaseMenu
{
    public TMP_Text powerText;
    public TMP_Text angleText;
    public TMP_Text scoreText;

    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.BirdCannon; // Set the state to BirdCannon

        powerText.text = $"Power: {Cannon.Instance.CurrentPower}";
        Cannon.Instance.OnAngleChanged += AngleValueChanged;

        angleText.text = $"Angle: {Cannon.Instance.Angle}";
        Cannon.Instance.OnPowerChanged += PowerValueChanged;

        scoreText.text = $"Score: {Cannon.Instance.Score}";
        Cannon.Instance.OnScoreValueChanged += ScoreValueChanged;
    }

    private void AngleValueChanged(float angle) => angleText.text = $"Angle: {(int)angle}";

    private void PowerValueChanged(float power) => powerText.text = $"Power: {(int)power}";

    private void ScoreValueChanged(int score) => scoreText.text = $"Score: {score}";

    private void OnDestroy()
    {
        if (Cannon.Instance != null)
        {
            Cannon.Instance.OnAngleChanged -= AngleValueChanged;
            Cannon.Instance.OnPowerChanged -= PowerValueChanged;
            Cannon.Instance.OnScoreValueChanged -= ScoreValueChanged;
        }
    }
}
