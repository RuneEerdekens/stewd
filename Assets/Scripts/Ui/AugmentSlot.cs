using UnityEngine;
using UnityEngine.EventSystems;

public class AugmentSlot : ISlot
{
    private PlayerAugmentController augmentController; // Reference to the controller managing augments
    private Augment currentAugment; // The augment currently assigned to this slot

    private void Awake()
    {
        augmentController = FindObjectOfType<PlayerAugmentController>();
    }

    public override void OnDrop(PointerEventData eventData)
    {
        Augment draggable = eventData.pointerDrag.GetComponent<Augment>();
        if (draggable != null && draggable.assignedAugment != null && CompareTag(draggable.tag))
        {
            // Handle the drop
            ApplyAugment(draggable);
        }
    }

    public void OnAugmentDraggedOut(Augment augment)
    {
        // This method is called when an augment is dragged out of this slot
        if (currentAugment == augment)
        {
            RemoveAugment();
        }
    }

    public override void ApplyAugment(Augment augment)
    {
        if (currentAugment != null)
        {
            RemoveAugment(); // Remove the current augment if any
        }

        // Assign the new augment
        currentAugment = augment;
        augment.currentSlot = this; // Update the augment's reference to this slot
        augment.startPos = transform.position; // Update the starting position of the augment
        augmentController.AssignAugmentToSlot(augment.assignedAugment);
    }

    private void RemoveAugment()
    {
        if (currentAugment != null)
        {
            augmentController.RemoveAugmentFromSlot(currentAugment.assignedAugment);
            currentAugment.transform.position = currentAugment.startPos; // Return to original position
            currentAugment = null; // Clear the reference
        }
    }
}
