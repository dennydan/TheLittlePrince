using System.Collections.Generic;
using UnityEngine;

namespace Gamekit3D.GameCommands
{
    public class Collector : MonoBehaviour
    {
        public bool attachCollectables = false;
        public bool debugLog;

        Dictionary<string, int> collections = new Dictionary<string, int>();
        public virtual void OnCollect(Collectable collectable)
        {
            if (attachCollectables)
                collectable.transform.parent = transform;
            var count = 0;
            if (collections.TryGetValue(collectable.name, out count))
                collections[collectable.name] = count + 1;
            else
                collections[collectable.name] = 1;

            if(debugLog)
                Debug.Log("You Get: " + collectable.name + ", nums: " + collections[collectable.name]);
            GameManager.OnCollectStar();
            for(int i = 0; i < collections[collectable.name]; i++)
            {
                GameObject.Find("StarAmount").transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        public bool HasCollectable(string name)
        {
            return collections.ContainsKey(name);
        }

        public bool HasCollectableQuantity(string name, int requiredCount)
        {
            int count;
            if (collections.TryGetValue(name, out count))
                return count >= requiredCount;
            return false;
        }

        public int GetCollectableQuantity(string name)
        {
            int count;
            if (collections.TryGetValue(name, out count))
                return count;
            return -1;
        }
    }


}