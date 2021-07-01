using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    /* --- COMPONENTS --- */
    public SlotUI firstSlot;

    /* --- VARIABLES --- */
    public int size = 0;

    /* --- METHODS --- */
    public void Add(Collectible collectible) {

        // itterate once to check if the item is already in bagpack
        SlotUI slot = firstSlot;
        int i = 0;
        while (slot != null && i < size) {
            if (slot.SameAs(collectible)) {
                slot.Add(collectible);
                return;
            }
            slot = slot.nextSlot;
        }

        // itterate second time, if item wasn't in bagpack, to look for an empty slot
        slot = firstSlot;
        i = 0;
        while (slot != null && i < size) {
            if (slot.IsEmpty()) {
                slot.Add(collectible);
                return;
            }
            slot = slot.nextSlot;
            i += 1;
        }
    }

}
