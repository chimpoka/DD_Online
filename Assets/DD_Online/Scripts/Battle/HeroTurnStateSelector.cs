using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTurnStateSelector : MonoBehaviour
{
    public SpriteRenderer[] spriteRenderers1;
    public SpriteRenderer[] spriteRenderers2;
    private static SpriteRenderer[][] spriteRenderers;

    private void Start()
    {
        spriteRenderers = new SpriteRenderer[2][];
        spriteRenderers[0] = spriteRenderers1;
        spriteRenderers[1] = spriteRenderers2;
    }

    public static void setSelector(Hero hero)
    {
         if (hero.turnState == TurnState.Undone)
             spriteRenderers[(int)hero.team][hero.position - 1].color = Color.black;
         else if (hero.turnState == TurnState.Done)
             spriteRenderers[(int)hero.team][hero.position - 1].color = Color.red;
         else if (hero.turnState == TurnState.InProcess)
             spriteRenderers[(int)hero.team][hero.position - 1].color = Color.green;
    }
}
