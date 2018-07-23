using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public controller playerController;
    public AnimationCurve DropOffCurve;
    public float Damage = 30;
    public float force = 9999;
    public float PelletsPerShell = 5;
    public float EffectiveRange = 30;
    public float GroupingDistance = 10;
    public float GroupingRadius = 1;
    public override void TriggerDown()
    {
        if (playerController.firegun)
        {
            base.TriggerDown();
        }
    }
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
            GameObject spawned = new GameObject(); /* ulletPool.BulletPoolInstance.getObject(transform.position);*/
            spawned.GetComponent<Rigidbody>().AddForce(ray.direction * force);
            spawned.GetComponent<TrailRenderer>().Clear();
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
    }
    public void CancelReload()
    {
        Reloading = false;
        ReloadTimer = ReloadTime;
    }
    public override void Reload()
    {
        //base.Reload();

        if (InMag < MagSize)
        {
            InMag++;
            if (InMag == MagSize)
            {
                Reloading = false;
            }
            else
            {
                StartReload();
            }
            OnAmmoChange.Invoke(/*TypeOfWeapon,*/ InMag, MagSize);
        }
    }
    public override void TriggerDown()
    {
        base.TriggerDown();
    }
    public override void StartReload()
    {
        base.StartReload();
    }
    void FixedUpdate()
    {
        TBSTimer -= Time.deltaTime;
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
        OnAmmoChange.Invoke(/*TypeOfWeapon,*/ InMag, MagSize);
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