using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public LevelModel[] levels;
    public int currentLevel;

    public void Initialize()
    {

        currentLevel = 0;
    }

    public void NextLevel()
    {
        currentLevel = currentLevel + 1 < levels.Length ? currentLevel + 1 : 0;
    }

    public LevelModel GetLevel()
    {
        return levels[currentLevel];
    }
}

[System.Serializable]
public class LevelModel
{
    public string word;
    public GameObject model;

}