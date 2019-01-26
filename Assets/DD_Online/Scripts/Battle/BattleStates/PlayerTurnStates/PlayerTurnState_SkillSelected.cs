using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState_SkillSelected : PlayerTurnState
{
    public PlayerTurnState_SkillSelected(BattleState_PlayerTurn playerTurn)
        : base(playerTurn)
    {}

    public override void execute()
    {
        // Select hero
        if (Input.GetMouseButtonDown(0))
        {
            Hero hero = getHeroByRaycast();
            if (hero != null && hero.isTargeted)
            {
                playerTurn.targetHero = hero;
                playerTurn.battle.battleState = new BattleState_UsingSkill(playerTurn.battle, playerTurn);
            }
        }

        // Select skill
        if (Input.GetMouseButtonDown(0))
        {
            Skill skill = getSkillByRaycast();
            if (skill != null)
                onSelectSkill(skill);
        }
    }



    private void onSelectSkill(Skill skill)
    {
        if (skill.isActive)
        {
            playerTurn.selectSkill(skill, !skill.IsSelected);
            if (skill.skillName == SkillName.SkipTurn)
                playerTurn.battle.battleState = new BattleState_UsingSkill(playerTurn.battle, playerTurn);
            else if (skill.IsSelected == false)
                playerTurn.playerTurnState = new PlayerTurnState_SkillNotSelected(playerTurn);
        }
    }
}