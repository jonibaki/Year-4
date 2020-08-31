using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Monopoly
{
    public class CanvasManager : MonoBehaviour

    {
        public static CanvasManager Instance;
        [SerializeField]
        private  ListRoomController _listRoomController;
        public ListRoomController ListRoomController { get { return _listRoomController; } }

        private void Awake()
        {
            Instance = this;

        }
    }
}
