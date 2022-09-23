using System;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Art.Scenes.DemoStackScript
{
    public class DemoStack : MonoBehaviour
    {
        private int OrderOfTheItem=0;
        [SerializeField] private int xGridSize;
        [SerializeField] private int yGridSize; 
        [SerializeField] private int maxNumberOfStack;
        [SerializeField] private GameObject _cube;
        public float offSet;

        private bool GridIsFull = false;
        public void OnClick()
        {
            Stack2();
        }
        private void Stack2()
        {
            if (GridIsFull) return; 

            var modx = OrderOfTheItem % xGridSize ;
            
            var dividey =  OrderOfTheItem / xGridSize;
           
            var mody = dividey % yGridSize; 
           
            var divideXY = OrderOfTheItem / (xGridSize * yGridSize);
           
            var vector3A = new Vector3(modx*offSet,divideXY*offSet,mody*offSet);

            Instantiate(_cube,  vector3A, quaternion.identity, transform);
               
            if (OrderOfTheItem == maxNumberOfStack - 1) 
            {
               GridIsFull = true;
            }
            else
            {
               OrderOfTheItem += 1;
            }
        }
    }
}