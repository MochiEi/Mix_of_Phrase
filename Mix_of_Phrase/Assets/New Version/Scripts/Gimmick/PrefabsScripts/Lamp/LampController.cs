using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController : MonoBehaviour, ActiveCheck
{
    [Header("初期点灯")]
    [SerializeField] bool lighting;

    [Header("点灯速度")]
    [SerializeField] float speed;

    [Header("トリガー")]
    [SerializeField] MonoBehaviour[] setTrigger;
    private ActiveCheck[] trigger;

    private int activeCount;
    private bool isActive;

    private SpriteRenderer sprite;
    private float brightness;

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

        if (lighting)
            isActive = activeCount % 2 == 0;
        else
            isActive = activeCount % 2 == 1;

        if (isActive)
            brightness += speed * Time.deltaTime;
        else
            brightness -= speed * Time.deltaTime;

        brightness = Mathf.Clamp(brightness, 0.4f, 1f);

        sprite.color = new Color(brightness, brightness, brightness);
    }

    public bool IsActive()
    {
        return isActive;
    }
}