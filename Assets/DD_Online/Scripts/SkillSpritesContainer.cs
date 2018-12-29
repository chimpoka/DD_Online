using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpritesContainer : MonoBehaviour
{
    public Sprite[] skillSprites;
    public static Sprite[] SkillSprites;

    public Sprite defaultSprite;
    public static Sprite DefaultSprite;

    private void Start()
    {
        SkillSprites = skillSprites;
        DefaultSprite = defaultSprite;
    }

    public static Sprite getSprite(string name)
    {
        for (int i = 0; i < SkillSprites.Length; i++)
        {
            if (SkillSprites[i].name == name)
            {
                return SkillSprites[i];
            }
        }
        Debug.LogError("Can not find sprite " + name);
        return DefaultSprite;
    }
}
