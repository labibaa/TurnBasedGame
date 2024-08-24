using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    public static GameSceneManager instance;
    [SerializeField]GameObject door;
    List<AsyncOperation> _scenesToLoad = new List<AsyncOperation>();    

    Queue<GameObject> PlanesToDelete = new Queue<GameObject>();
    bool IsSceneLoaded=false;

    public bool IsSceneLoaded1 { get => IsSceneLoaded; set => IsSceneLoaded = value; }

    public void AddToQueue(GameObject plane)
    {
        PlanesToDelete.Enqueue(plane);
        if (PlanesToDelete.Count>2)
        {
            Destroy(PlanesToDelete.Dequeue());
        }
    }






    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //LoadGame();
        //StartCoroutine(LoadProgress());
        //SceneManager.LoadSceneAsync((int)SceneIndexes.PersistantScene,LoadSceneMode.Additive);
    }

    public void LoadGame()
    {
       // _scenesToLoad.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.PersistantScene));
        _scenesToLoad.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.Room1, LoadSceneMode.Additive));
        StartCoroutine(LoadProgress());
       

    }
    private void Update()
    {
        if (_scenesToLoad.Count>0)
        {
            Debug.Log("Progress " + _scenesToLoad[0].progress);
        }
       
    }
    public IEnumerator LoadProgress()
    {
        foreach (var scene in _scenesToLoad)
        {
            while(!scene.isDone)
            {
               
                yield return null;
            }
            door.SetActive(true);
        }
    }
}
