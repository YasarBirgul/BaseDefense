using Abstract;
using UnityEngine;

namespace Concrete
{
    public class StackableBaseGem : StackableBase
    {
        public override GameObject SendToStack()
        {
            return transform.gameObject;
        }
    }
}