using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plantButtonScript : MonoBehaviour
{
    //この辺はボタンの持つ植物を渡したりするスクリプトです
    public int buttonNumber;
    public GameObject plantSeed;//このボタン（項目）が示す植物。
    [SerializeField] plantAdministerSystem pAs;

    public void OnButtonPressed()
    {
        pAs.buttonPressed(buttonNumber);
        pAs.selectedPlant = plantSeed;
        pAs.selectedPlantData = plantSeed.GetComponent<seedScript>().plantData;
        sOP.choosenButton = this;
    }

    //ここから下はUIのカバーを下げるコードです

    [SerializeField] GameObject cover;
    [SerializeField] Image coverImage;
    [SerializeField] float chargeTime;//装填時間

    public bool isReady  { get;  private set; } = false;//装填完了かどうか
    [SerializeField] shootsOutPlant sOP;
    private bool isCharge = false;//装填中かどうか

    void Start()
    {
        used();
    }
    public void used()//撃たれた(装填開始のサイン)
    {
        StartCoroutine(startCharge(chargeTime));
        isReady = false;
    }

    public IEnumerator startCharge(float time)//UIをカバーしてそれを下げるコードです
    {
        isCharge = true;
        cover.SetActive(true);
        coverImage.fillAmount = 1;
        while (coverImage.fillAmount > 0)
        {
            coverImage.fillAmount -= Time.deltaTime / time;
            yield return null;
        }
        isReady = true;
        cover.SetActive(false);
        coverImage.fillAmount = 0;
    }
}
