using System.Collections.Generic;
using UnityEngine;

namespace Systems.Pooling
{
    public class PoolObjectInfo
    {
        public string LookupString;
        public List<GameObject> InactiveObject = new List<GameObject>();
    }
}
