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
        if (Input.GetMouseButtonDown(0))
        {
            Hero hero = getHeroByRaycast();
            if (hero != null)
                playerTurn.battle.selectHero(hero);
        }
    }

    public override void selectSkill()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Skill skill = getSkillByRaycast();
            if (skill != null && skill.owner == playerTurn.battle.currentHero)
                playerTurn.playerTurnState = new PlayerTurnState_SkillSelected(playerTurn, skill);
        }
    }
}
