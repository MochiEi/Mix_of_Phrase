using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhraseWindowAlphaController : MonoBehaviour
{
    [SerializeField] Collider2D coll;
    private Collider2D[] result = new Collider2D[3];
    private ContactFilter2D filter = new ContactFilter2D();

    private CanvasGroup canvas;

    private float alpha;
    private bool alphaDown;

    private float delayTime;
    [SerializeField] float speed;

    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        alpha = canvas.alpha;

        filter.useLayerMask = true;
        filter.SetLayerMask(1 << 7);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            delayTime = 2;

        delayTime -= Time.deltaTime * speed;
        delayTime = Mathf.Max(delayTime, 0);

        if (delayTime == 0)
            Overlap();
        else
            alpha += Time.deltaTime * speed;

        alpha = Mathf.Clamp(alpha, 0, 1);

        canvas.alpha = alpha;
    }

    private void Overlap()
    {
        int count = coll.OverlapCollider(filter, result);

        if (count > 0)
            alpha -= Time.deltaTime * speed;
        else
            alpha += Time.deltaTime * speed;
    }
}
