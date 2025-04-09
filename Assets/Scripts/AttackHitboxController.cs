using UnityEngine;

public class AttackHitboxController : MonoBehaviour
{
    public GameObject hitbox1, hitbox2, hitbox3;

    public void EnableHitbox1()=> hitbox1.SetActive(true);
    public void EnableHitbox2()=> hitbox2.SetActive(true);
    public void EnableHitbox3()=> hitbox3.SetActive(true);

    public void DisableHixbox()
    {
        hitbox1.SetActive(false);
        hitbox2.SetActive(false);
        hitbox3.SetActive(false);
    }
}
