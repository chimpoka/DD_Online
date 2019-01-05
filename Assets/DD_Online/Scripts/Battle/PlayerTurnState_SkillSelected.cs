using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState_SkillSelected : PlayerTurnState
{
    public PlayerTurnState_SkillSelected(BattleState_PlayerTurn playerTurn)
        : base(playerTurn)
    {
        onSelectSkill(playerTurn.selectedSkill);
    }

    public override void selectHero()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Hero hero = getHeroByRaycast();
            if (hero != null && hero.isTargeted)
                playerTurn.battle.battleState = new BattleState_UsingSkill(playerTurn.battle, playerTurn);
        }
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
        if (skill != null)
        {
            if (skill.skillName == SkillContainer.SkillName.SkipTurn)
                playerTurn.battle.battleState = new BattleState_UsingSkill(playerTurn.battle, playerTurn);

            else if (skill.isEnabled)
            {
                if (skill.isSelected == false)
                {
                    playerTurn.selectSkill(skill, true);
                    playerTurn.selectedSkill = skill;
                }
                else
                {
                    playerTurn.selectSkill(skill, false);
                    playerTurn.selectedSkill = null;
                    playerTurn.playerTurnState = new PlayerTurnState_SkillNotSelected(playerTurn);
                }
            }
        }
    }
}