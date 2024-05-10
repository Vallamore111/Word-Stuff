using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ColorPalette
{
    public static Color[] colors = new Color[8] 
    { 
            Color.white, 
            Color.black, 
            Color.red, 
            Color.blue, 
            Color.green, 
            Color.yellow, 
            Color.cyan, 
            Color.magenta
    };

    public static Color fadedBlack = new Color(0, 0, 0, .2353f);



    public static Color GetRandomColor()
    {
        Color newColor = colors[Random.Range(0, colors.Length)];
        return newColor;
    }


    public static Color GetColorNoBW()
    {
        Color newColor = colors[Random.Range(2, colors.Length)];
        return newColor;
    }


    public static IEnumerator FadeAlpha(Text text)
    {
        Color startColor = text.color;
        float alphaValue = startColor.a;
        float fadeSpeed = 0.75f;

        for (float time = 0; time < fadeSpeed; time += Time.deltaTime / fadeSpeed)
        {
            Color newColor = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(alphaValue, 0, time));
            text.color = newColor;
            yield return null;
        }

        text.gameObject.SetActive(false);
    }


    public static IEnumerator FadeAlpha(TextMesh text)
    {
        Color startColor = text.color;
        float alphaValue = startColor.a;
        float fadeSpeed = 0.75f;

        for (float time = 0; time < fadeSpeed; time += Time.deltaTime / fadeSpeed)
        {
            Color newColor = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(alphaValue, 0, time));
            text.color = newColor;
            yield return null;
        }

        text.gameObject.SetActive(false);
    }

}
