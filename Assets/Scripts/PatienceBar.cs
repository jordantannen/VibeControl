using UnityEngine;
using UnityEngine.UI;

public class PatienceBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetPatience(int patience)
    {
        slider.value = patience;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxPatience(int patience)
    {
        slider.maxValue = patience;
        slider.value = patience;
        fill.color = gradient.Evaluate(1f);
    }
}
