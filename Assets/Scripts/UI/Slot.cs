﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Inventory.UI
{
    using Items;

    public class Slot : MonoBehaviour
    {
        public InventoryUI Parent
        {
            get => parent;
            internal set => parent = value;
        }

        private InventoryUI parent;

        [SerializeField]
        private Image itemSprite;
        [SerializeField]
        private TextMeshProUGUI nameLabel;
        [SerializeField]
        private TextMeshProUGUI amountLabael;

        public void SetItem(ItemStack item)
        {
            if(item == null || item.IsEmpty)
            {
                this.itemSprite.sprite = null;
                this.itemSprite.color = Color.clear;
                this.nameLabel.text = "";
                this.amountLabael.text = "";
            }
            else
            {
                this.itemSprite.sprite = item.Item.Sprite;
                this.itemSprite.color = Color.white;
                this.nameLabel.text = item.Item.DisplayName;
                this.amountLabael.text = item.Amount.ToString();
            }
        }
    }
}