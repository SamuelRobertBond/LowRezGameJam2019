using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorTree : MonoBehaviour
{
    protected Node rootNode = new RootNode();

    public void step(GameUnitEntity entity, BoardManager board) {
        rootNode.step(entity, board);
    }


}
