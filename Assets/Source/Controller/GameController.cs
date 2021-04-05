using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // other managers and controllers
    public ScreenManager screenManager;
    public InstantiateManager instantiateManager;
    public LevelController levelController;

    // GameObjects from unity
    public GameObject lettermodel;
    public GameObject letterGrid;
    public GameObject letterParent;

    public enum gameStates {ready, game, end};
    public gameStates CurrentState;
    public LevelModel currentLevel;
    public List<Letter> letters;
    public List<Word> wordbase;
    public Letter currentLetter;

    public void Awake()
    {
        // make inializations   
        screenManager.Initialize();
        levelController.Initialize();
        currentLetter = null;
    }

    public void ChangeState(gameStates state)
    {
        CurrentState = state;

        switch (CurrentState)
        {
            case (gameStates.ready):
                OnReadyState();
                break;
            case (gameStates.game):
                OnGameState();
                break;
            case (gameStates.end):
                OnEndState();
                break;
            default:
                Debug.LogError("Invalid game state!");
                break;
        }
    }

    public void CreateGrids()
    {
        int len = currentLevel.word.Length;
        int step = 100;

        
        int spos = -50 - (((len - 1)/2) * 100);
        wordbase = instantiateManager.CreateGrids(len, letterParent.transform);
        foreach (Word item in wordbase)
        {
            item.SetActive(new Vector2(spos, -910));
            spos += step;
        }
    }

    public void CreateLetterModels()
    {
        int len = currentLevel.word.Length;
        string word = currentLevel.word;
        int i = 0;
        letters = instantiateManager.CreateLetters(len, letterParent.transform);
        foreach (Letter item in letters)
        {
            item.letter.text = "" + word[i++];
            item.SetActive(Helper.GetRandom(new Vector2(-480, -600), new Vector2(480, 0)));
        }

        GameObject newModel = instantiateManager.CreateModel(currentLevel.model, letterParent.transform);
        newModel.transform.localPosition = new Vector3(0.0f, 400.0f, 0.0f);
        newModel.transform.localScale = new Vector3(250, 250, 1);
        
    }

    public void OnReadyState()
    {
        screenManager.ShowScreen(0);
    }

    public void OnGameState()
    {
        currentLevel = levelController.GetLevel();
        CreateGrids();
        CreateLetterModels();
        screenManager.ShowScreen(1);
    }

    public void OnEndState()
    {
        // if completed, cheers
        // else indicate that the word is wrong
        screenManager.ShowScreen(2);
    }

    public void OnLetterSet(Letter letter)
    {
        Vector2 letterPos = letter.rectTransform.anchoredPosition;
        int len = currentLevel.word.Length;
        float lastDistance = 1000f;
        float distance = 1000f;
        for (int i = 0; i < len; i++)
        {
            Word word = wordbase[i];
            Vector2 wordPos = word.rectTransform.anchoredPosition;
            distance = Vector2.Distance(wordPos, letterPos);
            if (distance < lastDistance)
            {
                lastDistance = distance;
                letterPos = wordPos;
                Debug.Log("" + distance);
                word.letterInserted = true;
                word.insertedLetter = letter;
                CheckLetter(word, i);
            }
            else {
                continue;
            }
        }
        letter.rectTransform.anchoredPosition = (letterPos);
        currentLetter = null;
    }

    public void CheckLetter(Word tempWord, int i)
    {
        if (tempWord.insertedLetter.letter.text != ""+currentLevel.word[i])
        {
            // wrong letter make red
            
        }
    }

    public void CheckWordBase()
    {
        
    }

    private void Update()
    {
        
    }
}
