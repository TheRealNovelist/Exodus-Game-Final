using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Old
{
    public class GunPickUp2 : MonoBehaviour
    {
        [SerializeField] private Transform shotgun;
        [SerializeField] private Inventory inventory;
        [SerializeField] private List<Rifle> allGuns;

        //This function will put the gun we pass in to the first position, SiblingIndex = 0
        public void PutToTheTop(Transform gun)
        {
            gun.transform.SetSiblingIndex(0);
        }

        //This function will put the gun we pass in to the second position, SiblingIndex = 1
        public void PutAtSecondPlace(Transform gun)
        {
            gun.transform.SetSiblingIndex(1);
        }


        public void GunUpdate()
        {
            GunItem gunItemSlot1 = inventory.gunPanel.equippedItems[0] as GunItem;
            GunItem gunItemSlot2 = inventory.gunPanel.equippedItems[1] as GunItem;

            if (gunItemSlot1 != null)
            {
            }

            if (gunItemSlot2 != null)
            {
            }
        }
    }
}
