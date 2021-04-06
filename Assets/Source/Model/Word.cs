using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word : MonoBehaviour
{
    public RectTransform rectTransform;
    public bool letterInserted;
    public Letter insertedLetter;
    public bool called;

    public void SetActive(Vector2 anchorPos)
    {
        gameObject.SetActive(true);
        rectTransform.anchoredPosition = anchorPos;
    }
}
