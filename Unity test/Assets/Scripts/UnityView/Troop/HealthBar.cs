using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Gradient gradient;
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;

    public float Value
    {
        get => slider.value;
        set
        {
            slider.value = value;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }

    public void Initialize(float maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;
        fill.color = gradient.Evaluate(1f);
    }
}
