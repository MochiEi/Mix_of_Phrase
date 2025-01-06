using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLayerController : MonoBehaviour
{
    public void FrontLayer()
    {
        Physics2D.IgnoreLayerCollision(7, 10, false);
        Physics2D.IgnoreLayerCollision(7, 11, true);
        Physics2D.IgnoreLayerCollision(7, 12, true);
    }
    public void MiddleLayer()
    {
        Physics2D.IgnoreLayerCollision(7, 10, true);
        Physics2D.IgnoreLayerCollision(7, 11, false);
        Physics2D.IgnoreLayerCollision(7, 12, true);
    }
    public void BackLayer()
    {
        Physics2D.IgnoreLayerCollision(7, 10, true);
        Physics2D.IgnoreLayerCollision(7, 11, true);
        Physics2D.IgnoreLayerCollision(7, 12, false);
    }
}
