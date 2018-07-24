using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shotgun : Gun
{
    [Header("Shotgun settings")]
    public controller1 playerController;
    public AnimationCurve DropOffCurve;
    public float Damage = 30;
    public float force = 9999;
    [Header("Grouping settings")]
    public float PelletsPerShell = 5;
    public float EffectiveRange = 30;
    public float GroupingDistance = 10;
    public float GroupingRadius = 1;
    public override void Fire()
    {
        base.Fire();
        List<Ray> rays = new List<Ray>();
        for (int i = 0; i < PelletsPerShell; i++)
        {
            //Debug.DrawLine(transform.position, transform.forward * 5, Color.green);
            rays.Add(new Ray(transform.position, (/*transform.position +*/ (transform.forward * GroupingDistance) + (Random.insideUnitSphere * GroupingRadius))));
            //Debug.DrawLine(transform.position, (transform.position + (transform.forward * GroupingDistance) + (Random.insideUnitSphere * GroupingRadius)), Color.red);
        }
        foreach (Ray ray in rays)
        {
            GameObject spawned =  BulletTrailPool.instance.pool.getObject(transform.position);
            spawned.GetComponent<Rigidbody>().AddForce(ray.direction * force);
            spawned.GetComponent<TrailRenderer>().Clear();
            spawned.GetComponent<Bullet>().alive = true;
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (RaycastHit hit in hits)
            {
                Debug.DrawLine(transform.position, hit.point);
                if (hit.collider.tag == "Enemy")
                {
                    float damagepercentage = Mathf.Clamp(DropOffCurve.Evaluate(hit.distance / EffectiveRange), 0, 1);
                    //hit.collider.GetComponent<IDamageable>().TakeDamage(Damage * (damagepercentage));
                }
            }
        }
        RecoilMovement.instance.Move(transform.forward);
    }
    public void CancelReload()
    {
        Reloading = false;
        ReloadTimer = ReloadTime;
        ReloadInicator.instance.StopReload();
        playerController.reload1 = false;
    }
    public override void Reload()
    {
        InMag = MagSize;
        Reloading = false;
        ReloadInicator.instance.StopReload();
        playerController.reload1 = false;
    }
    public override void TriggerDown()
    {
        base.TriggerDown();
    }
    public override void StartReload()
    {
        playerController.reload1 = true;
        ReloadInicator.instance.StartReload(ReloadTime);
        base.StartReload();
    }
    void FixedUpdate()
    {
        TBSTimer -= Time.deltaTime;
        if (playerController.firegun)
        {
            base.TriggerDown();
        }
        if (playerController.reloadgun == true && playerController.reload1 == false)
        {
            StartReload();
        }
        //Reload logic
        if (Reloading)
        {
            ReloadTimer -= Time.deltaTime;
            if (ReloadTimer <= 0)
            {
                Reload();
            }
        }
    }
    void Start()
    {
        Fire();
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 endOfLine = transform.position + (transform.forward * GroupingDistance);
        Gizmos.DrawLine(transform.position, endOfLine);

        Gizmos.color = Color.gray;
        //Gizmos.DrawSphere(endOfLine, GroupingRadius);
        Gizmos.DrawWireSphere(endOfLine, GroupingRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, endOfLine + (Random.insideUnitSphere * GroupingRadius));
    }
}
    //buggy
    //public void CancelReload()
    //{
    //    Reloading = false;
    //    ReloadTimer = ReloadTime;
    //    ReloadInicator.instance.StopReload();
    //}
    //public override void Reload()
    //{
    //    //base.Reload();
    //    if (playerController.reload1 == true)
    //    {
    //        Reloading = false;
    //        InMag = MagSize;
    //        //just in case
    //        ReloadInicator.instance.StopReload();
    //        //reset player controller reload bools
    //        playerController.reloadgun = false;
    //        playerController.reload1 = false;
    //    }
    //}
    //public override void StartReload()
    //{
    //    reloadTimer.StartTimer(ReloadTime);

    //    //ReloadTimer = ReloadTime;
    //    playerController.reload1 = true;
    //    ReloadInicator.instance.StartReload(ReloadTime);
    //    //base.StartReload();
    //    Reloading = true;
    //}
    //void FixedUpdate()
    //{
    //    TBSTimer -= Time.deltaTime;
    //    //Debug.Log("firegun = " + playerController.firegun);
    //    if (playerController.firegun)
    //    {
    //        base.TriggerDown();
    //    }
    //    if (playerController.reloadgun == true && playerController.reload1 == false)
    //    {
    //        StartReload();
    //    }
    //    //Reload logic
    //    if (Reloading)
    //    {
    //        ReloadTimer -= Time.deltaTime;
    //        Debug.Log("RELOAD VALUE = " + (ReloadTime - ReloadTimer)/ReloadTime);
    //        if (ReloadTimer <= 0)
    //        {
    //            Debug.Log("reload complete");
    //            Reload();
    //        }
    //    }
    //}
    //void Start()
    //{
    //    OnAmmoChange.Invoke(/*TypeOfWeapon,*/ InMag, MagSize);
    //}
    //public void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Vector3 endOfLine = transform.position + (transform.forward * GroupingDistance);
    //    Gizmos.DrawLine(transform.position, endOfLine);

    //    Gizmos.color = Color.gray;
    //    //Gizmos.DrawSphere(endOfLine, GroupingRadius);
    //    Gizmos.DrawWireSphere(endOfLine, GroupingRadius);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position, endOfLine + (Random.insideUnitSphere * GroupingRadius));
    //}