using Microsoft.Xna.Framework;
using MonoGameLibrary.Networking;
using Steamworks;
using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary
{
    public class SteamCore : Core
    {
        protected Lobby? Lobby;
        private HostSocket HostSocket;
        private ClientConnection ClientConnection;
        protected List<SteamId> ConnectedPlayers = new List<SteamId>();
        protected override void Initialize()
        {
            base.Initialize();
            SteamClient.Init(480, false);
            SteamMatchmaking.OnLobbyEntered += (lobby) =>
            {
                this.Lobby = lobby;
                OnLobbyJoin();
                if (SteamClient.SteamId == lobby.Owner.Id)
                {
                    OnLobbyJoinHost();
                }
                else
                {
                    OnLobbyJoinClient();
                }
            };
            SteamFriends.OnGameLobbyJoinRequested += (lobby, friend) =>
            {
                SteamMatchmaking.JoinLobbyAsync(lobby.Id);
            };
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (SteamClient.IsValid)
            {
                SteamClient.RunCallbacks();
                if (HostSocket != null)
                {
                    HostSocket.Receive();
                }
                if (ClientConnection != null)
                {
                    ClientConnection.Receive();
                }
            }
        }
        protected override void OnExiting(object sender, ExitingEventArgs args)
        {
            if (SteamClient.IsValid) SteamClient.Shutdown();
            base.OnExiting(sender, args);
        }
        protected void HostServer(int PlayerCount)
        {
            if (!SteamClient.IsValid) return;
            SteamMatchmaking.CreateLobbyAsync(PlayerCount);

        }
        protected virtual void OnLobbyJoin() { }
        protected virtual void OnLobbyJoinHost()
        {
            HostSocket = SteamNetworkingSockets.CreateRelaySocket<HostSocket>(0);
            HostSocket.Bind(this);
        }
        protected virtual void OnLobbyJoinClient()
        {
            ClientConnection = SteamNetworkingSockets.ConnectRelay<ClientConnection>(Lobby.Value.Owner.Id);
            ClientConnection.Bind(this);
        }
        public virtual void OnClientConnectionRequested(Connection connection, ConnectionInfo data) { }
        public virtual void OnClientConnected(Connection connection, ConnectionInfo data) { }
        public virtual void OnClientDisconnected(Connection connection, ConnectionInfo data) { }
        public virtual void OnHostReceivedMessage(NetIdentity from, byte[] data) { }
        public virtual void OnClientReceivedMessage(byte[] data) { }
        protected void HostBroadcastMessage(byte[] data, SendType sendType = SendType.Unreliable)
        {
            if (HostSocket == null) return;
            unsafe
            {
                fixed (byte* p = data)
                {
                    foreach (var connection in HostSocket.Connected)
                    {
                        connection.SendMessage((nint)p, data.Length, sendType);
                    }
                }
            }
        }
        protected void ClientSendMessage(byte[] data, SendType sendType = SendType.Unreliable)
        {
            if (ClientConnection == null) return;
            unsafe
            {
                fixed (byte* p = data)
                {
                    ClientConnection.Connection.SendMessage((nint)p, data.Length, sendType);
                }
            }
        }
    }
}
