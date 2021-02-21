using UnityEngine;

public class Savior : MonoBehaviour
{
    [SerializeField, Min(0)] int life;

    [SerializeField] Number number;

    // Start is called before the first frame update
    void Start()
    {
        UpdateLife();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case TagsNames.Ball:
                life--;
                UpdateLife();
                break;
        }
    }

    void UpdateLife()
    {
        if (life <= 0)
        {
            gameObject.SetActive(false);
            life = 0;
            return;
        }
        else if (!gameObject.activeSelf) gameObject.SetActive(true);

        number.SetNumber(life);
    }

    public void AddLife(int value)
    {
        life += value;
        UpdateLife();
    }

    public void SetLife(int value)
    {
        life = value;
        UpdateLife();
    }
}
