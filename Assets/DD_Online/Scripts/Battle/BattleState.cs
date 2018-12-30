using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleState
{
    public Battle battle;
    public BattleState(Battle battle)
    {
        this.battle = battle;
    }

    public virtual void execute() {}
}
