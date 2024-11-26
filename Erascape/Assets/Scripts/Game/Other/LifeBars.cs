using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Other
{
    public class LifeBars : MonoBehaviour
    {
        public Image circle;
        public Image rectangle;

        public Image redcircle;
        public Image redrectangle;
        public Image cheatIcon;
        float lifeMax = 150;
        public float life;

        float realTimeLife = 50;

        public float velocitaBarraRossa = 0.1f;
    
        public GameObject defeatMenu;
        private bool cheats = false;

        void Awake()
        {
            cheatIcon.gameObject.SetActive(false);
        }

        void LifeBarsCalculation(Image circle, Image rectangle, float valoreVita)
        {
            float lifePercent = valoreVita / lifeMax;
            rectangle.fillAmount = Mathf.InverseLerp(0.5f, 1, lifePercent);

            float tmp = Mathf.InverseLerp(0, 0.5f, lifePercent);
            circle.fillAmount = Mathf.Lerp(0, 0.75f, tmp);
        }

        void Update()
        {
            if (life != realTimeLife)
            {
                LifeBarsCalculation(circle, rectangle, life);
                StartCoroutine(TimerRedLife());
            }
        }

        IEnumerator TimerRedLife()
        {
            yield return new WaitForSeconds(0.5f);

            while (life != realTimeLife)
            {
                realTimeLife = Mathf.MoveTowards(realTimeLife, life, velocitaBarraRossa);
                LifeBarsCalculation(redcircle, redrectangle, realTimeLife);
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void TakeDamage(float damage)
        {
            //Debug.Log("Il player ha subito " + damage + " danni");
            //Debug.Log("cheats active" + cheats);
            if (!cheats)
            {
                life -= damage;
            }

            if (life <= 0)
            {
                life = 0;
                defeatMenu.GetComponent<DefeatMenu>().Show();
            
            }
        }

        public float GetLifeMax()
        {
            return lifeMax;
        }
        public void IncreaseLife(float amount)
        {
            life += amount;
            if (life > lifeMax)
            {
                life = lifeMax;
            }
        }
        
        public void SetHealth(float amount)
        {
            life = amount;
            if (life > lifeMax)
            {
                life = lifeMax;
            }
        }

        public void CheatsOn(bool unlimited)
        {
            cheats = unlimited;
        }
    }
}
