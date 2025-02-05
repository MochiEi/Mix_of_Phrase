using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhraseWindowAlphaController : MonoBehaviour
{
    [SerializeField] Collider2D coll;
    private Collider2D[] result = new Collider2D[3];
    private ContactFilter2D filter = new ContactFilter2D();

    void Start()
    {
        filter.useLayerMask = true;
        filter.SetLayerMask(1 << 7);
    }

    void Update()
    {

    }

    private void Overlap()
    {
        int count = coll.OverlapCollider(filter, result);

        if (count > 0)
        {

        }
    }
}
