using Enums;
using UnityEngine;

namespace Interfaces
{
    public interface IGetPoolObject
    {
        GameObject GetObject(PoolType poolName);
    }
}
