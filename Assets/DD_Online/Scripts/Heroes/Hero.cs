using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroClass { Defender, Priest, Hunter, Berserk, Fairy, Mage }
public enum TurnState { Done, Undone, InProcess }

// TODO: Delete MonoBehaviour Inheritance
public class Hero : MonoBehaviour
{
    public int pureDamage;
    public int pureHealth;
    public int pureSpeed;

    public HeroClass heroClass;
    public int damage;
    public int health;
    public int speed;

    public int ownerID;
    public int position;
    public TurnState turnState;

    public List<SkillContainer.SkillName> skillNames;

    [HideInInspector]
    public List<Skill> skillList;

    private void Start()
    {
        turnState = TurnState.Undone;

        foreach (SkillContainer.SkillName skillName in skillNames)
        {
            skillList.Add(SkillContainer.getSkill(heroClass, skillName));
        }
    }



    void SetSkill(Skill skill)
    {

    }

    void UseSkill(Skill skill)
    {

    }

    //List<Skill> GetSkills()
    //{
    //    return skillList;
    //}
}
