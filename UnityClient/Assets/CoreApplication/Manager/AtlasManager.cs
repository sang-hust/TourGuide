using System;
using UnityEngine;
using UnityEngine.U2D;

public class AtlasManager : PersistentSingleton<AtlasManager>
{
    public SpriteAtlas ui;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public Sprite GetSprite(string nameSprite)
    {
        return ui.GetSprite(nameSprite);
    }
}
