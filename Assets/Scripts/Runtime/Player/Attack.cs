using UnityEngine;

namespace Runtime.Player
{
    public class Attack : MonoBehaviour
    {
        public ParticleSystem particle;

        public Collider collider;

        private void Start()
        {
            collider = GetComponent<Collider>();
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Rock"))
            {
                Destroy(other.gameObject);
                hit();
            }
        }

        private void hit()
        {
            particle.Play();
            collider.enabled = false;
        }
    }
}