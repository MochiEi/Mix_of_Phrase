using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonoBlockController : MonoBehaviour
{
    [SerializeField] Transform colorBlockManager;
    private ActiveColorController colorController;

    private MemoryGimmickController02 gimmickController;

    private string correctColor;
    private Animator anim;

    void OnEnable()
    {
        colorController = colorBlockManager.GetComponent<ActiveColorController>();
        anim = GetComponent<Animator>();

        gimmickController = transform.parent.gameObject.GetComponent<MemoryGimmickController02>();
        correctColor = gimmickController.GetColor(transform);
    }

    void Update()
    {
        if (correctColor == colorController.ActiveColor())
            anim.SetBool("isActive", true);
        else
            anim.SetBool("isActive", false);
    }
}