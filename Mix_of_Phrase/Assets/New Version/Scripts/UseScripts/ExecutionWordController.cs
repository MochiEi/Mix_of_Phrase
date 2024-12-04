using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutionWordController : MonoBehaviour
{
    private PhraseController controller;

    [SerializeField] CreateBoxController createBox;

    void Start()
    {
        controller = GetComponent<PhraseController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (createBox != null)
            {
                if (controller.ExecutionWord() == "create box")
                {
                    createBox.CreateBox();
                }

                if (controller.ExecutionWord() == "break box")
                {
                    createBox.DelateBox();
                }
            }
        }
    }
}
