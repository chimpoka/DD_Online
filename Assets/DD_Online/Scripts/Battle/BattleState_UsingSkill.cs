using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState_UsingSkill : BattleState
{
    public BattleState_UsingSkill(Battle battle)
      : base(battle)
    { }

    public override void execute()
    {
        battle.battleState = new BattleState_HeroSelection(battle);
    }
}
