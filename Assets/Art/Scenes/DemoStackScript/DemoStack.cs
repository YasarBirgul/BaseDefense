using UnityEngine;

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
            var vector2a = new Vector3(10, 2, 1);
            Debug.Log("Mag  "+ vector2a.magnitude);
            Debug.Log("sqrMag  "+vector2a.sqrMagnitude);
            Debug.Log("  double sqr" +vector2a.sqrMagnitude*vector2a.sqrMagnitude);
            if (GridIsFull) return; 

            var modx = OrderOfTheItem % xGridSize ;
            
            var dividey =  OrderOfTheItem / xGridSize;
           
            var mody = dividey % yGridSize; 
           
            var divideXY = OrderOfTheItem / (xGridSize * yGridSize);

            var RotationVector = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);

            var vector3A = new Vector3(modx * offSet , divideXY * offSet, mody * offSet);

            Instantiate(_cube, transform.localPosition + vector3A, Quaternion.identity,transform);

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