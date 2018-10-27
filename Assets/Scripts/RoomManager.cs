﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.Experimental.EditorVR.Core;
using UnityEditor.SceneManagement;


public class RoomManager : MonoBehaviour {

    private string sampleRoomPath = "Assets/Rooms/";
    private string roomPath = "Assets/Scenes/";
    private static RoomManager _instance;
    public static RoomManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public bool CreateRoom(string roomName, string roomType = "Default")
    {
        string pathToExistingRoom = sampleRoomPath + roomType;
        string pathToNewRoom = roomPath + roomName;

        if (string.IsNullOrEmpty(roomName) || isRoomNameValid(pathToNewRoom))
        {
            throw new System.Exception("Invalid room name or room already exists!");
        }
       return AssetDatabase.CopyAsset(pathToExistingRoom, pathToNewRoom);
    }

    public void EditRoom(string roomName)
    {
        string path = roomPath + roomName;

        if (string.IsNullOrEmpty(roomName) || !isRoomNameValid(path))
        {
            throw new System.Exception("Invalid room name or room doesn't exists!");
        }
    }

    public bool DeleteRoom(string roomName)
    {
        string path = roomPath + roomName;

        if (string.IsNullOrEmpty(roomName) || !isRoomNameValid(path))
        {
            throw new System.Exception("Invalid room name or room doesn't exists!");
        }

        return AssetDatabase.DeleteAsset(path);

    }

    void Update()
    {
        if (!Application.isPlaying)
        {

            if (!string.IsNullOrEmpty(SceneState.sceneTo) && SceneState.alreadyStarted && SceneState.gamePlayed)
            {
                EditorSceneManager.OpenScene(roomPath + SceneState.sceneTo);
                EditingContextManager.ShowEditorVR();

            }


        }
    }

    public bool isRoomNameValid(string sampleRoomPath)
    {
        return File.Exists(sampleRoomPath);
    }

    public List<string> GetAllRooms(string roomType = "SampleRooms" )
    {
        if(roomType == "SampleRooms")
        {
            roomType = "/Rooms";
        }
        else
        {
            roomType = "/Scenes";
        }
        List<string> allRooms = new List<string>();
        DirectoryInfo di = new DirectoryInfo(Application.dataPath + roomType);
        FileInfo[] files = di.GetFiles();
        foreach (var file in files)
        {
            print(file.Name);
            allRooms.Add(file.Name);
        }
        return allRooms;
        
    }

}