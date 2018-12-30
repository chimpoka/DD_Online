using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType { Default, Attack, Heal, Buff, Debuff, Bleed, Poison, Stun }
public enum TargetTeam { Default, Player, Enemy }

[System.Serializable]
public class Skill
{
    public SkillContainer.SkillName skillName;
    //public int damage;
    public Hero owner;
    public SkillType type;
    public int value;
    public TargetTeam targetTeam;
    public bool isEnabled;
    public GameObject prefab;

    public int[] possiblePosition;
    public int[] possibleTarget;

    public void setOwner(Hero hero)
    {
        owner = hero;
    }

    public void setImage()
    {
        if (prefab != null)
        {
            prefab.GetComponent<SpriteRenderer>().enabled = true;
            prefab.GetComponent<SpriteRenderer>().sprite =
               SkillSpritesContainer.getSprite("Skill_" + skillName.ToString());

        }
    }

    public void removeImage()
    {
        if (prefab != null)
            prefab.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void enable(bool flag)
    {
        isEnabled = flag;
        prefab.GetComponent<SpriteRenderer>().enabled = true;
        SpriteRenderer[] sprites = prefab.GetComponents<SpriteRenderer>();

        foreach (SpriteRenderer sprite in sprites)
        {
            Color color = new Color();
            color.r = sprite.color.r;
            color.g = sprite.color.g;
            color.b = sprite.color.b;
            if (flag == true)
                color.a = 1f;
            else
                color.a = 0.25f;

            sprite.color = color;
        }
    }

    //Skill(SkillContainer.SkillName name)
    //{

    //}

    //private void Start()
    //{
    //    if (type == SkillType.Buff || type == SkillType.Heal)
    //        team = Team.Player;
    //    else
    //        team = Team.Enemy;

    //    //damage = GetComponent<Hero>().damage;
    //}


}
