using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class BombPhysicController : MonoBehaviour, IDamager
{
    private const int DAMAGE = 50;
    public int Damage()
    {
        return DAMAGE;
    }
}
