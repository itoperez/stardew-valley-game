using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    public ItemContainer inventory;
    public List<InventoryButton> buttons;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        SetIndex();
        Show();
    }

    private void OnEnable()
    {
        Show();
    }

    private void SetIndex()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetIndex(i);
        }
    }

    public virtual void Show()
    {
        for (int i = 0; i < inventory.slots.Count && i < buttons.Count; i++)
        {
            if (inventory.slots[i].item == null)
            {
                buttons[i].Clean();
            }      
            else if (inventory.slots[i].count <= 0 && inventory.slots[i].item.stackable)
            {
                buttons[i].Clean();
                inventory.slots[i] = null;
            }            
            else
            {
                buttons[i].Set(inventory.slots[i]);
            }
        }
    }

    public virtual void OnClick(int id)
    {

    }
}
