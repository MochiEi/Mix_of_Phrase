using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtnGimmickController01 : MonoBehaviour, ActiveCheck
{
    private bool isActive;

    private int initlamp;
    private MonoBehaviour[] lamp = new MonoBehaviour[5];
    private ActiveCheck[] trigger = new ActiveCheck[5];

    void Start()
    {
        initlamp = 0;

        foreach (Transform child in transform)
        {
            if(child.CompareTag("Lamp"))
            {
                lamp[initlamp] = child.GetComponent<MonoBehaviour>();
                initlamp++;
            }                
        }

        for (int i = 0; i < 5; i++)
        {
            trigger[i] = lamp[i] as ActiveCheck;
        }
    }

    void Update()
    {
        int activeChack = 0;

        foreach(ActiveCheck Lamp in trigger)
        {
            if(Lamp.IsActive())
            activeChack++;
        }

        isActive = activeChack == 5;
    }

    public bool IsActive()
    {
        return isActive;
    }
}
