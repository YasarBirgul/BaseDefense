using System.Diagnostics.Eventing.Reader;
using DG.Tweening;
using UnityEngine;

namespace Controllers.Gate
{
    public class GateMeshController : MonoBehaviour
    {
        private float GateAngleZ = 0;
        public void TurnGateOpen(bool GateOpen)
        {
            GateAngleZ = GateOpen ? 0 : 90;
            transform.DORotate(new Vector3(0,0,GateAngleZ), 1.2f, 0).SetEase(Ease.OutBounce);
        }
    }
}