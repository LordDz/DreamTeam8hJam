using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthImg : MonoBehaviour
{
    public int PlayerNr = 1;
    private float height;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        height = image.rectTransform.rect.height;
    }

    public void SetHealth(float originalHealth, float currentHealth)
    {
        float scale = currentHealth / originalHealth;
        //height -= damage;
        image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height * scale);
    }
}
