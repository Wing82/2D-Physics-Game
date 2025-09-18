using TMPro;
using UnityEngine;

public class InGame : MonoBehaviour
{
    public TMP_Text powerText;
    public TMP_Text angleText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        powerText = GetComponent<TMP_Text>();
        angleText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        powerText.text = $"Power: {Cannon.Instance.currentPower}";
        Cannon.Instance.OnAngleChanged += (float angle) => angleText.text = $"Angle: {angle}";

        angleText.text = $"Angle: {Cannon.Instance.angle}";
        Cannon.Instance.OnPowerChanged += (float power) => powerText.text = $"Power: {power}";
    }
}
