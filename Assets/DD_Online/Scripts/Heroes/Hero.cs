using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroClass { Defender, Priest, Hunter, Berserk, Fairy, Mage }
public enum TurnState { Done, Undone, InProcess }

// TODO: Delete MonoBehaviour Inheritance
public class Hero : MonoBehaviour
{
    public enum Team { Left, Right }

    public int pureDamage;
    public int pureHealth;
    public int pureSpeed;

    public HeroClass heroClass;
    public int damage;
    public int health;
    public int speed;

    public int ownerID;
    public Team team;
    public int position;
    public TurnState turnState;
    new public BoxCollider2D collider;

    public List<SkillContainer.SkillName> skillNames;

    [HideInInspector]
    public List<Skill> skillList;

    private void Awake()
    {
        turnState = TurnState.Undone;

        foreach (SkillContainer.SkillName skillName in skillNames)
        {
            Skill skill = SkillContainer.getSkill(heroClass, skillName);
            skill.setOwner(this);
            skillList.Add(skill);
        }
    }



    public void SetSkillImages()
    {
        foreach (Skill skill in skillList)
        {
            skill.setImage();
        }
    }

    void UseSkill(Skill skill)
    {

    }

    //List<Skill> GetSkills()
    //{
    //    return skillList;
    //}
}
