using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState_SkillSelected : PlayerTurnState
{
    public PlayerTurnState_SkillSelected(BattleState_PlayerTurn playerTurn, Skill skill)
        : base(playerTurn)
    {
        onSelectSkill(skill);
    }

    public override void selectHero()
    {
       // Hero hero = getSelectedHero();
       // onSelectHero(hero);
    }

    public override void selectSkill()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Skill skill = getSkillByRaycast();
            if (skill != null)
                onSelectSkill(skill);
        }
    }



    private void onSelectSkill(Skill skill)
    {
        if (skill != null && skill.owner == playerTurn.battle.currentHero)
        {
            if (skill.isSelected == false)
            {
                playerTurn.battle.selectSkill(skill);
            }
            else
            {
                skill.setSelector(false);
                playerTurn.playerTurnState = new PlayerTurnState_SkillNotSelected(playerTurn);
            }
        }
    }
}
