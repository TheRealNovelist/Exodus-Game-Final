using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "New Composite Modifier", menuName = "Gameplay/Modifier/Composite")]
    public class ModComposite : Modifier
    {
        [SerializeField] private List<Modifier> _modifiers;
        
        public override WeaponData Modify(WeaponData data)
        {
            Debug.Log("Modified " + _modifiers.Count + " component(s)");
            foreach (var mod in _modifiers)
            {
                data = mod.Modify(data);
            }
            return data;
        }
    }
}