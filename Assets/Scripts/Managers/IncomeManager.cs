using UnityEngine;
using System.Collections;

public class IncomeManager : MonoBehaviour
{

    private float timer;
    private const float INCOME_TIMER = 5;

    // Use this for initialization
    void Start()
    {
        this.timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.timer += Time.deltaTime;
        if (this.timer >= INCOME_TIMER) {
            this.timer -= INCOME_TIMER;
            this.addIncome();
        }
    }

    private void addIncome() {
        ControllerManager[] players = FindObjectsOfType<ControllerManager>();
        foreach (ControllerManager p in players) {
            p.money += p.income;
        }
    }
}
