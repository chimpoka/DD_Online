using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState_SkillNotSelected : PlayerTurnState
{ 
    public PlayerTurnState_SkillNotSelected(BattleState_PlayerTurn playerTurn)
        : base(playerTurn)
    { }

    public override void selectHero()
    {
            Hero hero = getSelectedHero();
            onSelectHero(hero);
    }

    public override void selectSkill()
    {
        //BoxCollider2D collider = (BoxCollider2D)checkHitCollider();
        //if (collider != null)
        //{
        //    Skill skill = getSelectedSkill(collider);
        //    onSelectSkill(skill);
        //}
    }



    private void onSelectSkill(Skill skill)
    {
        if (skill != null)
        {
            //setSelector(hero);
            //setSkills(hero);
        }
    }

    private Skill getSelectedSkill(BoxCollider2D collider)
    {
        foreach (Hero hero in playerTurn.battle.heroes)
        {
            if (hero.isSelected == true)
            {
                foreach (Skill skill in hero.skillList)
                {
                    if (skill.collider == collider)
                        return skill;
                }
            }
        }

        return null;
    }

    private void setSkillSelector(Skill skill)
    {
        playerTurn.battle.skillSelectorPrefabs = new GameObject[2];
    }





    private void onSelectHero(Hero hero)
    {
        if (hero != null)
        {
            setHeroSelector(hero);
            setSkills(hero);
        }
    }

    private void setSkills(Hero hero)
    {
        playerTurn.battle.createSkills(hero);

        bool enableFlag;
        if (hero == playerTurn.battle.currentHero)
            enableFlag = true;
        else
            enableFlag = false;

        hero.enableSkills(enableFlag);
    }

    private void setHeroSelector(Hero hero)
    {
        playerTurn.battle.selectHero(hero);
    }

    private Hero getSelectedHero()
    {
        BoxCollider2D collider = (BoxCollider2D)checkHitCollider();
        if (collider != null)
        {
            foreach (Hero hero in playerTurn.battle.heroes)
            {
                if (hero.collider == collider)
                    return hero;
            }
        }

        return null;
    }

    private Collider2D checkHitCollider()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
                return hit.collider;
        }

        return null;
    }
}
