using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateManager : MonoBehaviour
{
    public Word WordSample;
    public Letter LetterSample;

    public List<Letter> CreateLetters(int number, Transform parent)
    {
        List<Letter> returnList = new List<Letter>();

        for (int i = 0; i < number; i++)
        {
            returnList.Add(Instantiate(LetterSample, parent));
        }

        return returnList;
    }

    public List<Word> CreateGrids(int number, Transform parent)
    {
        List<Word> returnList = new List<Word>();

        for (int i = 0; i < number; i++)
        {
            returnList.Add(Instantiate(WordSample, parent));
        }

        return returnList;
    }

    public GameObject CreateModel(GameObject prefab, Transform parent)
    {
        return Instantiate(prefab, parent);
    }
}
