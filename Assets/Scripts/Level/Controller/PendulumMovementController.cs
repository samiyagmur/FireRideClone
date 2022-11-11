using Data.ValueObject;
using Status;
using UnityEngine;

namespace Controller
{
    public class PendulumMovementController : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer lineRenderer;

        private Transform _roofTransform;

        private SpringJoint _springJoint;

        private PendulumMovementData _pendulumMovementData;

        internal void SetData(PendulumMovementData pendulumMovementData) => _pendulumMovementData = pendulumMovementData;

        public void ConnectToRoof()
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, _pendulumMovementData.HitAngle, out hit))
            {
                _roofTransform = hit.transform;

                float distancePoint = Vector3.Distance(transform.position, _roofTransform.position);

                SpringJointSetting(distancePoint);

                _springJoint.anchor = _pendulumMovementData.SelfAncorPoint;

                LineRendererAppearance(LineRenderAppearanceStatus.Show);

                _springJoint.connectedAnchor = SetTargetWallMidPoint();
            }
        }

        private Vector3 SetTargetWallMidPoint()
            => _roofTransform.position - new Vector3(0, _roofTransform.localScale.y / 2, 0);

        private void LineRendererAppearance(LineRenderAppearanceStatus lineRenderAppearanceStatus)
            => lineRenderer.positionCount = (int)lineRenderAppearanceStatus;

        private void SpringJointSetting(float distancePoint)
        {
            _springJoint.autoConfigureConnectedAnchor = false;

            _springJoint.maxDistance = distancePoint * _pendulumMovementData.RateOfMaxDistance;
            _springJoint.minDistance = distancePoint * _pendulumMovementData.RateOfMinDistancef;

            _springJoint.spring = _pendulumMovementData.SpringJointSpring;
            _springJoint.damper = _pendulumMovementData.SpringJointDamper;
            _springJoint.massScale = _pendulumMovementData.SpringJointMassScale;
        }

        internal void AddSpringJoint() => _springJoint = gameObject.AddComponent<SpringJoint>();

        public void DisConnectToRoof()
        {
            Destroy(_springJoint);
            LineRendererAppearance(LineRenderAppearanceStatus.Hide);
        }

        private void LateUpdate() => DrawRope();

        private void DrawRope()
        {
            if (!_springJoint) return;

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, SetTargetWallMidPoint());
        }
    }
}