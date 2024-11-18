using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage; // Reference to the fill image
    [SerializeField] private Gradient colorGradient; // Gradient for health colors

   
    public void UpdateFill(float fillAmount)
    {
       
        fillAmount = Mathf.Clamp01(fillAmount); // Ensure the value is between 0 and 1

        if (fillImage != null)
        {
            fillImage.fillAmount = fillAmount; // Update the fill amount
            fillImage.color = colorGradient.Evaluate(fillAmount); // Update the color
        }
    }
}