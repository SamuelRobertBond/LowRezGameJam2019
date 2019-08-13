using System.Collections;
using System;
using UnityEngine;

public abstract class AiDecisionHeuristic : MonoBehaviour
{

    public const int VIEW_RANGE = 6;

    private AiController aiController;
    protected virtual void Start()
    {
        this.aiController = this.GetComponent<AiController>();
    }

    public abstract AiDecision[] makeDecisions();


    public AiHeuristicInfo[] getAllCaptureEntitiesWithinRange(AiController controller, RaycastHit2D hit) {

        foreach (CaptureEntity c in this.aiController.capturedEntities) {

            // Check Each Direction
            foreach (Direction d in Enum.GetValues(typeof(Direction))) {

                Vector2 direction = EntityUtils.getDirectionVector(d);
                direction *= BoardManager.TILE_SCALE * VIEW_RANGE;

                Vector2 end = c.rb2D.position + direction;
                

            }
            
        }

        return null;

    }

}
