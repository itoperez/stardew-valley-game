using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 12;
    int selectedTool;

    public Action<int> onChange;

    public Item GetItem
    {
        get { return GameManager.instance.inventoryContainer.slots[selectedTool].item; }
    }

    private void Update()
    {
        float delta = Input.mouseScrollDelta.y;
        if (delta != 0)
        {
            if (delta > 0)
            {
                selectedTool--;
                selectedTool = (selectedTool < 0 ? toolbarSize - 1 : selectedTool);                
            }
            else
            {
                selectedTool++;
                selectedTool = (selectedTool >= toolbarSize ? 0 : selectedTool);
            }
            onChange?.Invoke(selectedTool);
        }
    }

    internal void Set(int id)
    {
        selectedTool = id;
    }
}
