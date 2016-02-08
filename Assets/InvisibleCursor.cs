using UnityEngine;
using System.Collections;

public class InvisibleCursor : MonoBehaviour {

    [Tooltip("Makes the Mouse Cursor invisible if box is checked.")]
    public bool CursorIsVisible = false;

    void Awake()
    {
        Cursor.visible = CursorIsVisible;
    }
}
