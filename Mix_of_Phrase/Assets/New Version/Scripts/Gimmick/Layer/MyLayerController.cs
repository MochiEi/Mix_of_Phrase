using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLayerController : MonoBehaviour
{
    private enum MyLayer
    {
        Front, Middle, Back
    };
    [SerializeField] MyLayer mylayer;

    private bool selectLayer;

    private SpriteRenderer ground;

    void Start()
    {
        if (mylayer == MyLayer.Front)
            gameObject.layer = 10;
        if (mylayer == MyLayer.Middle)
            gameObject.layer = 11;
        if (mylayer == MyLayer.Back)
            gameObject.layer = 12;

        ground = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        selectLayer = !Physics2D.GetIgnoreLayerCollision(7, gameObject.layer);

        if (selectLayer)
        {
            ground.sortingOrder = 0;
            ground.color = new Color(0, 0, 0, 1);
        }
        else
        {
            ground.sortingOrder = -10;
            ground.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
    }
}
