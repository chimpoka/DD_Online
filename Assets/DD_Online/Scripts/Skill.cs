﻿using System.Collections;
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
    public BoxCollider2D collider;
    public GameObject selector;
    public bool isSelected;

    public int[] possiblePosition;
    public int[] possibleTarget;


    public bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            isSelected = value;
        }
    }


    public void setOwner(Hero hero)
    {
        owner = hero;
    }

    public void instantiate(Vector3 position)
    {
        if (prefab == null)
        {
            prefab = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Skills/Prefab_" + skillName.ToString()) as GameObject,
                    position, Quaternion.identity) as GameObject;
            collider = prefab.GetComponent<BoxCollider2D>();
        }
    }

    public void destroy()
    {
        if (prefab != null)
        {
            GameObject.Destroy(prefab);
            prefab = null;
            collider = null;
        }
    }

    public void enable(bool flag)
    {
        if (prefab != null)
        {
            isEnabled = flag;
            SpriteRenderer[] sprites = prefab.GetComponentsInChildren<SpriteRenderer>();

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
    }



    public void setSelected(bool flag)
    {
        IsSelected = flag;
    }
}
