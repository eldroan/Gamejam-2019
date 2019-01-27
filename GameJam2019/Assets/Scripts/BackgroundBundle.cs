using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[Serializable]
public class Background
{
    public int Id;
    public string Name;
    public Sprite Sprite;
}

public class BackgroundBundle : MonoBehaviour
{
    [SerializeField]
    private Background[] background;

    public static BackgroundBundle Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);
    }

    public Background GetRandomBackground()
    {
        return background.Select(x => x).Where(x => x.Id == UnityEngine.Random.Range(0, background.Length - 1)).FirstOrDefault();
    }

    public Background GetBackground(int id)
    {
        return background.Select(x => x).Where(x => x.Id == id).FirstOrDefault();
    }
}

