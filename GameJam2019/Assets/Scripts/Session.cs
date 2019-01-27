using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session : MonoBehaviour
{
    public CharacterType PlayerA { get; set; }
    public CharacterType PlayerB { get; set; }

    public static Session Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);
    }
}
