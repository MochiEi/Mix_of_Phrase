using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorBlockController : MonoBehaviour
{
    private enum BlockColor { Red, Blue };
    [SerializeField] BlockColor blockColor;

    [SerializeField] Transform colorBlockManager;
    private ActiveColorController colorController;

    private string thisColor;
    private Animator anim;

    void Start()
    {
        colorController = colorBlockManager.GetComponent<ActiveColorController>();

        thisColor = blockColor.ToString();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (thisColor == colorController.ActiveColor())
            anim.SetBool("isActive", true);
        else
            anim.SetBool("isActive", false);
    }
}