using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Augment : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Image thisImage;
    [HideInInspector] public Vector3 startPos;

    public PrimaryAttackAugmentBase assignedAugment;

    private void Awake()
    {
        thisImage = GetComponent<Image>();
        startPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        thisImage.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPos;
        thisImage.raycastTarget = true;
    }
}
