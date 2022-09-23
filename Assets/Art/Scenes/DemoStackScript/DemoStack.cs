using System;
using UnityEngine;

namespace Art.Scenes.DemoStackScript
{
    public class DemoStack : MonoBehaviour
    {

        private float numberOfDiamonds;
        private float x;
    //  private float y;
        private float z;
        [SerializeField] private GameObject _cube;


        private void Stack()
        {
            var divide = numberOfDiamonds/x;
            var mod = numberOfDiamonds%x ;

            for (int i = 0; i < x; i++)
            {
                if (divide != i) 
                    return;
                {
                    
                }
            }
        }
    }
}