using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface IDamagable
{
    void ApplyDamage(float damage);
    void Death();
}
