using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthImg : MonoBehaviour
{
    public Image image;
    public float height;

    public void Damage(float originalHealth, float currentHealth)
    {
        //height -= damage;
        image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }
}
