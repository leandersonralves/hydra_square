using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public void OnClick ()
    {
        Grid.instance.OnMove(this);
    }
}
