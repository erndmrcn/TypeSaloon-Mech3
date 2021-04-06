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
        // zDistance = Mathf.Abs(startPos.z - Camera.main.transform.position.z);
        // offset = startPos - Camera.main.ScreenToWorldPoint(
           // new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistance));
    }

    public void PointerDown()
    {
        //Vector2 pos;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        //Vector3 temp = pos;
        //temp.z = 0;
        //transform.localPosition = temp;
        
        gameController.currentLetter = this;
        Vector3 myScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        transform.position =
           Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, myScreenPos.z));
        //Vector3 pos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
    
        //transform.position = (pos);
    }

    public void PointerUp()
    {
        gameController.OnLetterSet(this);
    }
}
