using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    public GameController gameController;
    public Vector3 startPos;
    public Text letter;
    public Image BGimage;
    public RectTransform rectTransform;
    public bool isCorrect;
    public bool called;

    public void SetActive(Vector2 anchorPos)
    {
        gameObject.SetActive(true);
        rectTransform.anchoredPosition = anchorPos;
    }

    public void SetCorrect()
    {
        isCorrect = true;
        BGimage.color = Color.green;
    }

    public void SetWrong()
    {
        isCorrect = false;
        BGimage.color = Color.red;
    }

    public void SetDefault()
    {
        isCorrect = false;
        BGimage.color = Color.white;
    }

    public void PointerClick()
    {
        startPos = transform.localPosition;
    }

    public void PointerDown()
    {
        gameController.currentLetter = this;
        Vector3 myScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        transform.position =
           Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, myScreenPos.z));
    }

    public void PointerUp()
    {
        gameController.OnLetterSet(this);
    }
}
