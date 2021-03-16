using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
#pragma warning disable 0649
    [Header("Ammo")]
    [SerializeField] private int defaultAmmo = 120;
    [SerializeField] private int magSize = 30;
    [SerializeField] private int currentAmmo;
    [SerializeField] private int currentMagAmmo;
    [Header("Shoot")]
    [SerializeField] private float coolDown;
    [SerializeField] private int damage;
    [SerializeField] private new Camera camera;
    [SerializeField] private int range;
    [Header("FX")]
    [SerializeField] private GameObject bloodPrefab;
    [SerializeField] private GameObject decal;
    [SerializeField] private GameObject magObject;
    [SerializeField] private ParticleSystem muzzleParticle;
    [Header("UI")]
    [SerializeField] private Text changingTextAmmo;
    [SerializeField] private Text changingTextMag;
#pragma warning restore 0649
    private float _lastFireTime = 0;
    private int _minAngle = -2;
    private int _maxAngle = 2;

    void Start()
    {
        currentAmmo = defaultAmmo - magSize;
        currentMagAmmo = magSize;
        UpdateTextAmmo(currentAmmo);
        UpdateTextMagAmmo(currentMagAmmo);
    }

    void Update()
    {
        Shooting();
    }

    private void Shooting()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Reload();
        }
        if (Input.GetMouseButton(0))
        {
            if (CanFire())
            {
                Fire();
            }
        }
    }

    public void UpdateTextAmmo(int ammo)
    {
        changingTextAmmo.text = "Kalan mermi: " + ammo;
    }

    public void UpdateTextMagAmmo(int ammo)
    {
        changingTextMag.text = "Şarjörde kalan mermi: " + ammo;
    }

    private void Reload()
    {
        if (currentAmmo == 0 || currentMagAmmo == magSize)
        {
            return;
        }
        if (currentAmmo < magSize)
        {
            currentMagAmmo = currentMagAmmo + currentAmmo;
            UpdateTextMagAmmo(currentMagAmmo);
            currentAmmo = 0;
            UpdateTextAmmo(currentAmmo);
        }
        else
        {
            currentAmmo -= magSize - currentMagAmmo;
            UpdateTextAmmo(currentAmmo);
            currentMagAmmo = magSize;
            UpdateTextMagAmmo(currentMagAmmo);
        }
        GameObject newMagObject = Instantiate(magObject);
        newMagObject.transform.position = magObject.transform.position;
        newMagObject.AddComponent<Rigidbody>();
    }

    private bool CanFire()
    {
        if (currentMagAmmo > 0 && (_lastFireTime + coolDown) < Time.time)
        {
            _lastFireTime = Time.time + coolDown;
            return true;
        }
        else return false;

    }

    private void Fire()
    {
        muzzleParticle.Play(true);
        currentMagAmmo -= 1;
        UpdateTextMagAmmo(currentMagAmmo);
        Debug.Log("Az sık az: " + currentMagAmmo);
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 10))
        {
            if (hit.transform.tag == "Zombie")
            {
                hit.transform.GetComponent<ZombieHealth>().Hit(damage);
                GenerateBloodEffect(hit);
            }
            else
            {
                GenerateHitEffect(hit);
            }
        }
        transform.localEulerAngles = new Vector3(Random.Range(_minAngle, _maxAngle), Random.Range(_minAngle, _maxAngle), Random.Range(_minAngle, _maxAngle));
    }

    private void GenerateBloodEffect(RaycastHit hit)
    {
        GameObject bloodObject = Instantiate(bloodPrefab, hit.point, hit.transform.rotation);
        Debug.Log("Ah! ameliyatlı yerim");
    }

    private void GenerateHitEffect(RaycastHit hit)
    {
        //sürekli yeniden bir mermi izi oluşturmak optimizasyon için hiç iyi değil. bunu object pooling kullanarak oluşturucam.
        GameObject hitObject = Instantiate(decal, hit.point, Quaternion.identity);
        hitObject.transform.rotation = Quaternion.FromToRotation(decal.transform.forward * -1, hit.normal);
        Debug.Log("Ah! kör oldum");


    }
}
