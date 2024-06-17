using UnityEngine;

namespace PVZ
{
    public interface IDamage
    {
        void CauseDamage(Object target);

        void ReceiveDamage(int damage)
        {
            throw new System.NotSupportedException();
        }
    }
}