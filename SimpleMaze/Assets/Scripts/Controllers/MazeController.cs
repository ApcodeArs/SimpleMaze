using Helpers;
using Models;
using UnityEngine;
using Utils;

public class MazeController : MonoBehaviourSingletonBase<MazeController> {
    
    [SerializeField]
    private GameObject _cellPrefab;

    [SerializeField]
    private RectTransform _mazeParent;
    
    [SerializeField]
    private Camera _camera;
    
    private Vector3 _cellSize = new Vector3(1.5f,1.5f,0);

    [SerializeField]
    private GameObject p1;
    [SerializeField]
    private GameObject p2;
    [SerializeField]
    private GameObject p3;
    [SerializeField]
    private GameObject p4;
    
    public void Init() {
        var sa = Screen.safeArea;
        
        p1.transform.position = _camera.ScreenToWorldPoint(sa.position);
        p1.transform.position = new Vector3(p1.transform.position.x, p1.transform.position.y, -0.75f);
        
        p2.transform.position = _camera.ScreenToWorldPoint(sa.position + new Vector2(sa.width, 0.0f));
        p2.transform.position = new Vector3(p2.transform.position.x, p2.transform.position.y, -0.75f);
        
        p3.transform.position = _camera.ScreenToWorldPoint(sa.position + new Vector2(0.0f, sa.height));
        p3.transform.position = new Vector3(p3.transform.position.x, p3.transform.position.y, -0.75f);
        
        p4.transform.position = _camera.ScreenToWorldPoint(sa.position + new Vector2(sa.width, sa.height));
        p4.transform.position = new Vector3(p4.transform.position.x, p4.transform.position.y, -0.75f);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        var safeAreaWorldWidth = p2.transform.position.x - p1.transform.position.x;
        var safeAreaWorldHeight = p3.transform.position.y - p1.transform.position.y;
        
        var cellColumns = Mathf.FloorToInt(safeAreaWorldWidth / _cellSize.x);
        var cellRows =  Mathf.FloorToInt(safeAreaWorldHeight / _cellSize.y);
        
        _mazeParent.sizeDelta = new Vector2((cellColumns - 1) * _cellSize.x, (cellRows - 1) * _cellSize.y);;


        /*var deltaY = safeAreaWorldHeight / 2 - _mazeParent.sizeDelta.y / 2;*/

        _mazeParent.transform.position = p1.transform.position
                                         + new Vector3(safeAreaWorldWidth / 2, safeAreaWorldHeight / 2, 0.0f);
                                         /*+ new Vector3(0.0f, - deltaY, 0.0f);*/
        
        var cells = MazeGenerator.Generate(cellColumns, cellRows);

        var shifting = _mazeParent.transform.position + new Vector3(-_mazeParent.sizeDelta.x / 2f, -_mazeParent.sizeDelta.y / 2f);

        for (var x = 0; x < cellColumns; x++) {
            for (var y = 0; y < cellRows; y++) {
                var position = shifting + new Vector3(x * _cellSize.x, y * _cellSize.y, y * _cellSize.z);
                var cellGameObject = Instantiate(_cellPrefab, position, Quaternion.identity, _mazeParent.transform);
        
                var cell = cellGameObject.GetComponent<Cell>();
                cell.Init(cells[x, y]);
            }
        }
    }
}
