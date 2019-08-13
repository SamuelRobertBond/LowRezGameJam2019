using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDecision
{

    public Direction direction;
    public CaptureEntity captureEntity;
    public int entityType;

    public AiDecision(CaptureEntity captureEntity, int entityType, Direction direction) {
        this.captureEntity = captureEntity;
        this.entityType = entityType;
        this.direction = direction;
    }

}
