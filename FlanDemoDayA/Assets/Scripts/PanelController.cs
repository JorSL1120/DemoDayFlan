using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public GameObject panel;
    public float targetAlpha = 1f;
    public float fadeSpeed = 2f;

    private Image panelImage;
    private bool fadeIn = false;

    void Start()
    {
        if (panel != null)
        {
            panelImage = panel.GetComponent<Image>();

            if (panelImage != null)
            {
                Color c = panelImage.color;
                panelImage.color = new Color(c.r, c.g, c.b, 0f);
            }
        }
    }

    void Update()
    {
        if (fadeIn && panelImage != null)
        {
            Color c = panelImage.color;
            float newAlpha = Mathf.MoveTowards(c.a, targetAlpha, Time.deltaTime * fadeSpeed);
            panelImage.color = new Color(c.r, c.g, c.b, newAlpha);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fadeIn = true;
        }
    }
}
