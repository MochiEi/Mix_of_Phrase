using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController : MonoBehaviour, ActiveCheck
{
    [Header("トリガー")]
    [SerializeField] MonoBehaviour[] setTrigger;
    private ActiveCheck[] trigger;

    [Header("点灯速度")]
    [SerializeField] float speed;

    private int activeCount;
    private bool isActive;

    private SpriteRenderer sprite;
    private float lighting;

    void Start()
    {
        trigger = new ActiveCheck[setTrigger.Length];

        for (int i = 0; i < setTrigger.Length; i++)
            trigger[i] = setTrigger[i] as ActiveCheck;

        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        activeCount = 0;

        foreach (ActiveCheck button in trigger)
        {
            if (button.IsActive())
                activeCount++;
        }

        activeCount = Mathf.Max(activeCount, 0);

        isActive = activeCount % 2 == 1;

        if (isActive)
            lighting += speed * Time.deltaTime;
        else
            lighting -= speed * Time.deltaTime;

        lighting = Mathf.Clamp(lighting, 0.4f, 1f);

        sprite.color = new Color(lighting, lighting, lighting);
    }

    public bool IsActive()
    {
        return isActive;
    }
}