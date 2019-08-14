using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgressiveAiController : AiDecisionManager
{
    private UnitManager unitManager;

    private void Start()
    {
        unitManager = FindObjectOfType<UnitManager>();
    }

    protected override int getScore(AiHeuristicInfo info)
    {
        int score = 100;

        int unitScore = getUnitScore(info.srcUnits);
        int enemyScore = getUnitScore(info.enemyUnits);

        // If flag is owned already half score
        if (info.target.owner == info.src.owner) {
            score /= 2; // Half Score if flag is already owned
        }

        score += enemyScore - unitScore;
        score += this.aiController.money - enemyScore;

        return score;
    }

    protected override AiDecision makeDecision(AiHeuristicInfo info)
    {

        int unitScore = getUnitScore(info.srcUnits);
        int enemyScore = getUnitScore(info.enemyUnits);

        int unitLevel = 3;

        if (enemyScore > unitScore)
        {

            if (info.target != null && info.target.owner.getUpgradeLevel() > info.src.owner.getUpgradeLevel()){
                unitLevel = 3;
            }

            unitLevel = 2;
        }else {
            unitLevel = 1;
        }

        return new AiDecision(info.target, unitLevel, info.direction);
    }

    private int getUnitScore(LinkedList<GameUnitEntity> units) {

        int score = 0;

        foreach (GameUnitEntity unit in units)
        {
            score += unit.cost;
        }

        return score;

    }

}
