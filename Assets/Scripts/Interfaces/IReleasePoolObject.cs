using Enums;
using UnityEngine;

namespace Interfaces
{
    public interface IReleasePoolObject
    {
        void ReleaseObject(GameObject obj, PoolType poolName);
    } 
}
