using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType { Default, Attack, Heal, Buff, Debuff, Bleed, Poison, Stun }
public enum Team { Default, Player, Enemy }

[System.Serializable]
public class Skill
{
    public SkillContainer.SkillName skillName;
    //public int damage;
    public SkillType type;
    public int value;
    public Team targetTeam;

    public int[] possiblePosition;
    public int[] possibleTarget;

    
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
