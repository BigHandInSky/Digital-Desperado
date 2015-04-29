using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//script which handles data from LoadedLevelsData, then creates a 2D map from it
public class MapUI : MonoBehaviour {
        
    [SerializeField] private MapSpawnerScript PlayerSpawner;
    [SerializeField] private MapSpawnerScript TargetSpawner;
    [SerializeField] private MapSpawnerScript EndLvlSpawner;
    [SerializeField] private MapSpawnerScript PlatformSpawner;
    [SerializeField] private MapSpawnerScript TowerSpawner;

    [SerializeField] private float MapPosScale = 5f;
    public float PosScale { get { return MapPosScale; } }
    [SerializeField] private float MapSizeScale = 0.20f;
    public float SizeScale { get { return MapSizeScale; } }

    public void Setup()
    {
        vClearMap();

        List<MenuLoadLevelsFromXML.MenuLoadXMLMapData> _mapList = MenuLoadLevelsFromXML.Instance.GetLevelObjs(LoadedLevels.Instance.iCurrentLvl);

        foreach (MenuLoadLevelsFromXML.MenuLoadXMLMapData obj in _mapList)
        {
            if (obj.Type == MenuLoadLevelsFromXML.MapDataObjType.Play)
                vSetupMapUIPlayer(obj.Position, obj.Rotation.y);

            else if (obj.Type == MenuLoadLevelsFromXML.MapDataObjType.Targ)
                vSetupMapUITarget(obj.Position, obj.Rotation.y);

            else if (obj.Type == MenuLoadLevelsFromXML.MapDataObjType.Levl)
                vSetupMapUILevel(obj.Position, obj.Scale, obj.Rotation.y);

            else if (obj.Type == MenuLoadLevelsFromXML.MapDataObjType.Towr)
                vSetupMapUITower(obj.Position, obj.Scale, obj.Rotation.y);

            else if (obj.Type == MenuLoadLevelsFromXML.MapDataObjType.EndT)
                vSetupMapUIEndTower(obj.Position, obj.Rotation.y);
        }
    }

    public void vClearMap()
    {
        PlayerSpawner.ClearImages();
        TargetSpawner.ClearImages();
        EndLvlSpawner.ClearImages();
        PlatformSpawner.ClearImages();
        TowerSpawner.ClearImages();
    }
    
    public void vSetupMapUIPlayer(Vector2 _Pos, float _yAxisRot)
    {
        PlayerSpawner.Create(_Pos, Vector3.one, _yAxisRot);
    }
    public void vSetupMapUIEndTower(Vector3 _Pos, float _yAxisRot)
    {
        EndLvlSpawner.Create(_Pos, Vector3.one, _yAxisRot);
    }
    public void vSetupMapUITarget(Vector3 _Pos, float _yAxisRot)
    {
        TargetSpawner.Create(_Pos, Vector3.one, _yAxisRot);
    }
    public void vSetupMapUILevel(Vector3 _Pos, Vector3 _Scale, float _yAxisRot)
    {
        PlatformSpawner.Create(_Pos, _Scale, _yAxisRot);
    }
    public void vSetupMapUITower(Vector3 _Pos, Vector3 _Scale, float _yAxisRot)
    {
        TowerSpawner.Create(_Pos, _Scale, _yAxisRot);
    }
}
