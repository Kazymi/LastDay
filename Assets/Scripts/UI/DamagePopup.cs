using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private float dessapiar;
    private Color textColor;

    private Vector3 moveVector;

    private void Update()
    {
        var speed = 2f;
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 1f * Time.deltaTime;
        dessapiar -= Time.deltaTime;

        var scaleIncrease = 1f;
        if (dessapiar > 0.5)
        {
            transform.localScale += new Vector3(0.001f,0.001f,0.001f) * scaleIncrease * Time.deltaTime;
        }
        else
        {
            transform.localScale -= new Vector3(0.001f,0.001f,0.001f) * Time.deltaTime;
        }

        if (dessapiar < 0)
        {
            float speedD = 3f;
            textColor.a -= speedD * Time.deltaTime;
            text.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Setup(int damage, Color color, bool isCrit)
    {
        moveVector = new Vector3(Random.Range(-.4f,.4f), 1f) * 3;
        dessapiar = 1;
        text.color = color;
        textColor = text.color;
        if (isCrit)
        {
            text.fontSize = text.fontSize + (int) (text.fontSize * 0.25f);
        }

        text.SetText(damage.ToString());
    }
}