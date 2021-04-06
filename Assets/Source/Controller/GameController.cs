using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // other managers and controllers
    public ScreenManager screenManager;
    public InstantiateManager instantiateManager;
    public PoolingController poolingController;
    public LevelController levelController;

    // GameObjects from unity
    public GameObject lettermodel;
    public GameObject letterGrid;
    public GameObject letterParent;
    public Image BGImage;
    public List<Sprite> images;

    public enum gameStates {ready, game, end};
    public gameStates CurrentState;
    public LevelModel currentLevel;
    public List<Letter> letters;
    public List<Word> wordbase;
    public Letter currentLetter;
    public GameObject model;
    public bool firstcall = true;

    public Animator wrongAnimator;

    public void Awake()
    {
        // make inializations   
        screenManager.Initialize();
        levelController.Initialize();
        poolingController.Initialize();
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
        wordbase.Capacity = len;
        for (int i = 0; i < len; i++)
        {
            Word word = PoolingController.PoolingManager.GetWordBase();
            word.called = true;
            word.SetActive(new Vector2(spos, -910));
            wordbase.Add(word);
            spos += step;
        }
    }

    public void CreateLetterModels()
    {
        int len = currentLevel.word.Length;
        string word = currentLevel.word;
        int i = 0;

        for (int j = 0; j < len; j++)
        {
            Letter letter = PoolingController.PoolingManager.GetLetter();
            letter.called = true;
            letter.letter.text = word[i++].ToString();
            letter.SetActive(Helper.GetRandom(new Vector2(-480, -600), new Vector2(480, 0)));
            letters.Add(letter);
        }

        model = instantiateManager.CreateModel(currentLevel.model, letterParent.transform);
        if (currentLevel.model.name == "Scissors")
        {
            model.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
        model.transform.localPosition = new Vector3(0.0f, 200.0f, 0.0f);
        model.transform.localScale = new Vector3(250, 250, 1);
        
    }

    public void OnReadyState()
    {
        screenManager.ShowScreen(0);
    }

    public void OnGameState()
    {
        if (!firstcall)
        {
            // play confetti for 1 or 2 secs
            ClearScene();
            levelController.NextLevel();
        }

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
        // play confetti animation for 2 or 3 secs
        screenManager.ShowScreen(2);
    }

    public void ClearScene()
    {
        Object.Destroy(model);
        poolingController.CleanScene();
        letters.Clear();
        wordbase.Clear();
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
                word.letterInserted = true;
                word.insertedLetter = letter;
                CheckLetter(letter, word, i);
            }
        }
        letter.rectTransform.anchoredPosition = (letterPos);
        currentLetter = null;
    }

    public void CheckLetter(Letter letter, Word tempWord, int i)
    {
        
        if (tempWord.insertedLetter.letter.text != currentLevel.word[i].ToString())
        {
            // wrong letter make red
            // if count > 0, decrement correctCount by 1
            wrongAnimator.gameObject.SetActive(true);
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
            ChangeState(gameStates.end);
        }
    }

    public void BGChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // BG #1
            BGImage.sprite = images[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // BG #2
            BGImage.sprite = images[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // BG #3
            BGImage.sprite = images[2];
        }
    }

    private void Update()
    {
        BGChange();
    }
}
