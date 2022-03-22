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
        [SerializeField]
        private Image itemSprite;
        [SerializeField]
        private TextMeshProUGUI nameLabel;
        [SerializeField]
        private TextMeshProUGUI amountLabael;

        public void SetItem(ItemStack item)
        {
            this.itemSprite.sprite = item.Item.Sprite;
            this.nameLabel.text = item.Item.DisplayName;
            this.amountLabael.text = item.Amount.ToString();
        }
    }
}