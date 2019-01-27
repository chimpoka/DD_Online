using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Battle : GameState
{
    public GameState_Battle(Game game)
    : base(game)
    {
        battleState = new BattleState_StartBattle(this);
    }

    public BattleState battleState;

    public override void update()
    {
        battleState.execute();
        battleState.showSkillHover();
    }
}