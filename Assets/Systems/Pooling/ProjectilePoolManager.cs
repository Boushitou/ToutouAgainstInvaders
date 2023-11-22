using System.Collections.Generic;
using UnityEngine;

namespace Systems.Pooling
{
    public class ProjectilePoolManager : MonoBehaviour
    {
        public static List<PoolObjectInfo> ObjectPools = new List<PoolObjectInfo>();

        public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation)
        {
            //If the pool exist
            PoolObjectInfo pool = null;

            foreach (PoolObjectInfo p in ObjectPools)
            {
                if (p.LookupString == objectToSpawn.name)
                {
                    pool = p;
                    break;
                }
            }

            //Else if it doesn't

            if (pool == null)
            {
                pool = new PoolObjectInfo() { LookupString = objectToSpawn.name };
                ObjectPools.Add(pool);
            }

            //Check if there are inactive objects in the pool

            GameObject spawnableObj = null;

            foreach(GameObject obj in pool.InactiveObject)
            {
                if (obj != null)
                {
                    spawnableObj = obj;
                    break;
                }
            }

            if (spawnableObj == null)
            {
                //There is no inactivate object, we create a new one
                spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
            }
            else
            {
                //There is an inactivate object, we activate it
                spawnableObj.transform.position = spawnPosition;
                spawnableObj.transform.rotation = spawnRotation;
                pool.InactiveObject.Remove(spawnableObj);
                spawnableObj.SetActive(true);
            }

            return spawnableObj;
        }

        public static void ReturnObjectPool(GameObject obj)
        {
            string objName = obj.name.Substring(0, obj.name.Length - 7); //7 is the number of character for "(Clone)"

            PoolObjectInfo pool = null;

            foreach (PoolObjectInfo p in ObjectPools)
            {
                if (p.LookupString == objName)
                {
                    pool = p;
                    break;
                }
            }

            if (pool == null)
            {
                Debug.LogWarning("Trying to release an object that is not pooled:" + objName);
            }
            else
            {
                obj.SetActive(false);
                pool.InactiveObject.Add(obj);
            }
        }
    }
}
