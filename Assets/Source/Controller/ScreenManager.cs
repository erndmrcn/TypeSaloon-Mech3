using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public Screen activeScreen;
    [SerializeField]
    private List<Screen> screens;

    public void Initialize()
    {
        foreach (Screen item in screens)
        {
            item.Initialize();
        }

        activeScreen = screens[0];
        activeScreen.Show();
    }

    public void ShowScreen(int index)
    {
        activeScreen.Hide();
        activeScreen = screens[index];
        activeScreen.Show();
    }

}
