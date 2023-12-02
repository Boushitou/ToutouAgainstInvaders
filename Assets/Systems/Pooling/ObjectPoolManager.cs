using System.Collections.Generic;
using UnityEngine;

namespace Systems.Pooling
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static List<PoolObjectInfo> ObjectPools = new List<PoolObjectInfo>();

        private GameObject _objectPoolEmptyHolder;

        private static Dictionary<PoolType, GameObject> _poolTypeEmpties = new Dictionary<PoolType, GameObject>();

        public enum PoolType
        {
            PlayerProjectile,
            EnemyProjectile,
            Enemies,
            Particules,
            Boids,
            None
        }

        public static PoolType PoolingType;

        private void Awake()
        {
            SetupEmpties();
        }

        public void SetupEmpties()
        {
            _objectPoolEmptyHolder = new GameObject("Pooled Objects");

            _poolTypeEmpties[PoolType.PlayerProjectile] = CreateEmptyObject("Player Projectiles");
            _poolTypeEmpties[PoolType.EnemyProjectile] = CreateEmptyObject("Enemy Projectiles");
            _poolTypeEmpties[PoolType.Enemies] = CreateEmptyObject("Enemies");
            _poolTypeEmpties[PoolType.Particules] = CreateEmptyObject("Particles");
            _poolTypeEmpties[PoolType.Boids] = CreateEmptyObject("Boids");
        }

        private GameObject CreateEmptyObject(string objName)
        {
            GameObject emptyObj = new GameObject(objName);
            emptyObj.transform.SetParent(_objectPoolEmptyHolder.transform);
            return emptyObj;
        }

        public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
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
                //Set parent of the object
                GameObject parentObject = SetParentObject(poolType);

                //There is no inactivate object, we create a new one
                spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);

                if (parentObject != null)
                {
                    spawnableObj.transform.SetParent(parentObject.transform);
                }
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

        public static GameObject SetParentObject(PoolType poolType)
        {
            return _poolTypeEmpties.TryGetValue(poolType, out GameObject obj) ? obj : null;
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
                Debug.LogWarning("Trying to release an object that is not pooled: " + objName);
                Destroy(obj);
            }
            else
            {
                obj.SetActive(false);
                pool.InactiveObject.Add(obj);
            }
        }
    }
}
