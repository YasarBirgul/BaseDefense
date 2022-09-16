using DG.Tweening;
using UnityEngine;

namespace Controllers.Gate
{
    public class GateMeshController : MonoBehaviour
    {
        private float _gateAngleZ;
        public void TurnGateOpen(bool GateOpen)
        {
            _gateAngleZ = GateOpen ? 0 : 90;
            transform.DORotate(new Vector3(0,0,_gateAngleZ), 1.2f, 0).SetEase(Ease.OutBounce);
        }
    }
}