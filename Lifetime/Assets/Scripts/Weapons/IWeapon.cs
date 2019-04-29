using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void Fire(Vector3 origin, Vector3 direction, float damageModifier, float knockbackModifier);
}
