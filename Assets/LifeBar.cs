using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    private Life pLife;
    [SerializeField] private SpriteRenderer frame;
    [SerializeField] private RawImage valueBar;

    private Color emptyColour;
    private Color fullColour;

    // Start is called before the first frame update
    void Start()
    {
        emptyColour = Color.red;
        fullColour = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        if (pLife == null && GameManager.player != null)
        {
            pLife = GameManager.player.GetComponent<Life>();
        }
        else
        {
            if (pLife != null)
            {
                Color currentColour = getColour(pLife.current, pLife.maximum);

                valueBar.GetComponent<RectTransform>().sizeDelta = new Vector2(pLife.current, 10);

                valueBar.color = currentColour;
                frame.color = currentColour;
            }
        }
    }

    private Color getColour(int value, int max)
    {
        float decValue = (float)value / (float)max;
        return Color.Lerp(emptyColour, fullColour, decValue);
    }
}
