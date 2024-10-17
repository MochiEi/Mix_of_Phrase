using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedVariableController : MonoBehaviour
{
    public static SharedVariableController instance;

    public bool initGame = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}