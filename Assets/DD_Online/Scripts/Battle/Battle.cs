using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public BattleState battleState;

    private void Awake()
    {
        SkillContainer.Instance.init();
    }

    void Start()
    { 
        battleState = new BattleState_StartBattle(this);
    }

    void Update()
    {
        battleState.execute();
        battleState.showSkillHover();
    }
}