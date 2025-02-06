using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MemoryGimmickController02 : MonoBehaviour
{
    private enum BlockColor { Red, Blue };

    [System.Serializable]
    private struct Result
    {
        public Transform block;
        public BlockColor blockColor;
        public MonoBlockController controller;
    };
    [SerializeField] Result[] result;

    [SerializeField] MonoBehaviour trigger;
    private ActiveCheck activeCheck;

    [SerializeField] Collider2D resetTrigger;
    private Collider2D[] resetResult = new Collider2D[3];
    private ContactFilter2D filter = new ContactFilter2D();

    private bool isActive;
    private bool setActive;

    [SerializeField] SpriteRenderer colorHints;
    private Color32 mono = new Color32(190, 190, 190, 255);
    private Color32 red = new Color32(195, 80, 79, 255);
    private Color32 blue = new Color32(96, 135, 187, 255);
    private Tween hints;

    void Start()
    {
        result = transform.Cast<Transform>()
            .Where(obj => obj.gameObject.CompareTag("ColorBlock"))
            .Select(obj => new Result { block = obj, blockColor = BlockColor.Red ,controller = obj.GetComponent<MonoBlockController>() })
            .ToArray();
        activeCheck = trigger as ActiveCheck;

        filter.useLayerMask = true;
        filter.SetLayerMask(1 << 7);

        setActive = true;
    }

    void Update()
    {
        if (activeCheck.IsActive())
        {
            isActive = true;
        }

        if (isActive && setActive)
        {
            setActive = false;
            SetColor();

            for (int i = 0; i < result.Length; i++)
                result[i].controller.enabled = true;
        }

        if (isActive)
            Overlap();
    }

    private void Overlap()
    {
        int count = resetTrigger.OverlapCollider(filter, resetResult);

        if(count > 0)
        {
            isActive = false;
            setActive = true;

            for (int i = 0; i < result.Length; i++)
                result[i].controller.enabled = false;
        }
    }

    private void Dotween()
    {
        hints = DOTween.Sequence()
            .AppendCallback(() =>
            {
                for(int i = 0;i < result.Length; i++)
                {
                    if (result[i].blockColor == BlockColor.Red)
                    {

                    }
                }
            })
            .SetAutoKill(false).Pause();
    }

    void SetColor()
    {
        BlockColor[] colors = (BlockColor[])System.Enum.GetValues(typeof(BlockColor));

        for (int i = 0; i < result.Length; i++)
            result[i].blockColor = colors[Random.Range(0, colors.Length)];
    }

    public string GetColor(Transform setObj)
    {
        foreach (var obj in result)
        {
            if(setObj == obj.block)
            {
                return obj.blockColor.ToString();
            }
        }

        return null;
    }
}