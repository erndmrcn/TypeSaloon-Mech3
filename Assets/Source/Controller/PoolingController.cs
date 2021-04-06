using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingController : MonoBehaviour
{
    public List<Letter> letters;
    public List<Word> wordbase;
    public static PoolingController PoolingManager;

    public void Initialize()
    {
        if (PoolingManager == null)
        {
            PoolingManager = this;
            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        foreach (Letter item in letters)
        {
            item.gameObject.SetActive(false);
        }
        foreach (Word item in wordbase)
        {
            item.gameObject.SetActive(false);
        }
    }

    public Letter GetLetter()
    {
        return letters.Find(x => x.called == false);
    }

    public Word GetWordBase()
    {
        return wordbase.Find(x => x.called == false);
    }

    public void CleanScene()
    {
        while (letters.Find(x => x.called == true))
        {
            Letter letter = letters.Find(x => x.called == true);
            letter.gameObject.SetActive(false);
            letter.called = false;
            letter.SetDefault();
        }

        while (wordbase.Find(x => x.called == true))
        {
            Word word = wordbase.Find(x => x.called == true);
            word.gameObject.SetActive(false);
            word.called = false;
        }
    }
}
