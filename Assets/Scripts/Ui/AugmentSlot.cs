using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AugmentSlot : MonoBehaviour, IDropHandler
{
    public string augmentCat;
    private PlayerAugmentController augmentController;
    private PrimaryAttackAugmentBase currentAugment;

    private void Awake()
    {
        augmentController = FindObjectOfType<PlayerAugmentController>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Augment draggable = eventData.pointerDrag.GetComponent<Augment>();
        if(draggable != null && draggable.assignedAugment != null && augmentCat == draggable.tag)
        {
            draggable.startPos = transform.position;
            currentAugment = draggable.assignedAugment;
            augmentController.AssignAugmentToSlot(currentAugment);
        }
    }
}
