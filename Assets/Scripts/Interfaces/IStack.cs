using UnityEngine;

namespace Abstract
{
    public interface IStack
    { 
        void SetStackHolder(GameObject gameObject);
        void SetGrid();
        void SendGridDataToStacker();
    }
}