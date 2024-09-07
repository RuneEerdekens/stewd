using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Augment : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    private Image thisImage;
    [HideInInspector] public Vector3 startPos; // Starting position of the augment

    public AugmentBase assignedAugment; // Reference to the augment data
    public ISlot currentSlot; // Reference to the current slot

    private void Awake()
    {
        thisImage = GetComponent<Image>();
        startPos = transform.position; // Initialize starting position
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // Update augment position while dragging
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        thisImage.raycastTarget = false; // Prevent UI blocks during drag

        GetComponent<RectTransform>().SetAsLastSibling();

        if (currentSlot != null)
        {
            if (currentSlot is AugmentSlot augSlot)
            {
                augSlot.OnAugmentDraggedOut(this);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPos;
        thisImage.raycastTarget = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Augment droppedAugment = eventData.pointerDrag.GetComponent<Augment>();
        if (droppedAugment != null && droppedAugment != this)
        {
            SwapWith(droppedAugment);
        }
    }

    private void SwapWith(Augment otherAugment)
    {
        // Swap positions
        Vector3 tempPos = startPos;
        startPos = otherAugment.startPos;
        otherAugment.startPos = tempPos;

        // Update positions in the world
        transform.position = startPos;
        otherAugment.transform.position = otherAugment.startPos;

        // Swap slots if necessary
        ISlot tempSlot = currentSlot;
        currentSlot = otherAugment.currentSlot;
        otherAugment.currentSlot = tempSlot;

        // Notify the controller

        currentSlot.ApplyAugment(this);
        otherAugment.currentSlot.ApplyAugment(otherAugment);
    }
}
