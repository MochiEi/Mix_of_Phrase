using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveColorBlock : MonoBehaviour
{
    private enum SelectColor
    {
        Red, Blue
    };
    [SerializeField] SelectColor selectColor;

    public void IsActiveRed()
    {
        selectColor = SelectColor.Red;
    }
    public void IsActiveBlue()
    {
        selectColor = SelectColor.Blue;
    }

    public string ActiveColor()
    {
        return selectColor.ToString();
    }
}
