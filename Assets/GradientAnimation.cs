using UnityEngine;
using UnityEngine.UI;

public class GradientAnimation : MonoBehaviour
{
    public Image buttonImage; // Reference to the button's Image component
    public Color color1 = Color.white;
    public Color color2 = Color.grey;
    public float duration = 1.0f;

    private void Update()
    {
        float t = Mathf.PingPong(Time.time / duration, 1);
        buttonImage.color = Color.Lerp(color1, color2, t);
    }
}
