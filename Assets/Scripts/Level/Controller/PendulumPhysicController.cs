using Managers;
using Type;
using UnityEngine;

namespace Controller
{
    public class PendulumPhysicController : MonoBehaviour
    {
        [SerializeField]
        private PendulumManager pendulumManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ParticalPysicController particalPysicController))
            {
                pendulumManager.WhenHitPartical((int)particalPysicController.GetParticalType());

                if (particalPysicController.GetParticalType() == ParticalTypeAsScore.BeamOrenge)
                {
                    pendulumManager.ChangeLevelColor();
                }
            }

            if (other.CompareTag("Wall"))
            {
                pendulumManager.WhenHitWall();
            }
        }
    }
}