using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileReference : MonoBehaviour, IPointerClickHandler {
    [ReadOnly]
    public string typeID;

    private Vector3 positionToMove;
    private float speed;
    private bool clicked;

    public Cell GridCell { get; set; }
    public bool IsMoving { get; set; }

    public void StartMovement(Vector3 position, float tileSpeed) {
        positionToMove = position;
        speed = tileSpeed;
        IsMoving = true;
    }

    public void LateTick() {
        if(!IsMoving) {
            return;
        }
        if(Vector3.Distance(transform.position, positionToMove) > 0.1f) {
            transform.position = Vector3.MoveTowards(transform.position, positionToMove, speed * Time.deltaTime);
            return;
        }
        transform.position = positionToMove;
        IsMoving = false;
    }
    public void Dispose() { }

    public bool Equals(TileReference other) {
        return typeID == other.typeID;
    }

    public void OnPointerClick(PointerEventData eventData) {
        GameplayData.tileClickEvent.Invoke(GridCell);
    }

#if UNITY_EDITOR
    private void OnValidate() {
        typeID ??= Guid.NewGuid().ToString();
    }
#endif
}