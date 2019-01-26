using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState_TurnAfterSkillSelected : BattleState
{
    public BattleState_TurnAfterSkillSelected(Battle battle, BattleState state)
        : base(battle, state)
    { }

    public override void execute()
    {
        // Select hero
        if (Input.GetMouseButtonDown(0))
        {
            Hero hero = getHeroByRaycast();
            if (hero != null && hero.isTargeted)
            {
                targetHero = hero;
                battle.battleState = new BattleState_UsingSkill(battle, this);
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
            selectSkill(skill, !skill.IsSelected);
            if (skill.skillName == SkillName.SkipTurn)
                battle.battleState = new BattleState_UsingSkill(battle, this);
            else if (skill.IsSelected == false)
                battle.battleState = new BattleState_TurnBeforeSkillSelected(battle, this);
        }
    }
}