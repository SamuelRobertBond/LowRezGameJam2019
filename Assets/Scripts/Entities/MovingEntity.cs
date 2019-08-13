using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingEntity : GameEntity
{

    public Vector2 dir;

    public float moveTime = 1f;
    private float inverseMoveTime;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        inverseMoveTime = 1f / moveTime;
    }

    protected IEnumerator SmoothMovement(Vector3 end) {

        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon) {

            boxCollider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(this.transform.position, end, blockingLayer);
            boxCollider.enabled = true;

            if (hit.collider != null) {
                GameUnitEntity entity = hit.collider.GetComponent<GameUnitEntity>();
                if (entity != null && ( entity.owner != this.owner || entity.isInCombat)) {
                    break;
                }
            }

            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }

    }

    protected void Move(Vector2 dir) {

        Vector2 start = rb2D.position;
        Vector2 end = start + dir;

        boxCollider.enabled = false;
        RaycastHit2D hit = Physics2D.Linecast(start, end, blockingLayer);
        Debug.DrawLine(start, end, Color.red, .5f, false);
        boxCollider.enabled = true;

        if (hit.transform != null) {

            GameEntity hitEntity = hit.transform.GetComponent<GameEntity>();

            if (hitEntity == null) {
                Debug.Log("This should never happen, debug and check components on objects");
                return;
            }

            // Unit
            GameUnitEntity targetUnitEntity = hitEntity as GameUnitEntity;
            GameUnitEntity srcUnitEntity = this as GameUnitEntity;
            if (targetUnitEntity != null && srcUnitEntity != null) {

                // Units are of the same type
                if (this.owner == targetUnitEntity.owner) {
                    return; // No case exists where these units will share space
                }

                // Apply Damage
                targetUnitEntity.health -= srcUnitEntity.damage;
                targetUnitEntity.isInCombat = true;
                srcUnitEntity.isInCombat = true;

                // TODO: Trigger Attack Animation / Sound

                // If the unit is dead, move into that units space
                if (targetUnitEntity.isDead() && !srcUnitEntity.isDead()) {
                    targetUnitEntity.isInCombat = false;
                    srcUnitEntity.isInCombat = false;
                    StartCoroutine(SmoothMovement(end));
                }

                return;
            }


        }

        StartCoroutine(SmoothMovement(end));
    }

    protected override void Act(){
        Move(this.dir * BoardManager.TILE_SCALE);
    }

}
