using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState_TurnBeforeSkillSelected : BattleState
{
    public BattleState_TurnBeforeSkillSelected(Battle battle, BattleState state)
        : base(battle, state)
    { }

    public override void execute()
    {
        // Select hero
        if (Input.GetMouseButtonDown(0))
        {
            Hero hero = getHeroByRaycast();
            if (hero != null)
                selectHero(hero);
        }

        // Select skill
        if (Input.GetMouseButtonDown(0))
        {
            Skill skill = getSkillByRaycast();
            if (skill != null && skill.isActive)
            {
                selectSkill(skill);
                if (skill.skillName == SkillName.SkipTurn)
                    battle.battleState = new BattleState_UsingSkill(battle, this);
                else
                    battle.battleState = new BattleState_TurnAfterSkillSelected(battle, this);
            }
        }
    }
}