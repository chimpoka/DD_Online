using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerTurnState
{
    public BattleState_PlayerTurn playerTurn;
    public PlayerTurnState(BattleState_PlayerTurn playerTurn)
    {
        this.playerTurn = playerTurn;
    }

    public virtual void selectSkill() {}
    public virtual void selectHero() {}
}
