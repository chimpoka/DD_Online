using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState_SkillNotSelected : PlayerTurnState
{ 
    public PlayerTurnState_SkillNotSelected(BattleState_PlayerTurn playerTurn)
        : base(playerTurn)
    { }

    public override void execute()
    {
        // Select hero
        if (Input.GetMouseButtonDown(0))
        {
            Hero hero = getHeroByRaycast();
            if (hero != null)
                playerTurn.selectHero(hero);
        }

        // Select skill
        if (Input.GetMouseButtonDown(0))
        {
            Skill skill = getSkillByRaycast();
            if (skill != null && skill.isActive)
            {
                playerTurn.selectSkill(skill);
                if (skill.skillName == SkillName.SkipTurn)
                    playerTurn.battle.battleState = new BattleState_UsingSkill(playerTurn.battle, playerTurn);
                else
                    playerTurn.playerTurnState = new PlayerTurnState_SkillSelected(playerTurn);
            }
        }
    }
}