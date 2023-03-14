using BestHTTP.SocketIO3;
using BestHTTP.SocketIO3.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketClientManager : SingletonComponent<SocketClientManager>
{
    public enum LOGIN_RESULT:int
    {
        CONNECT_FAIL = 0,
        NO_ID = 1,
        PASSWORD_ERROR = 2,
        SUCCESS = 3,
    }

    [SerializeField] private string address = "http://127.0.0.1:5000/";
    private SocketManager socketManager = null;

    private void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        SocketIO3Connet();
    }

    private void SocketIO3Connet()
    {
        Debug.Log("SocketClientManager >> Connect");

        SocketOptions options = new SocketOptions();
        options.AutoConnect = false;

        socketManager = new SocketManager(new System.Uri(address), options);
        socketManager.Open();

        socketManager.Socket.On<ConnectResponse>(SocketIOEventTypes.Connect, OnConnected);
        socketManager.Socket.On("PlayerConnected", PlayerConnected);
        socketManager.Socket.On<ConnectResponse>("ServerTest", ServerTest);

        socketManager.Socket.On<int>("testEvent", (value) => {
            Debug.Log("Get Test Event From Server : " + value);
        });

        socketManager.Socket.On<int>("join", (value) => {
            Debug.Log("Join Resoponse : " + value);
        });

        socketManager.Socket.On<LOGIN_RESULT>("login", (result) => {

            //LOGIN_RESULT result = (LOGIN_RESULT)value;
            switch (result)
            {
                case LOGIN_RESULT.CONNECT_FAIL:
                    Debug.Log("로그인 실패 : 데이터베이스 접속에 실패하였습니다");
                    break;
                case LOGIN_RESULT.NO_ID:
                    Debug.Log("로그인 실패 : 로그인 아이디를 찾을 수 없습니다");
                    break;
                case LOGIN_RESULT.PASSWORD_ERROR:
                    Debug.Log("로그인 실패 : 비밀번호 오류");
                    break;
                case LOGIN_RESULT.SUCCESS:
                    Debug.Log("로그인 성공");
                    break;
                default:
                    break;
            }
            Debug.Log("LOGIN RESULT : " + result.ToString());
        });


        socketManager.Socket.On("testEvent2", () => {
            Debug.Log("on Test Event");
        });

    }

    public struct JOIN_USER_DATA
    {
        public string email;
        public string nickName;
        public string password;

        public JOIN_USER_DATA(string email, string nickName, string password)
        {
            this.email = email;
            this.nickName = nickName;
            this.password = password;
        }
    }

    public struct LOGIN_USER_DATA
    {
        public string email;
        public string password;

        public LOGIN_USER_DATA(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }

    public struct LOGIN_RESPONSE
    {
        public int result;
        public GOODS_DATA goodsData;

        public LOGIN_RESPONSE(int result, GOODS_DATA goodsData)
        {
            this.result = result;
            this.goodsData = goodsData;
        }
    }

    public struct GOODS_DATA
    {
        public int GameMoney;
        public int Cash;
        public int Mileage;


        public GOODS_DATA(int GameMoney, int Cash, int Mileage)
        {
            this.GameMoney = GameMoney;
            this.Cash = Cash;
            this.Mileage = Mileage;
        }
    }


    public bool IsOn()
    {
        return socketManager.Socket.IsOpen;
    }

    public void Join(string email, string nickName, string password, Action<int> callback)
    {
        JOIN_USER_DATA JoinData = new JOIN_USER_DATA(email, nickName, password);
        socketManager.Socket.ExpectAcknowledgement<int>(callback).Emit("join", JoinData);
    }

    public void Login(string email, string password, Action<LOGIN_RESPONSE> callback)
    {
        LOGIN_USER_DATA LoginData = new LOGIN_USER_DATA(email, password);
        socketManager.Socket.ExpectAcknowledgement<LOGIN_RESPONSE>(callback).Emit("login", LoginData);
    }


    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    Debug.Log("Connect");
        //    SocketIO3Connet();
        //}



        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    Debug.Log("testEvent");
        //    LOGIN_USER_DATA LoginData = new LOGIN_USER_DATA(email, password);
        //    socketManager.Socket.Emit("login", LoginData);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    Debug.Log("testEvent2");
        //    socketManager.Socket.EmitAck("testEvent2");
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    Debug.Log("response");
        //}
    }

    public void TestReturn(int test)
    {
        Debug.Log("Get Test Return : " + test);
    }

    void OnAckCallback(Socket socket, BestHTTP.SocketIO.Packet originalPacket, params object[] args)
    {
        Debug.Log("OnAckCallback!");
    }
   
    private void OnConnected(ConnectResponse res)
    {
        Debug.Log("SocketClientManager >> OnConnected");
    }

    private void PlayerConnected()
    {
        Debug.Log("Player Connected!!");
    }

    private void ServerTest(ConnectResponse res)
    {
        Debug.Log("Server Test:" + res.sid);
    }

    private void OnApplicationQuit()
    {
        if (this.socketManager != null)
        {
            this.socketManager.Close();
            this.socketManager = null;
        }
    }
}
