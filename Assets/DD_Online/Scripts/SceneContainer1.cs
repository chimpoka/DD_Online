using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneContainer1 : MonoBehaviour
{
    public GameObject heroesContainerLeft;
    public GameObject heroesContainerRight;
    public static GameObject[] heroesContainers;

    //public RectTransform heroesContainerLeft;
    //public RectTransform heroesContainerRight;
    //public static RectTransform[] heroesContainers;

    void Awake()
    {
        heroesContainers = new GameObject[2];
        heroesContainers[0] = heroesContainerLeft;
        heroesContainers[1] = heroesContainerRight;
    }

}
