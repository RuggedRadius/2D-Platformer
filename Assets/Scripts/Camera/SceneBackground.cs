﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBackground : MonoBehaviour
{
    public Sprite[] layers;
    private GameObject backgroundParent;

    public float backgroundDistance = 20;

    void Start()
    {
        backgroundParent = Camera.main.transform.Find("Background").gameObject;

        for (int i = 0; i < layers.Length; i++)
        {
            // Create main instance
            GameObject newLayer = new GameObject("Layer " + (i + 1));
            newLayer.transform.SetParent(backgroundParent.transform);
            newLayer.transform.localScale = Vector3.one;
            newLayer.transform.localPosition = Vector3.zero;
            //newLayer.transform.localPosition += new Vector3(0f, 0f, 0f); // Adjustments

            SpriteRenderer renderer = newLayer.AddComponent<SpriteRenderer>();
            renderer.sprite = layers[i];
            renderer.sortingLayerName = "Background";
            renderer.sortingOrder = i;

            SceneParallax parallax = newLayer.AddComponent<SceneParallax>();
            parallax.parallexEffect = ((float) layers.Length - i) / (float) layers.Length;
            parallax.cam = Camera.main.gameObject;

            // Calculate horizontal offset
            int spritePPU = 16; // Set the same as the sprite's Pixel Per Unit value.
            float horizontalOffest = layers[i].rect.width / spritePPU;

            // Create right side instance
            GameObject layerRight = new GameObject("Layer " + (i + 1) + " Right");
            layerRight.transform.SetParent(newLayer.transform);
            layerRight.transform.localScale = Vector3.one;
            //layerRight.transform.localPosition = new Vector3(horizontalOffest, 0f, backgroundDistance);
            layerRight.transform.localPosition = Vector3.zero;

            SpriteRenderer rendererRight = layerRight.AddComponent<SpriteRenderer>();
            rendererRight.sortingLayerName = "Background";
            rendererRight.sprite = layers[i];
            rendererRight.sortingOrder = i;

            // Create left side instance
            GameObject layerLeft = new GameObject("Layer " + (i + 1) + " Left");
            layerLeft.transform.SetParent(newLayer.transform);
            layerLeft.transform.localScale = Vector3.one;
            layerLeft.transform.localPosition = Vector3.zero;

            SpriteRenderer rendererLeft = layerLeft.AddComponent<SpriteRenderer>();
            rendererLeft.sortingLayerName = "Background";
            rendererLeft.sprite = layers[i];
            rendererLeft.sortingOrder = i;

        }
    }
}
