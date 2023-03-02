using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace MachineLearningBasic.SurvivalOfPopulation.Data
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class Brain : MonoBehaviour
    {
        public int DNALength = 1;
        public float timeAlive;
        public DNA dna;

        private ThirdPersonCharacter m_Character;
        private Vector3 m_Move;
        bool alive = true;

        void OnCollisionEnter(Collision obj)
        {
            if (obj.gameObject.tag == "dead")
            {
                alive = false;
                gameObject.SetActive(alive);
            }
        }

        public void Init()
        {
            //initialise DNA
            //0 forward
            //1 back
            //2 left
            //3 right
            //4 crouch
            dna = new DNA(DNALength, Random.Range(0, 6));
            m_Character = GetComponent<ThirdPersonCharacter>();
            timeAlive = 0;
            alive = true;
            gameObject.SetActive(alive);
        }


        void Update()
        {

        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read DNA
            float h = 0;
            float v = 0;
            bool crouch = false;
            if (dna.GetGene(0) == 0) v = 1;
            else if (dna.GetGene(0) == 1) v = -1;
            else if (dna.GetGene(0) == 2) h = -1;
            else if (dna.GetGene(0) == 3) h = 1;
            else if (dna.GetGene(0) == 4) crouch = true;

            m_Move = v * Vector3.forward + h * Vector3.right;
            m_Character.Move(m_Move, crouch, false);
            if (alive)
                timeAlive += Time.deltaTime;
        }
    }
}