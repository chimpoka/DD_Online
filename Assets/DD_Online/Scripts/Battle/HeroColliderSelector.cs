using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroColliderSelector : MonoBehaviour
{
    public BoxCollider2D[] colliders1;
    public BoxCollider2D[] colliders2;
    public static BoxCollider2D[][] colliders;

    void Start()
    {
        colliders = new BoxCollider2D[2][];
        colliders[0] = colliders1;
        colliders[1] = colliders2;
    }

    public static void getHero()
    {
        if (checkHitCollider() != null)
        {
            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {

                }
            }
        }
    }

    private static Collider2D checkHitCollider()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
                return hit.collider;
        }

        return null;
    }
}
