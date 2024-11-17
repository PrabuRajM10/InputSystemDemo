using System;
using System.Collections;
using Enums;
using Helpers;
using UnityEngine;
using UnityEngine.Serialization;
using Enum = Enums.Enum;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class Bomb : MonoBehaviour, IPoolableObjects , IParticleEmitter
    {
        [SerializeField] private float timeBeforeExplosion;
        [SerializeField] private float explodeRadius = 2f;
        [SerializeField] private float damageAmount = 200;
        [SerializeField] private float thrust = 15f;
        [SerializeField] private float torqueForce = 10f;
        [SerializeField] private ParticleSystem particle;
        [SerializeField] Color gizmoColor = Color.green;

        private Rigidbody _rigidBody;
        private SphereCollider _sc;
        private ObjectPooling _pool;

        public void Init(ObjectPooling pool)
        {
            _pool = pool;
            _rigidBody = GetComponent<Rigidbody>();
            _sc = GetComponent<SphereCollider>();
        }

        public void BackToPool()
        {
            _pool.AddBackToList(this , Enum.PoolObjectTypes.Bomb);
        }

        public void Deploy(Vector3 position)
        {
            var transform1 = transform;
            transform1.position = position;
            
            var randomX = Random.Range(-1f, 1f);
            var randomY = Random.Range(-1f, 1f);
            var randomZ = Random.Range(-1f, 1f);

            var randomTorque = new Vector3(randomX, randomY, randomZ) * torqueForce;
            
            _rigidBody.AddForce(transform1.up * thrust);
            _rigidBody.AddTorque(randomTorque, ForceMode.VelocityChange);
            
            StartCoroutine(StartTimer());
        }

        IEnumerator StartTimer()
        {
            float currentTime = 0;
            while (currentTime < timeBeforeExplosion)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }
            
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, explodeRadius);
            CameraShake.ShakeCamera(1 , 0.5f);
            foreach (var hitCollider in hitColliders)
            {
                var enemy = hitCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damageAmount);
                }
            }
            
            PlayParticle(particle , transform.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Jumpable>() != null)
            {
                _sc.isTrigger = false;
            }
        }
        

        void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(transform.position, explodeRadius);
        }

        public void PlayParticle(ParticleSystem particleSystem , Vector3 position)
        {
            particleSystem.Play();
            Invoke(nameof(OnParticleSpawnDone) , particleSystem.main.duration);
        }

        public void OnParticleSpawnDone()
        {
            BackToPool();
        }
    }
}