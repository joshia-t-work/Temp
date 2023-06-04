using DKP.ObserverSystem;
using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

namespace DKP.UnitSystem
{
    /// <summary>
    /// Represents a generic ground unit
    /// </summary>
    public class GenericGroundUnit : SkilledUnit
    {[SerializeField, InitializationField]
        BoxCollider2D attackDetection;

        [SerializeField, InitializationField]
        Transform model;
        public override Transform Model => model;

        [SerializeField, InitializationField]
        LayerMask enemyLayer;

        [SerializeField, ReadOnly]
        private List<Collider2D> colliderOnAttackRange = new List<Collider2D>();
        public ReadOnlyCollection<Collider2D> ColliderOnAttackRange;

        public override void Awake()
        {
            base.Awake();
            ColliderOnAttackRange = colliderOnAttackRange.AsReadOnly();
#if UNITY_EDITOR
            if (!attackDetection.gameObject.layer.Equals(LayerMask.NameToLayer("Detection")))
            {
                UnityEngine.Debug.LogWarning("The unit detection layer is incorrect!", gameObject);
            }
#endif
        }

#if UNITY_EDITOR
        public override void updateReferences()
        {
            base.updateReferences();
            if (attackDetection != null && UnitStats != null)
            {
                attackDetection.size = new Vector2(UnitStats.AttackRange.Value, attackDetection.size.y);
                attackDetection.offset = new Vector2(UnitStats.AttackRange.Value / 2f, attackDetection.offset.y);
            }
        }
#endif

        public override void Spawn()
        {
            base.Spawn();
            colliderOnAttackRange.Clear();
            if (attackDetection != null && UnitStats != null)
            {
                attackDetection.size = new Vector2(UnitStats.AttackRange.Value, attackDetection.size.y);
                attackDetection.offset = new Vector2(UnitStats.AttackRange.Value / 2f, attackDetection.offset.y);
            }
        }

        /// <summary>
        /// Call to move the unit once
        /// </summary>
        public void Move()
        {
            if (rb.bodyType == RigidbodyType2D.Static)
                return;
            float prefVal = rb.velocity.x;
            if (Mathf.Sign(model.transform.localScale.x) < 0)
            {
                prefVal = Mathf.Max(prefVal - UnitStats.MoveSpeed.Value * Time.deltaTime * 5f, -UnitStats.MoveSpeed.Value);
            } else
            {
                prefVal = Mathf.Min(prefVal + UnitStats.MoveSpeed.Value * Time.deltaTime * 5f, UnitStats.MoveSpeed.Value);
            }
            //rb.AddForce(Vector2.right * UnitStats.MoveSpeed.Value * 1.5f * Mathf.Sign(model.transform.localScale.x), ForceMode2D.Impulse);
            rb.velocity = new Vector2(prefVal, rb.velocity.y);
        }

        public void Stop()
        {
            //rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        /// <summary>
        /// Call to knock the unit backwards
        /// </summary>
        public void KnockBack()
        {
            rb.AddForce(Vector2.up + Vector2.left * 2f * Mathf.Sign(model.transform.localScale.x), ForceMode2D.Impulse);
        }

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.isTrigger)
                return;
            Rigidbody2D rigidbody = collision.attachedRigidbody;
            if (rigidbody != null)
            {
                if (rigidbody.TryGetComponent(out Unit unit))
                {
                    if (unit.UnitTeam != UnitTeam)
                    {
                        colliderOnAttackRange.Add(collision);
                    }
                }
            }
        }
        //public virtual void OnTriggerStay2D(Collider2D collision)
        //{
        //    if (collision.isTrigger)
        //        return;
        //    if (collision.attachedRigidbody == null)
        //        return;
        //    if ((enemyLayer.value & (1 << collision.attachedRigidbody.transform.gameObject.layer)) > 0)
        //    {
        //        if (!colliderOnAttackRange.Contains(collision))
        //        {
        //            colliderOnAttackRange.Add(collision);
        //        }
        //    }
        //}

        public virtual void OnTriggerExit2D(Collider2D collision)
        {
            if (colliderOnAttackRange.Contains(collision))
            {
                colliderOnAttackRange.Remove(collision);
            }
        }
    }
}