using UnityEngine;
using UnityEngine.UI;

public class CrossFade : MonoBehaviour
{
    // Start is called before the first frame update
    private Image image;
    public float timeToFade = 1;

    private void Awake()
    {
        image = GetComponent<Image>();

    }

    private void Fade()
    {
        image.canvasRenderer.SetAlpha(0f);
        image.CrossFadeAlpha(1, timeToFade, true);
    }
    private void OnEnable()
    {
        Fade();

    }
}
