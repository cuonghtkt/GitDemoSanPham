using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPlayer : MonoBehaviour
{
    dulieugame data;
    public GameObject[] khoaSkill;
    public bool[] skillCD;
    public GameObject[] imgSkill;
    public GameObject player, objskill5, objskill2, objskill3, objskill4, objskill1, blackSkill5;
    public AudioSource asSKill;
    // Start is called before the first frame update
    void Start()
    {
        data = SaveData.LoadData();
        KiemTraKhoaSkill();
    }

    // Update is called once per frame
    void Update()
    {
        if (data.skill1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && skillCD[0] && playGameController.instance.hatThong > 0)
            {
                StartCoroutine(skill1());
            }
        }
        if (data.skill2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2) && skillCD[1])
            {
                StartCoroutine(skill2());
            }
        }
        if (data.skill3)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3) && skillCD[2])
            {
                StartCoroutine(skill3());
            }
        }
        if (data.skill4)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4) && skillCD[3])
            {
                StartCoroutine(skill4());
            }
        }
        if (data.skill5)
        {
            if (Input.GetKeyDown(KeyCode.Alpha5) && skillCD[4])
            {
                StartCoroutine(skill5());
            }
        }
    }
    public void KiemTraKhoaSkill()
    {
        if (data.skill1)
        {
            Destroy(khoaSkill[0]);
        }
        if (data.skill2)
        {
            Destroy(khoaSkill[1]);
        }
        if (data.skill3)
        {
            Destroy(khoaSkill[2]);
        }
        if (data.skill4)
        {
            Destroy(khoaSkill[3]);
        }
        if (data.skill5)
        {
            Destroy(khoaSkill[4]);
        }
    }
    IEnumerator skill1()
    {
        asSKill.volume = SaveData.dataNow.vol;
        asSKill.Play();
        skillCD[0] = false;
        imgSkill[0].SetActive(false);
        efficSkill1();
        yield return new WaitForSeconds(0.1f);
        imgSkill[0].SetActive(true);
        skillCD[0] = true;
    }
    void efficSkill1()
    {
        Instantiate(objskill1, gameObject.transform.position, transform.rotation);
        playGameController.instance.AddHatThong(-1);

    }
    IEnumerator skill2()
    {
        asSKill.volume = SaveData.dataNow.vol;
        asSKill.Play();
        efficSkill2();
        skillCD[1] = false;
        imgSkill[1].SetActive(false);
        yield return new WaitForSeconds(10);
        imgSkill[1].SetActive(true);
        skillCD[1] = true;
    }
    void efficSkill2()
    {
        int hp = Hp.hp;
        hp += 2;
        if (hp >= SaveData.dataNow.Hp)
        {
            hp = SaveData.dataNow.Hp;
        }
        Hp.hp = hp;
        Instantiate(objskill2, player.transform.position, transform.rotation);

    }
    IEnumerator skill3()
    {
        asSKill.volume = SaveData.dataNow.vol;
        asSKill.Play();
        efficSkill3();
        skillCD[2] = false;
        imgSkill[2].SetActive(false);
        yield return new WaitForSeconds(10);
        imgSkill[2].SetActive(true);
        skillCD[2] = true;
    }
    void efficSkill3()
    {
        Instantiate(objskill3, player.transform.position, transform.rotation);
        Vector2 vitri = player.transform.position;
        vitri.x += 3 * player.transform.localScale.x;
        player.transform.position = vitri;
    }
    IEnumerator skill4()
    {
        asSKill.volume = SaveData.dataNow.vol;
        asSKill.Play();
        Time.timeScale = 0.5f;
        objskill4.SetActive(true);
        skillCD[3] = false;
        imgSkill[3].SetActive(false);
        yield return new WaitForSeconds(2);
        Time.timeScale = 1;
        objskill4.SetActive(false);
        yield return new WaitForSeconds(10);
        imgSkill[3].SetActive(true);
        skillCD[3] = true;
    }
    IEnumerator skill5()
    {
        asSKill.volume = SaveData.dataNow.vol;
        asSKill.Play();
        efficSkill5();
        skillCD[4] = false;
        imgSkill[4].SetActive(false);
        blackSkill5.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        blackSkill5.SetActive(false);
        yield return new WaitForSeconds(10);
        imgSkill[4].SetActive(true);
        skillCD[4] = true;
    }
    void efficSkill5()
    {
        Instantiate(objskill5, gameObject.transform.position, transform.rotation);
        scpitskill5.skill5Die = false;
    }
}
