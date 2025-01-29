using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraController : MonoBehaviour
{
    void Awake()
    {
        float posY = 0;
        posY = IsSelect.isSelected ? -10 : 0;
        transform.position = new Vector3(0, posY , -10); 
    }

    void Update()
    {
        
    }
}
