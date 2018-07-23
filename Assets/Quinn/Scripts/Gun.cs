using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public int InMag = 5;
    public int MagSize = 5;
    //ready to fire at 0
    public float TBS = 1;
    public float ReloadTime = 3;
    public bool ReloadAfterLastShot = true;
    //public WeaponType TypeOfWeapon;
    //public ReticleController.ReticleType TypeOfReticle;
    public GameObject Modle;
    public Animation FireAnimation;
    public delegate void AmmoChange(/*WeaponType type,*/ int InMag, int MagSize);
    public AmmoChange OnAmmoChange;

    [Header("READ ONLY")]
    public float TBSTimer;
    public float ReloadTimer;
    public bool Reloading = false;

    public virtual void Fire()
    {
        TBSTimer = TBS;
        InMag--;
        FireAnimation.Play();
        OnAmmoChange.Invoke(/*TypeOfWeapon,*/ InMag, MagSize);
        if (ReloadAfterLastShot && InMag == 0)
        {
            StartReload();
        }
    }
    public virtual void TriggerDown()
    {
        if (InMag > 0)
        {
            if (TBSTimer <= 0)
            {
                Fire();
            }
        }
    }
    public virtual void Reload()
    {
        InMag = MagSize;
        TBSTimer = 0;
        Reloading = false;
        OnAmmoChange.Invoke(/*TypeOfWeapon,*/ InMag, MagSize);
    }
    public virtual void StartReload()
    {
        ReloadTimer = ReloadTime;
        Reloading = true;
    }
}