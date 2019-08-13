using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnitEntity : MovingEntity
{

    // Combat
    public int health = 1;
    public int damage = 1;

    // Economy
    public int income = 1;
    public int cost = 1;

    public bool isInCombat = false;

    public bool isDead() {
        return this.health <= 0;
    }

    public void step(BoardManager board) {
        base.Act(); // Attempt To Act
    }

}
