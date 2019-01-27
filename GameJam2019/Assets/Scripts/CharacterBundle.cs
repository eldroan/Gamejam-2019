using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Character
{
    public CharacterType Id;
    public string Name;
    public int Hits;
    public GameObject Prefab;
    public Sprite Avatar;
}

public class CharacterBundle : MonoBehaviour
{
    [SerializeField]
    private Character[] characters;

    public static CharacterBundle Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);
    }

    public Character GetCharacter(CharacterType id)
    {
        return characters.Select(x => x).Where(x => x.Id == id).FirstOrDefault();
    }
}
