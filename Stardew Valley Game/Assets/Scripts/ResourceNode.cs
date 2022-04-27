using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class ResourceNode : ToolHit
{
    //[SerializeField] GameObject pickUpDrop;
    [SerializeField] float spread = 0.7f;

    [SerializeField] Item item;
    [SerializeField] int itemCountInOneDrop = 1;
    [SerializeField] int dropCount = 5;
    [SerializeField] ResourseNodeType nodeType;

    public override void Hit()
    {
        while (dropCount > 0)
        {
            dropCount -= 1;

            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;

            ItemSpawnManager.instance.SpawnItem(position, item, itemCountInOneDrop);

            //GameObject go = Instantiate(pickUpDrop);
            //go.GetComponent<PickUpItem>().Set(item, itemCountInOneDrop);
            //go.transform.position = position;
        }
        Destroy(gameObject);
    }

    public override bool CanBeHit(List<ResourseNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }
}
