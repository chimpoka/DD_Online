using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState_PlayerTurn : BattleState
{
    public PlayerTurnState playerTurnState;

    public BattleState_PlayerTurn(Battle battle, BattleState state)
       : base(battle, state)
    {
        playerTurnState = new PlayerTurnState_SkillNotSelected(this);
    }

    public override void execute()
    {
        playerTurnState.execute();

        if (Input.GetKeyDown(KeyCode.Space))
            battle.battleState = new BattleState_UsingSkill(battle, this);
    }
}
