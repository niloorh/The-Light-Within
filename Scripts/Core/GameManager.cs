using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int current_room = 1;
    //private string brightLightingScene = "full_bright";
    //private string dimLightingScene = "dim";
    //private string currentLightingScene;

    public GameObject door_1;
    public GameObject door_2;

    public GameObject[] lights;
    public float brightIntensity = 0.7f;
    public float dimIntensity = 0.2f;
    public float darkIntensity = 0.02f;
    public float firstDuration = 3f;
    public float secondDuration = 3f;
    public float thirdDuration = 3f;

    //public GameObject lightSwitcher;

    public Texture2D[] brightDir, brightColor;
    public Texture2D[] dimDir, dimColor;
    public Texture2D[] darkDir, darkColor;

    private LightmapData[] brightMap, dimMap, darkMap;

    //public LightmapCrossfadeGPU lightmapCrossfadeGPU;

    void setIntensity(float intensity)
    {
        foreach (var light in lights)
        {
            light.GetComponent<Light>().intensity = intensity;
        }
    }

    IEnumerator LerpIntensity(float from, float to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Calculate interpolation factor [0,1]
            float t = elapsed / duration;

            // Lerp between from and to
            setIntensity(Mathf.Lerp(from, to, t));

            elapsed += Time.deltaTime;
            yield return null; // wait for next frame
        }

        // Ensure it finishes exactly at target
        setIntensity(to);
    }

    // Start is called before the first frame update
    void Start()
    {
        setIntensity(brightIntensity);
        //lightSwitcher.GetComponent<LevelLightmapData>().LoadLightingScenario(0);
        //if (!SceneManager.GetSceneByName(brightLightingScene).isLoaded &&
        //    !SceneManager.GetSceneByName(dimLightingScene).isLoaded)
        //{
        //    StartCoroutine(SwitchTo(brightLightingScene));
        //}

        ///////////////////////////////////////////////////////
        //List<LightmapData> lightmap = new List<LightmapData>();

        //for (int i = 0; i < brightDir.Length; i++)
        //{
        //    LightmapData lmdata = new LightmapData();

        //    lmdata.lightmapDir = brightDir[i];
        //    //Debug.Log(brightDir[i]);
        //    lmdata.lightmapColor = brightColor[i];

        //    lightmap.Add(lmdata);
        //}

        //brightMap = lightmap.ToArray();
        ///////////////////////////////////////////////////////
        //lightmap = new List<LightmapData>();

        //for (int i = 0; i < dimDir.Length; i++)
        //{
        //    LightmapData lmdata = new LightmapData();

        //    lmdata.lightmapDir = dimDir[i];
        //    lmdata.lightmapColor = dimColor[i];

        //    lightmap.Add(lmdata);
        //}

        //dimMap = lightmap.ToArray();
        ///////////////////////////////////////////////////////
        //lightmap = new List<LightmapData>();

        //for (int i = 0; i < darkDir.Length; i++)
        //{
        //    LightmapData lmdata = new LightmapData();

        //    lmdata.lightmapDir = darkDir[i];
        //    lmdata.lightmapColor = darkColor[i];

        //    lightmap.Add(lmdata);
        //}

        //darkMap = lightmap.ToArray();
        ///////////////////////////////////////////////////////
        //LightmapSettings.lightmaps = brightMap;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private IEnumerator SwitchTo(string targetLightingScene)
    //{
    //    if (currentLightingScene == targetLightingScene) yield break;

    //    // 1) Load target additively
    //    Debug.Log("Loading scene...");
    //    var load =  SceneManager.LoadSceneAsync(targetLightingScene, LoadSceneMode.Additive);
    //    while (!load.isDone) yield return null;

    //    Debug.Log("Activating scene...");
    //    // 2) Make target the Active Scene so its RenderSettings (skybox/ambient/fog) apply
    //    var target = SceneManager.GetSceneByName(targetLightingScene);
    //    SceneManager.SetActiveScene(target);

    //    // 3) Unload previous lighting scene (if any)
    //    if (!string.IsNullOrEmpty(currentLightingScene))
    //    {
    //        Debug.Log("Unloading scene...");
    //        var unload = SceneManager.UnloadSceneAsync(currentLightingScene);
    //        while (unload != null && !unload.isDone) yield return null;
    //    }

    //    currentLightingScene = targetLightingScene;

    //    // Optional: if you change skybox via code, call this:
    //    // DynamicGI.UpdateEnvironment();
    //    Debug.Log("Done!");
    //    yield break;
    //}
    public void startGame()
    {
        StartCoroutine(LerpIntensity(brightIntensity, dimIntensity, firstDuration));
    }

    public void objective_is_done(int obj_code)
    {
        switch (current_room)
        {
            case 1:
                {
                    if (obj_code == 1)
                    {
                        door_1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        next_room();
                    }
                    break;
                }
            case 2:
                {
                    if (obj_code == 2)
                    {
                        door_2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        next_room();
                    }
                    break;
                }
            case 3:
                {
                    if (obj_code == 3)
                    {
                        Debug.Log("Done!");
                        next_room();
                    }
                    break;
                }
        }
    }

    void next_room()
    {
        switch (current_room)
        {
            case 1:
                {
                    current_room = 2;
                    //StartCoroutine(SwitchTo(dimLightingScene));
                    //lightSwitcher.GetComponent<LevelLightmapData>().LoadLightingScenario(1);
                    //LightmapSettings.lightmaps = dimMap;
                    //lightmapCrossfadeGPU.LightmapsA = LightmapSettings.lightmaps;
                    //lightmapCrossfadeGPU.LightmapsB = dimMap;
                    //lightmapCrossfadeGPU.StartCrossfade();
                    StartCoroutine(LerpIntensity(dimIntensity, darkIntensity, firstDuration));
                    //foreach (GameObject light in lights)
                    //{
                    //    light.GetComponent<Light>().intensity = 0.5f;
                    //}
                    break;
                }
            case 2:
                {
                    current_room = 3;
                    //lightSwitcher.GetComponent<LevelLightmapData>().LoadLightingScenario(2);
                    //LightmapSettings.lightmaps = darkMap;
                    //lightmapCrossfadeGPU.LightmapsA = LightmapSettings.lightmaps;
                    //lightmapCrossfadeGPU.LightmapsB = darkMap;
                    //lightmapCrossfadeGPU.StartCrossfade();
                    //StartCoroutine(LerpIntensity(dimIntensity, darkIntensity, secondDuration));
                    //foreach (GameObject light in lights)
                    //{
                    //    light.GetComponent<Light>().intensity = 0.25f;
                    //}
                    break;
                }
            case 3:
                {
                    //LightmapSettings.lightmaps = brightMap;
                    //lightmapCrossfadeGPU.LightmapsA = LightmapSettings.lightmaps;
                    //lightmapCrossfadeGPU.LightmapsB = brightMap;
                    //lightmapCrossfadeGPU.StartCrossfade();
                    StartCoroutine(LerpIntensity(darkIntensity, brightIntensity, thirdDuration));
                    Debug.Log("Finished!");
                    current_room = 0;
                    //foreach (GameObject light in lights)
                    //{
                    //    light.GetComponent<Light>().intensity = 0f;
                    //}
                    break;
                }
        }
    }
}
