using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// T�rman�� noktas�
/// </summary>
public class ClimbPoint : MonoBehaviour
{
    [SerializeField] List<Neighbour> neighbours;

    private void Awake()
    {
        // iki y�nl� kom�ular� getir
        var twoWayNeighbours = neighbours.Where(n => n.isTwoWay);
        foreach (var neighbour in twoWayNeighbours)
        {
            neighbour.point?.CreateConnection(this, -neighbour.direction, neighbour.connectionType, neighbour.isTwoWay);
        }
        
    }

    /// <summary>
    /// Verilen bilgiler do�rultusunda yeni bir kom�uluk ba�lant�s� olu�turur
    /// </summary>
    /// <param name="point"></param>
    /// <param name="direction"></param>
    /// <param name="connectionType"></param>
    /// <param name="isTwoWay"></param>
    public void CreateConnection(ClimbPoint point, Vector2 direction, ConnectionType connectionType,
        bool isTwoWay = true)
    {
        var neighbour = new Neighbour()
        {
            point = point,
            direction = direction,
            connectionType = connectionType,
            isTwoWay = isTwoWay
        };

        neighbours.Add(neighbour);
    }

  
    public Neighbour GetNeighbour(Vector2 direction)
    {
        Neighbour neighbour = null;

        if (direction.y != 0)
            neighbour = neighbours.FirstOrDefault(n => n.direction.y == direction.y);

        if (neighbour == null && direction.x != 0)
            neighbour = neighbours.FirstOrDefault(n => n.direction.x == direction.x);

        return neighbour;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.blue);

        foreach (var neighbour in neighbours)
        {
            if (neighbour.point != null)
                Debug.DrawLine(transform.position, neighbour.point.transform.position, (neighbour.isTwoWay) ? Color.green : Color.gray);
        }
    }
}

/// <summary>
/// Kom�u
/// </summary>
[System.Serializable]
public class Neighbour
{
    public ClimbPoint point;
    public Vector2 direction;
    public ConnectionType connectionType;
    /// <summary>
    /// iki yonlu m�?: yani kom�uya atlad�ktan sonra oradan geri d�nebilir miyim?
    /// </summary>
    public bool isTwoWay = true;
}

/// <summary>
/// Ba�lant� tipi: Z�playarak m� yoksa sa� sol hareketi ile mi ba�lant�l� oldu�u se�ilir
/// </summary>
public enum ConnectionType { Jump, Move }
