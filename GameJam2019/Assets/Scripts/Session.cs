using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session : MonoBehaviour
{
    public CharacterType PlayerA { get; set; }
    public CharacterType PlayerB { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
