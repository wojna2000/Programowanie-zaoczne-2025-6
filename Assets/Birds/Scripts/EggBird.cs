using UnityEngine;

public class EggBird : Bird
{
    [SerializeField] GameObject egg;
    [SerializeField] float eggSpeed;
    Rigidbody rbBird;
    bool activated = false;
    void Start()
    {
        rbBird = GetComponent<Rigidbody>();
    }

    public override void Activate()
    {
        if (activated) return;
        Vector3 currentPosition = transform.position;
        GameObject newEgg = Instantiate(egg);
        Rigidbody rb = newEgg.GetComponent<Rigidbody>();
        egg.transform.position = currentPosition + new Vector3(0f, -1f, 0f);
        rb.AddForce(Vector3.down * eggSpeed, ForceMode.Impulse);
        rbBird.AddForce(Vector3.up * eggSpeed, ForceMode.Impulse);
        activated = true;
    }
}
