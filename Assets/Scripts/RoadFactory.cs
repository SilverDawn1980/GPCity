using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class RoadFactory
    {
        public static GameObject getRoad(StreetView type)
        {
            GameObject road;
            switch (type)
            {
                case StreetView.NS:
                    road = GameObject.Instantiate(GameManager.getInstance().RoadStraightPrefab);
                    road.transform.rotation = Quaternion.Euler(0f,0f,0f);
                    Debug.Log("NS");
                    break;
                case StreetView.WE:
                    road = GameObject.Instantiate(GameManager.getInstance().RoadStraightPrefab);
                    road.transform.rotation = Quaternion.Euler(0f,90f,0f);
                    Debug.Log("WE");
                    break;
                case StreetView.NE:
                    road = GameObject.Instantiate(GameManager.getInstance().RoadCornerPrefab);
                    road.transform.rotation = Quaternion.Euler(0f,180f,0f);
                    break;
                case StreetView.ES:
                    road = GameObject.Instantiate(GameManager.getInstance().RoadCornerPrefab);
                    road.transform.rotation = Quaternion.Euler(0f,-90f,0f);
                    break;
                case StreetView.SW:
                    road = GameObject.Instantiate(GameManager.getInstance().RoadCornerPrefab);
                    road.transform.rotation = Quaternion.Euler(0f,0f,0f);
                    break;
                case StreetView.WN:
                    road = GameObject.Instantiate(GameManager.getInstance().RoadCornerPrefab);
                    road.transform.rotation = Quaternion.Euler(0f,90f,0f);
                    break;
                case StreetView.NES:
                    road = GameObject.Instantiate(GameManager.getInstance().RoadJunctionPrefab);
                    road.transform.rotation = Quaternion.Euler(0f,180f,0f);
                    break;
                case StreetView.ESW:
                    road = GameObject.Instantiate(GameManager.getInstance().RoadJunctionPrefab);
                    road.transform.rotation = Quaternion.Euler(0f,-90f,0f);
                    break;
                case StreetView.SWN:
                    road = GameObject.Instantiate(GameManager.getInstance().RoadJunctionPrefab);
                    road.transform.rotation = Quaternion.Euler(0f,0f,0f);
                    break;
                case StreetView.WNE:
                    road = GameObject.Instantiate(GameManager.getInstance().RoadJunctionPrefab);
                    road.transform.rotation = Quaternion.Euler(0f,90f,0f);
                    break;
                case StreetView.CROSS:
                    road = GameObject.Instantiate(GameManager.getInstance().RoadCrossPrefab);
                    road.transform.rotation = Quaternion.Euler(0f,0f,0f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            return road;
        }
    }
}