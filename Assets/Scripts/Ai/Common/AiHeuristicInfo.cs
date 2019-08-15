using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHeuristicInfo
{
    public LinkedList<GameUnitEntity> srcUnits;
    public LinkedList<GameUnitEntity> queuedUnits;
    public LinkedList<GameUnitEntity> enemyUnits;

    public CaptureEntity src; // The Ai's entity, friendly
    public CaptureEntity target; // The Ai's target, hostile

    public Direction direction;

    public AiHeuristicInfo(CaptureEntity src, Direction direction) {

       this.src = src;
       this.direction = direction;

       this.srcUnits = new LinkedList<GameUnitEntity>();
       this.queuedUnits = new LinkedList<GameUnitEntity>();
       this.enemyUnits = new LinkedList<GameUnitEntity>();

    }

}
