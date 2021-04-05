using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    // Base class for the other screens
    // functions can be overloaded

    // initialize function
    public virtual void Initialize()
    {
        Disable();
    }

    // show current screen
    public virtual void Show()
    {
        // play show animation if there exists
        gameObject.SetActive(true);

    }

    // hide current screen to show another
    public virtual void Hide()
    {
        // play hide animation if there exists
        Disable();
    }

    // make setactive(false) to disable
    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }
}
