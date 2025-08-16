using Steamworks;
using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Networking
{
    internal class HostSocket : SocketManager
    {
        private SteamCore Core;
        public void Bind(SteamCore core)
        {
            this.Core = core;
        }
        public override void OnConnecting(Connection connection, ConnectionInfo data)
        {
            if (Core == null) return;
            base.OnConnecting(connection, data);
            Core.OnClientConnectionRequested(connection, data);
        }

        public override void OnConnected(Connection connection, ConnectionInfo data)
        {
            if (Core == null) return;
            base.OnConnected(connection, data);
            Core.OnClientConnected(connection, data);
        }

        public override void OnDisconnected(Connection connection, ConnectionInfo data)
        {
            if (Core == null) return;
            base.OnDisconnected(connection, data);
            Core.OnClientDisconnected(connection, data);
        }

        public override void OnMessage(Connection connection, NetIdentity from, IntPtr dataPtr, int size, long msgNum, long recvTime, int channel)
        {
            if (Core == null) return;
            base.OnMessage(connection, from, dataPtr, size, msgNum, recvTime, channel);
            byte[] bytes = new byte[size];
            Marshal.Copy(dataPtr, bytes, 0, size);
            Core.OnHostReceivedMessage(from, bytes);
        }
    }
}
