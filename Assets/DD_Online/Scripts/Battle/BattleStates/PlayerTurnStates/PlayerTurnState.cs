using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerTurnState
{
    public BattleState_PlayerTurn playerTurn;
    public PlayerTurnState(BattleState_PlayerTurn playerTurn)
    {
        this.playerTurn = playerTurn;
    }

    public virtual void execute() {}





    protected Skill getSkillByRaycast()
    {
        BoxCollider2D collider = (BoxCollider2D)checkHitCollider();
        if (collider != null)
        {
            foreach (Hero hero in playerTurn.heroes)
            {
                if (hero.IsSelected == true)
                {
                    foreach (Skill skill in hero.skillList)
                    {
                        if (skill.collider == collider)
                            return skill;
                    }
                }
            }
        }

        return null;
    }

    protected Hero getHeroByRaycast()
    {
        BoxCollider2D collider = (BoxCollider2D)checkHitCollider();
        if (collider != null)
        {
            foreach (Hero hero in playerTurn.heroes)
            {
                if (hero.collider == collider)
                    return hero;
            }
        }

        return null;
    }

    private Collider2D checkHitCollider()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (hit.collider != null)
            return hit.collider;

        return null;
    }
}