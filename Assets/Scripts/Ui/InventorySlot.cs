using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : ISlot
{
    public Augment currAug;

    private void Start()
    {
        if (currAug != null)
        {
            ApplyAugment(currAug);
        }
    }

    public override void OnDrop(PointerEventData eventData)
    {
        currAug = eventData.pointerDrag.GetComponent<Augment>();
        if (currAug != null)
        {
            ApplyAugment(currAug);
        }
    }

    public override void ApplyAugment(Augment aug)
    {
        aug.startPos = transform.position;
        aug.transform.position = transform.position;
        aug.currentSlot = this;
    }
}
