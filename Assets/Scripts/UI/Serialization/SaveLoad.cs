using Core.Serialization;
using Inventory.Definition;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    [SerializeField]
    private InventoryHolder targetHolder;

    private JToken serialized;

    private void Start()
    {
        if (targetHolder.Inventory is not ISerializable)
        {
            throw new ApplicationException("Not serializable Invantory");
        }
    }

    [ContextMenu("SaveLoad/Print")]
    public void Print()
    {
        Debug.Log(serialized?.ToString(Formatting.Indented));
    }

    [ContextMenu("SaveLoad/Save")]
    public void Save()
    {
        serialized = ((ISerializable)targetHolder.Inventory).Serialize();
    }

    [ContextMenu("SaveLoad/Load")]
    public void Load()
    {
        ((ISerializable)targetHolder.Inventory).Deserialize(serialized);
    }
}
