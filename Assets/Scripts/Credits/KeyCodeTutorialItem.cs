//////////////////////////////////////////////
///                                        ///
///         Last Day - Source Code         ///
///                                        ///
///    Lead Programmer   Second Programmer ///
///      Daniel Lause       Julian Hopp    ///
///                                        ///
//////////////////////////////////////////////

using System;
using UnityEngine;

[Serializable]
public class KeyCodeTutorialItem : MonoBehaviour
{
    public KeyCode Code;

    private void Update()
    {
        if (Input.GetKeyDown(Code))
            gameObject.SetActive(false);
    }
}
