using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GenerationState
{
    Idle,
    GeneratingRooms,
    GeneratingLighting,
    GeneratingSpawn,
    GeneratingExit
}
public class GenerationManager : MonoBehaviour
{
    [SerializeField] Transform WorldGrid;

    [SerializeField] List<GameObject> RoomTypes;

    [SerializeField] List<GameObject> LightTypes;

    [SerializeField] int mapSize = 8;

    [SerializeField] Slider mapSizeSlider;

    [SerializeField] Button generateButton;

    [SerializeField] GameObject E_Room;

    [SerializeField] GameObject SpawnRoom, ExitRoom;

    private List<GameObject> GeneratedRooms;

    [Header("Settings")]
    public int mapEmptiness;

    public int mapBrightness;

    private int mapSizeSquare;

    private Vector3 currentPos;

    private float currentPosX, currentPosZ;

    private int currentPosTracker;

    public float roomSize = 7;

    public GenerationState currentState;

    private void Update()
    {
        mapSize = (int)Mathf.Pow(mapSizeSlider.value, 4);
        mapSizeSquare = (int)Mathf.Sqrt(mapSize);
    }
    public void ReloadWorld()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GenerateWorld()
    {
        for(int i = 0; i < mapEmptiness; i++)
        {
            RoomTypes.Add(E_Room);
        }

        generateButton.interactable = false;

        for (int state = 0; state < 3; state++)
        {

            for (int i = 0; i < mapSize; i++)
            {
                if (currentPosTracker == mapSizeSquare)
                {
                    currentPosX = 0;
                    currentPosTracker = 0;
                    currentPosZ += roomSize;
                }

                currentPos = new(currentPosX, 0, currentPosZ);
                switch (currentState)
                {
                    case GenerationState.GeneratingRooms:
                        GeneratedRooms.Add(Instantiate(RoomTypes[Random.Range(0, RoomTypes.Count)], currentPos, Quaternion.identity, WorldGrid));
                    break;

                    case GenerationState.GeneratingLighting:
                        int lightSpawn = Random.Range(-1, mapBrightness);
                        if(lightSpawn == 0)
                            Instantiate(LightTypes[Random.Range(0, LightTypes.Count)], currentPos, Quaternion.identity, WorldGrid);
                    break;
                }

                currentPosTracker++;
                currentPosX += roomSize;
            }
            NextState();

            switch (currentState)
            {
                case GenerationState.GeneratingExit:
                    int roomToReplace = Random.Range(0, GeneratedRooms.Count);
                    //GameObject exitRoom = Instantiate(ExitRoom, GeneratedRooms[roomToReplace], transform.position, Quaternion.identity, WorldGrid); 
                    break;
                case GenerationState.GeneratingLighting:
                    break;
            }
        }
    }

    public void NextState()
    {
        currentState++;
        currentPosX = 0;
        currentPosZ = 0;
        currentPosTracker = 0;
        currentPos = Vector3.zero; 
    }
}
