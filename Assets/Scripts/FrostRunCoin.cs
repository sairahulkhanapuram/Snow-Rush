using UnityEngine;
public class FrostRunCoin : MonoBehaviour
{
    private bool collected;
    private void Update() { transform.Rotate(0f, 100f * Time.deltaTime, 0f, Space.World); }
    public void Collect()
    {
        if (collected) return;
        collected = true;
        if (FrostRunGame.Instance != null) FrostRunGame.Instance.AddCoin();
        Destroy(gameObject);
    }
}
