using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    private TextMesh textPopup;
    private TouchInput touchInput;

    private void Awake()
    {
        textPopup = GetComponent<TextMesh>();
        touchInput = FindObjectOfType<TouchInput>();
    }


    public void CreateTextPopup(int text)
    {
        gameObject.transform.SetParent(null);
        gameObject.transform.position = ClampPopupToBasket();
        textPopup.color = Color.green;
        textPopup.text = "+" + text.ToString();
        StartCoroutine(ColorPalette.FadeAlpha(textPopup));
        Destroy(gameObject, 2f);
    }


    private Vector3 ClampPopupToBasket()
    {
        float xPadding = -4.5f;
        float yPadding = 6.5f;
        Vector3 basketPosition = FindObjectOfType<Basket>().gameObject.transform.position;

        float xPos = Mathf.Clamp(basketPosition.x + xPadding, touchInput.dropWidth.x, touchInput.dropWidth.y);
        basketPosition.x = xPos;
        basketPosition.y += yPadding;

        return basketPosition;
    }
}
