using DKP.ObserverSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a list of reference
    /// </summary>
    [Serializable]
    public class SerializableReferenceList<T>
        where T : new()
    {
        [SerializeField, ReadOnly]
        public List<SerializableReference<T>> List;

        public SerializableReferenceList()
        {
            List = new List<SerializableReference<T>>();
        }
        public SerializableReferenceList(List<SerializableReference<T>> val)
        {
            List = val;
        }
        public T this[int index]
        {
            get
            {
                return List[index].Value;
            }
            set
            {
                List[index].Value = value;
            }
        }
        public int Count => List.Count;
        public void Add(T value)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (List[i].Value.Equals(value))
                {
                    return;
                }
            }
            List.Add(new SerializableReference<T>(value));
        }
        public void Remove(T value)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (List[i].Value.Equals(value))
                {
                    List.RemoveAt(i);
                    return;
                }
            }
        }
        public int IndexOf(T value)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (List[i].Value.Equals(value))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}