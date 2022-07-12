using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameEngine
{
    public class Ball : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D myrigidbody;

        [SerializeField]
        private Collider2D mycollider;

        [SerializeField]
        private SpriteRenderer myrenderer;

        [SerializeField]
        private AudioSource ballHitaudioSource;

        public bool IsActive { get; private set; }

        private void OnEnable()
        {
            Color c = myrenderer.color;
            c.a = 0f;
            myrenderer.color = c;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsActive)
            {
                Basket basket = collision.GetComponent<Basket>();
                if (basket != null)
                {
                    basket.Score();
                    Deactivate();
                    Disappear();
                }
            }
        }

        private void Disappear()
        {
            StartCoroutine(Disappearing());
        }

        private IEnumerator Disappearing()
        {
            for(int i = 0; i < 50;i++)
            {
                Color c = myrenderer.color;
                c.a -= 0.02f;
                myrenderer.color = c;
                yield return new WaitForFixedUpdate();
            }
        }

        public void Appear()
        {
            myrenderer.color = new Color(myrenderer.color.r, myrenderer.color.g, myrenderer.color.b, 1f);
        }

        public void Deactivate()
        {
            if (IsActive)
            {
                IsActive = false;
                myrigidbody.bodyType = RigidbodyType2D.Static;
                mycollider.isTrigger = true;
            }
        }

        public void Activate()
        {
            if(!IsActive)
            {
                IsActive = true;
                mycollider.isTrigger = false;
                myrigidbody.bodyType = RigidbodyType2D.Dynamic;  
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            ballHitaudioSource.Play();
        }
    }
}
