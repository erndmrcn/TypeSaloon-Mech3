using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateManager : MonoBehaviour
{
    public GameObject prefab;
    public GameObject prefab1;
    public Letter LetterSample;

    public List<GameObject> CreateLetter(int number, Transform parent)
    {
        List<GameObject> returnList = new List<GameObject>();

        for (int i = 0; i < number; i++)
        {
            returnList.Add(Instantiate(prefab, parent));
        }

        return returnList;
    }

    public List<Letter> CreateLetters(int number, Transform parent)
    {
        List<Letter> returnList = new List<Letter>();

        for (int i = 0; i < number; i++)
        {
            returnList.Add(Instantiate(LetterSample, parent));
        }

        return returnList;
    }

    public List<GameObject> CreateGrid(int number, Transform parent)
    {
        List<GameObject> returnList = new List<GameObject>();

        for (int i = 0; i < number; i++)
        {
            returnList.Add(Instantiate(prefab1, parent));
        }

        return returnList;
    }

    public GameObject CreateModel(GameObject prefab, Transform parent)
    {
        return Instantiate(prefab, parent);
    }
}
