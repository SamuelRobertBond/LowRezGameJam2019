using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : ControllerManager
{

    public AiDecisionManager decisionManager;

    public void step() {

        AiDecision[] decisions = this.decisionManager.getDecisions();

        foreach (AiDecision decision in decisions) {

            selectedCaptureEntity = decision.captureEntity;

            // Change direction
            decision.captureEntity.direction = decision.direction;
            decision.captureEntity.changeDirection();

            // buy unit
            buyUnit(decision.entityType);

        }

    }

}
