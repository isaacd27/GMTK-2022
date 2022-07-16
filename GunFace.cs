using System;
using System.Collections.Generic;
using UnityEngine;

public class GunFace : MonoBehaviour
{
    public Projectile projPrefab;
    public Stake staPrefab;
    public Grenade gPrefab;


    public event EventHandler OnWeaponChanged;
    public event EventHandler OnPickedUpWeapon;
    public event EventHandler OnDodged;

    private enum State
    {
        Normal,
    }

   // private PlayerMain playerMain;
    public string Weapon = "Stake";
    public float stakecool = 0.5f;
    public int shotnumbul;
   

    private bool canUseShotgun;
    private bool canUseRifle = true;

    public int pistolAmmo = 10;
    public float pistolcool = 1f;

    public int rifleAmmo = 6;
    public float rifleCool = 2f;

    public int Grendaes = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void setPammo(int Delta)
    {
        pistolAmmo += Delta;
    }

    public void setRammo(int Delta)
    {
        rifleAmmo += Delta;
    }

    public void SetGrenades(int Delta)
    {
        Grendaes += Delta;
    }
    public void SetCanUseShotgun()
    {
        canUseShotgun = true;
        //SetWeapon(weaponShotgun);
    }

    public void SetCanUseRifle()
    {
        canUseRifle = true;
        //SetWeapon(weaponRifle);
    }

    public void SetWeapon(String weapon)
    {
        Weapon = weapon;
        //playerMain.PlayerSwapAimNormal.SetWeapon(weapon);
       // OnWeaponChanged?.Invoke(this, EventArgs.Empty);
    }

  

     public String GetWeapon()
      {
     return Weapon;
     }

    // Update is called once per frame
    void Update()
    {
       // Grendaes = GameStateManager.Instance.getBombs();
        Vector3 Mouseposition = Input.mousePosition;

        Mouseposition = Camera.main.ScreenToWorldPoint(Mouseposition);

        Vector2 Direction = new Vector2(
            Mouseposition.x - transform.position.x,
             Mouseposition.y - transform.position.y
             );


        transform.up = Direction;
    
    

       



        pistolcool -= Time.deltaTime;
        rifleCool -= Time.deltaTime;
        stakecool -= Time.deltaTime;

        if (Input.GetAxis("Fire1") != 0)
        {
          //  if(Weapon == "Stake" || Weapon == "Grenade")
          //  {
              //  Weapon = "Pistol";
          //  }
            onShoot(Direction);
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Danger"))
        {
            GameStateManager.Instance.OnDeath();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Danger"))
        {
            Debug.Log("hit");
        }
    }

    public void onShoot(Vector2 d)
    {
        if (Weapon == "Pistol")
        {
            if (pistolcool <= 0f)
            {

                if (pistolAmmo > 0)
                {
                    pistolAmmo -= 1;

                    Projectile temp = GameObject.Instantiate(projPrefab, new Vector3(this.transform.position.x + d.x, this.transform.position.y + d.y), this.transform.rotation);

                    temp.transform.position = this.transform.position + this.transform.up * 0.4f * Mathf.Sign(this.transform.localScale.x);
                   // temp.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);


                    temp.setDirection(d);
                    pistolcool = 1f;
                }
            }



        }
        else if (Weapon == "Rifle")
        {
            if (rifleCool <= 0f)
            {

                if (rifleAmmo > 0)
                {
                    rifleAmmo -= 1;

                    for (int i = 0; i > 3; i++)
                    {
                       

                        Projectile temp = GameObject.Instantiate(projPrefab, new Vector3(this.transform.position.x + d.x, this.transform.position.y + d.y), Quaternion.AngleAxis(i * 360 / 45, d));
                        temp.transform.position = this.transform.position + transform.up * 0.4f * Mathf.Sign(this.transform.localScale.x);

                        //temp.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);

                        temp.setDirection(Quaternion.AngleAxis(i * 360 / 45, d)*Vector2.one);


                    }





                  
                    rifleCool = 2f;
                }
            }
        }
        else if (Weapon == "Stake")
        {
            if(stakecool <= 0)
            {
                Stake temp = Instantiate(staPrefab, new Vector3(this.transform.position.x + d.x, this.transform.position.y + d.y), this.transform.rotation);

                temp.transform.position = this.transform.position + transform.up * 0.4f * Mathf.Sign(this.transform.localScale.x);
                temp.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);

                temp.setDirection(d);
                stakecool = 0.5f;
            }

        }
        else if (Weapon == "Shotgun")
        {
            for (int i = 0; i < shotnumbul; i++)
            {
                // Debug.Log("ran ");
                Projectile temp = GameObject.Instantiate(projPrefab, new Vector3(this.transform.position.x + d.x, this.transform.position.y + d.y), this.transform.rotation);

                temp.transform.position = this.transform.position + this.transform.up * 0.4f * Mathf.Sign(this.transform.localScale.x);
                // temp.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);

                temp.setDirection(d * 45 *i);


            }

        }

    }
}
