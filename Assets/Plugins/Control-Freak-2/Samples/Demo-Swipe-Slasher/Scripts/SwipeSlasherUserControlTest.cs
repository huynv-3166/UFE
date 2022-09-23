// -------------------------------------------
// Control Freak 2
// Copyright (C) 2013-2019 Dan's Game Tools
// http://DansGameTools.blogspot.com
// -------------------------------------------

using UnityEngine;
using ControlFreak2;

namespace ControlFreak2.Demos.SwipeSlasher
{
    public class SwipeSlasherUserControlTest : MonoBehaviour
    {

        void Update()
        {

            if (CF2Input.GetButtonDown("P1LeftSwipeL"))
                Debug.Log("P1LeftSwipeL");
            else if (CF2Input.GetButtonDown("P1LeftSwipeR"))
                Debug.Log("P1LeftSwipeR");
            else if (CF2Input.GetButtonDown("P1RightSwipeL"))
                Debug.Log("P1RightSwipeL");
            else if (CF2Input.GetButtonDown("P1RightSwipeR"))
                Debug.Log("P1RightSwipeR");
            else if (CF2Input.GetButtonDown("P1RightTab"))
                Debug.Log("P1RightTab");
        }
    }
}
