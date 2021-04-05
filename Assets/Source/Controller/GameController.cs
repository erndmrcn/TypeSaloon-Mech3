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
    public GameObject model;
    public bool firstcall = true;
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

        model = instantiateManager.CreateModel(currentLevel.model, letterParent.transform);
        model.transform.localPosition = new Vector3(0.0f, 200.0f, 0.0f);
        model.transform.localScale = new Vector3(250, 250, 1);
        
    }

    public void OnReadyState()
    {
        screenManager.ShowScreen(0);
    }

    public void OnGameState()
    {
        if(!firstcall)
            ClearScene();
        currentLevel = levelController.GetLevel();
        CreateGrids();
        CreateLetterModels();
        screenManager.ShowScreen(1);
        firstcall = false;
    }

    public void OnEndState()
    {
        // if completed, cheers
        // else indicate that the word is wrong
        screenManager.ShowScreen(2);
    }

    public void ClearScene()
    {
        Object.Destroy(model);
        foreach (Letter item in letters)
        {
            item.gameObject.SetActive(false);
            Object.Destroy((Letter) item);
        }
        foreach(Word item in wordbase)
        {
            item.gameObject.SetActive(false);
            Object.Destroy((Word) item);
        }
    }
    
    public void OnLetterSet(Letter letter)
    {
        Vector2 letterPos = letter.rectTransform.anchoredPosition;
        int len = currentLevel.word.Length;
        float distance;
        letter.SetDefault();
        for (int i = 0; i < len; i++)
        {
            Word word = wordbase[i];
            Vector2 wordPos = word.rectTransform.anchoredPosition;
            distance = Vector2.Distance(wordPos, letter.rectTransform.anchoredPosition);
            // distance = Vector2.Distance(wordPos, letterPos);
            if (distance < 50f)
            {
                letterPos = wordPos;
                Debug.Log("" + distance);
                word.letterInserted = true;
                word.insertedLetter = letter;
                letter.word = word;
                CheckLetter(letter, word, i);
            }
        }
        letter.rectTransform.anchoredPosition = (letterPos);
        currentLetter = null;
    }

    public void CheckLetter(Letter letter, Word tempWord, int i)
    {
        
        if (tempWord.insertedLetter.letter.text != ""+currentLevel.word[i])
        {
            // wrong letter make red
            // if count > 0, decrement correctCount by 1
            letter.SetWrong();
        }
        else
        {
            // correct letter increment count by 1
            // make letter bg green
            letter.SetCorrect();
        }

        bool isGameEnd = true;
        // if correctCount == len next level
        // if letter.word == null original bg
        for (int j = 0; j < letters.Count; j++)
        {
            if (letters[j].isCorrect == false)
            {
                isGameEnd = false;
                break;
            }
        }

        if (isGameEnd)
        {
            // nextLevel
            levelController.NextLevel();
            if (levelController.currentLevel == levelController.levels.Length)
            {
                // end game
                ChangeState(gameStates.end);
            }
            else
                ChangeState(gameStates.game);
        }
    }

    private void Update()
    {
        
    }
}
