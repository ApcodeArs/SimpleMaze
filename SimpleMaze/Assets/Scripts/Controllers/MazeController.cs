using Helpers;
using Models;
using UnityEngine;
using Utils;

public class MazeController : MonoBehaviourSingletonBase<MazeController> {
    
    [SerializeField] private GameObject _cellPrefab;

    [SerializeField] private GameObject _mazeParent;
    
    public void Init() {

        //todo improve
        var width = 21;
        var height = 16;
        
        var maze = MazeGenerator.Generate(width, height);
        
        for (var x = 0; x < width; x++) {
            for (var y = 0; y < height; y++) {
                //todo improve
                var cellGameObject = Instantiate(_cellPrefab, new Vector3(x, y), Quaternion.identity, _mazeParent.transform);
                
                var cell = cellGameObject.GetComponent<Cell>();
                cell.Init(maze[x,y]);

            }
        }
    }
}
