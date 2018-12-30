using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState_PlayerTurn : BattleState
{
    public PlayerTurnState playerTurnState;

    public BattleState_PlayerTurn(Battle battle)
       : base(battle)
    {
        playerTurnState = new PlayerTurnState_SkillNotSelected(this);
    }

    public override void execute()
    {
        playerTurnState.selectHero();
        playerTurnState.selectSkill();

        if (Input.GetKeyDown(KeyCode.Space))
            battle.battleState = new BattleState_UsingSkill(battle);
    }
}
