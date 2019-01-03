using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneElementsContainer : MonoBehaviour
{
    public Transform[] skillTransforms1;
    public Transform[] skillTransforms2;
    public static Transform[][] skillTransforms;

    public Transform[] heroTransforms1;
    public Transform[] heroTransforms2;
    public static Transform[][] heroTransforms;

    public BoxCollider2D[] heroColliders1;
    public BoxCollider2D[] heroColliders2;
    public static BoxCollider2D[][] heroColliders;

    public Transform[] turnStateIndicatorTransforms1;
    public Transform[] turnStateIndicatorTransforms2;
    public static Transform[][] turnStateIndicatorTransforms;

    public List<Hero> heroesTeam1;
    public List<Hero> heroesTeam2;
    [HideInInspector]
    public static List<Hero> heroes;


    private void Awake()
    {
        heroes = new List<Hero>();
        foreach (Hero hero in heroesTeam1)
            heroes.Add(hero);
        foreach (Hero hero in heroesTeam2)
            heroes.Add(hero);

        skillTransforms = new Transform[2][];
        skillTransforms[0] = skillTransforms1;
        skillTransforms[1] = skillTransforms2;

        heroTransforms = new Transform[2][];
        heroTransforms[0] = heroTransforms1;
        heroTransforms[1] = heroTransforms2;

        heroColliders = new BoxCollider2D[2][];
        heroColliders[0] = heroColliders1;
        heroColliders[1] = heroColliders2;

        turnStateIndicatorTransforms = new Transform[2][];
        turnStateIndicatorTransforms[0] = turnStateIndicatorTransforms1;
        turnStateIndicatorTransforms[1] = turnStateIndicatorTransforms2;
    }
}