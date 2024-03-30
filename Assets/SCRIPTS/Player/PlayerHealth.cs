using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHp;
    private int hp;
    private void Awake()
    {
        hp = maxHp;
    }
   
}
