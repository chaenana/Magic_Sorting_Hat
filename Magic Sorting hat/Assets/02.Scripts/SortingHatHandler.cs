using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SortingHatHandler : MonoBehaviour
{

    public CanvasGroup CG_MainBG, CG_BlackDim;
    public Button Btn_MainEnter, Btn_NextBtns, Btn_Feast;
    // public List<int> HouseList;
    public GameObject[] GO_Houses;
    /*
    1:그리핀도르
    2:슬리데린
    3:후플푸프
    4:레번클로 
    */
    public Animator Ani_SortingHat;
    public RectTransform RT_SortingHat;
    public GameObject GO_Paper;
    public int NextInt = 0;
    public int[] HouseList;

    public AudioClip[] SortingAudio;
    public AudioSource audio_Sorting, audio_feast;
    void Start()
    {
        Application.targetFrameRate = 60;
        Btn_MainEnter.onClick.AddListener(() =>
        {
            EnterToSorting();
        });
        Btn_NextBtns.onClick.AddListener(() =>
        {
            Next();
        });
        Btn_Feast.onClick.AddListener(() =>
        {
            FeastStart();
        });
    }
    void FeastStart()
    {
        audio_feast.Play();
    }

    void Next()
    {
        if (NextInt + 1 < 4)
        {
            NextInt++;
            print(NextInt);
            GO_Paper.GetComponent<RectTransform>().DOScale(new Vector3(0, 1, 1), 0.5f).SetEase(Ease.OutSine);
            GO_Paper.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetEase(Ease.OutSine);
            CG_BlackDim.DOFade(0, 0.5f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                for (int i = 0; i < GO_Houses.Length; i++)
                {
                    GO_Houses[i].SetActive(false);
                }

                Invoke("PaperOpen", 6f);
                sortingHouse(NextInt);
            });
        }
    }

    void EnterToSorting()
    {
        CG_MainBG.DOFade(0, 0.5f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            CG_MainBG.blocksRaycasts = false;
            Ani_SortingHat.Play("SortinHatAni", 0);
            getrandomInt(4, 1, 5);
            Invoke("PaperOpen", 6f);
            sortingHouse(0);
        });
    }

    void PaperOpen()
    {
        GO_Paper.GetComponent<RectTransform>().DOScale(Vector3.one, 0.5f).SetEase(Ease.OutSine);
        GO_Paper.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutSine);
        CG_BlackDim.DOFade(1, 0.5f).SetEase(Ease.OutSine);
    }

    void sortingHouse(int Index)
    {
        if (HouseList[Index] == 1)
        {
            GO_Houses[0].SetActive(true);
            audio_Sorting.clip = SortingAudio[0];
        }
        else if (HouseList[Index] == 2)
        {
            GO_Houses[1].SetActive(true);
            audio_Sorting.clip = SortingAudio[1];
        }
        else if (HouseList[Index] == 3)
        {
            GO_Houses[2].SetActive(true);
            audio_Sorting.clip = SortingAudio[2];

        }
        else if (HouseList[Index] == 4)
        {
            GO_Houses[3].SetActive(true);
            audio_Sorting.clip = SortingAudio[3];
        }
        audio_Sorting.Play();
    }

    public int[] getrandomInt(int length, int min, int max)
    {
        HouseList = new int[length];
        bool isSame;
        for (int i = 0; i < length; i++)
        {
            while (true)
            {
                HouseList[i] = Random.Range(min, max);
                isSame = false;
                for (int j = 0; j < i; ++j)
                {
                    if (HouseList[j] == HouseList[i])
                    {
                        isSame = true;
                        break;
                    }
                }
                if (!isSame) break;
            }
        }
        return HouseList;
    }
}
